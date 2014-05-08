using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Navigation/PolyNavAgent")]
public class PolyNavAgent : MonoBehaviour{

	public float maxSpeed             = 3.5f;
	public float mass                 = 20;
	public float stoppingDistance     = 0.1f;
  // public float slowingDistance      = 1;
  // public float decelerationRate     = 2;
	public bool closerPointOnInvalid  = false;
	public bool rotateTransform       = false;
	public float rotateSpeed          = 350;
	public bool sendMessages          = true;
	public bool debugPath             = true;
	
	private float lookAhead           = 1;
	private Vector2 velocity          = Vector2.zero;
	private float maxForce            = 100;
	private int requests              = 0;
	private List<Vector2> _activePath = new List<Vector2>();
	private Vector2 _primeGoal        = Vector2.zero;
	private Transform _transform;
	private Action<bool> reachedCallback;

	public static int totalAgents;

	public Vector2 agentPosition{
		get {return _transform.position;}
		set {_transform.position = value;}
	}

	public List<Vector2> activePath{
		get
		{
			return _activePath;
		}
		set
		{
			_activePath = value;
			if (_activePath.Count > 0)
				_activePath.RemoveAt(0);			
		}
	}

	public Vector2 primeGoal{
		get { return _primeGoal;}
		set { _primeGoal = value;}
	}

	public bool pathPending{
		get	{ return requests > 0;}
	}

	public PolyNav2D polyNav{
		get {return PolyNav2D.current;}
	}

	public bool hasPath{
		get { return activePath.Count > 0;}
	}

	public Vector2 nextPoint{
		get
		{
			if (hasPath)
				return activePath[0];
			return agentPosition;			
		}
	}

	public float remainingDistance{
		get
		{
			if (!hasPath)
				return 0;

			float dist= Vector2.Distance(agentPosition, activePath[0]);
			for (int i= 0; i < activePath.Count; i++)
				dist += Vector2.Distance(activePath[i], activePath[i == activePath.Count - 1? i : i + 1]);

			return dist;			
		}
	}

	public Vector2 movingDirection{
		get
		{
			if (hasPath)
				return velocity.normalized;
			return Vector2.zero;			
		}
	}

	//////
	//////

	void OnEnable(){
		totalAgents++;
	}

	void OnDisable(){
		totalAgents--;
	}

	void Awake(){
		_transform = transform;
	}

	//set the destination for the agent. as a result the agent starts moving
	public bool SetDestination(Vector2 goal){
		return SetDestination(goal, null);
	}

	//set the destination for the agent. as a result the agent starts moving. Only the callback from the last SetDestination will be called upon arrival
	public bool SetDestination(Vector2 goal, Action<bool> callback){

		if (!polyNav){
			Debug.LogError("No PolyNav2D in scene!");
			return false;
		}

		//goal is almost the same as the last goal. Nothing happens for performace in case it's called frequently
		if ((goal - primeGoal).magnitude < Mathf.Epsilon)
			return true;

		reachedCallback = callback;
		primeGoal = goal;

		//goal is almost the same as agent position. We consider arrived immediately
		if ((goal - agentPosition).magnitude < stoppingDistance){
			OnArrived();
			return true;
		}

		//check if goal is valid
		if (!polyNav.PointIsValid(goal)){
			if(closerPointOnInvalid){
				SetDestination(polyNav.GetCloserEdgePoint(goal), callback);
				return true;
			} else {
				OnInvalid();
				return false;
			}
		}

		//if a path is pending dont calculate new path
		//the prime goal will be repathed anyway
		if (requests > 0)
			return true;

		//compute path
		requests++;
		polyNav.FindPath(agentPosition, goal, SetPath);

		return true;
	}

	//the callback from polyNav for when path is ready to use
	private void SetPath(List<Vector2> path){
		
		//in case the agent stoped somehow, but a path was pending
		if (requests == 0)
			return;

		requests --;

		if (path.Count == 0){
			OnInvalid();
			return;
		}

		activePath = path;
		if (sendMessages)
			gameObject.SendMessage("OnNavigationStarted", SendMessageOptions.DontRequireReceiver);
	}

	//main loop
	public void LateUpdate(){

		if (!polyNav)
			return;

		//when there is no path just restrict
		if (!hasPath){
			Restrict();
			return;
		}

		//calculate velocities
    // if (remainingDistance < slowingDistance){
    //   
    //   velocity += Arrive(nextPoint) / mass;
    // 
    // } else {

			velocity += Seek(nextPoint) / mass;
    // }

		velocity = Truncate(velocity, maxSpeed);
		//

		//slow down if wall ahead
    // LookAhead();

		//move the agent
		agentPosition += velocity * Time.deltaTime;

		//restrict just after movement
		Restrict();

		//rotate if must
		if (rotateTransform){
			float rot = -Mathf.Atan2(movingDirection.x, movingDirection.y) * 180 / Mathf.PI;
			float newZ = Mathf.MoveTowardsAngle(transform.localEulerAngles.z, rot, rotateSpeed * Time.deltaTime);
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, newZ);
		}

