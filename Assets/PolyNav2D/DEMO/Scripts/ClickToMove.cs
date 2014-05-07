using UnityEngine;

//example
[RequireComponent(typeof(PolyNavAgent))]
public class ClickToMove : MonoBehaviour{
	
	private PolyNavAgent _agent;

	public PolyNavAgent agent{
		get
		{
			if (!_agent)
				_agent = GetComponent(typeof(PolyNavAgent)) as PolyNavAgent;
			return _agent;			
		}
	}

	private void Update () {
		if (Input.GetMouseButtonDown(0)) {
			agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		  
		}
	}

	//Message from Agent
	private void OnDestinationReached(){

		//do something here...
	}

	//Message from Agent
	private void OnDestinationInvalid(){

		//do something here...
	}
}