		//repath if there is no LOS with the next point
		if (polyNav.CheckLOS(agentPosition, nextPoint) == false)
			Repath();

		//in case after rapthing there is no path
		if (!hasPath){
			OnArrived();
			return;
		}

		//Check and remove if we reached a point. proximity distance depents
		float proximity = (activePath[activePath.Count -1] == nextPoint)? stoppingDistance : 0.001f;
		if ((agentPosition - nextPoint).magnitude <= proximity){

			activePath.RemoveAt(0);

			//if it was last point, means we dont still have an activePath
			if (!hasPath){
			
				OnArrived();
				return;
			
			} else {

				//repath after a point is reached
				Repath();
				if (sendMessages)
					gameObject.SendMessage("OnNavigationPointReached", SendMessageOptions.DontRequireReceiver);
			}
		}

		//little trick. Check the next waypoint ahead of the current for LOS and if true consider the current reached.
		//helps for tight corners and when agent has big innertia
		if (activePath.Count > 1 && polyNav.CheckLOS(agentPosition, activePath[1]))
			activePath.RemoveAt(0);
	}

	//recalculate path to prime goal if there is no pending requests
	private void Repath(){

		if (requests > 0)
			return;

		requests ++;
		polyNav.FindPath(agentPosition, primeGoal, SetPath);
	}

	//stop the agent and callback + message
	private void OnArrived(){

		Stop();

		if (reachedCallback != null)
			reachedCallback(true);

		if (sendMessages)
			gameObject.SendMessage("OnDestinationReached", SendMessageOptions.DontRequireReceiver);
	}

	//stop the agent and callback + message
	private void OnInvalid(){

		Stop();

		if (reachedCallback != null)
			reachedCallback(false);

		if (sendMessages)
			gameObject.SendMessage("OnDestinationInvalid", SendMessageOptions.DontRequireReceiver);
	}
	
	//clears the path and as a result the agent is stop moving
	//resets some vars
	public void Stop(){
		
		activePath.Clear();
		velocity = Vector2.zero;
		requests = 0;
		primeGoal = agentPosition;
	}


	
	//seeking a target
	private Vector2 Seek(Vector2 pos){

		Vector2 desiredVelocity= (pos - agentPosition).normalized * maxSpeed;
		Vector2 steer= desiredVelocity - velocity;
		steer = Truncate(steer, maxForce);
		return steer;
	}

	//slowing at target's arrival
  // private Vector2 Arrive(Vector2 pos){
  // 
  //   var desiredVelocity = (pos - agentPosition);
  //   float dist= desiredVelocity.magnitude;
  // 
  //   if (dist > 0){
  //     var reqSpeed = dist / (decelerationRate * 0.3f);
  //     reqSpeed = Mathf.Min(reqSpeed, maxSpeed);
  //     desiredVelocity *= reqSpeed / dist;
  //   }
  // 
  //   Vector2 steer= desiredVelocity - velocity;
  //   steer = Truncate(steer, maxForce);
  //   return steer;
  // }

	//slowing when there is an obstacle ahead
	//not implemented as best as it could.TODO
	private void LookAhead(){

		float currentLookAheadDistance= Mathf.Lerp(0, lookAhead, velocity.magnitude/maxSpeed);
		Vector2 lookAheadVector= agentPosition + velocity.normalized * currentLookAheadDistance;

		Debug.DrawLine(agentPosition, lookAheadVector, Color.blue);

		if (!polyNav.PointIsValid(lookAheadVector)){
			velocity -= (lookAheadVector - agentPosition);
			Repath();
		}
	}

	//keep agent within valid area
	private void Restrict(){

		if (!polyNav.PointIsValid(agentPosition))
			agentPosition = polyNav.GetCloserEdgePoint(agentPosition);
	}
    
    //limit the magnitude of a vector
    private Vector2 Truncate(Vector2 vec, float max){
        if (vec.magnitude > max) {
            vec.Normalize();
            vec *= max;
        }
        return vec;
    }


    ////////////////////////////////////////
    ///////////GUI AND EDITOR STUFF/////////
    ////////////////////////////////////////
    
    public void OnDrawGizmos(){

    	if (!hasPath)
    		return;

		if (debugPath){
			Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
			Gizmos.DrawLine(agentPosition, activePath[0]);
			for (int i= 0; i < activePath.Count; i++)
				Gizmos.DrawLine(activePath[i], activePath[(i == activePath.Count - 1)? i : i + 1]);
		}	
    }
}
