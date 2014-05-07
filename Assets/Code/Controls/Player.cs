using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour {
  // public delegate void Callback();
  // private Callback moveCallback = null;
  
  // public bool canMove = true;
  // public float moveSpeed = 0.25f;
  
  public void Start() {
    // speed = moveSpeed; // override the Pathfinding speed
  }

	public void Update() {
		if (Input.GetMouseButton(0)) {
			MoveTo( Camera.main.ScreenToWorldPoint( Input.mousePosition ) );
		}
    
    if( InMotion() ) {
      GetComponent<LayerSetter>().SetOrder( Game.currentRoom.GetComponent<Room>().GetNewOrder( transform.position.y ) );
      if( ( transform.localScale.x > 0 && Direction() < 0 ) || ( transform.localScale.x < 0 && Direction() > 0 ) ) {
        transform.localScale = new Vector3( -1 * transform.localScale.x, transform.localScale.y, transform.localScale.z );
        
        foreach( MeshFilter filter in GetComponentsInChildren<MeshFilter>() ) {
          Game.ReverseNormals( filter.mesh );
        }
      }
    } 
  }
  
  public int Direction() {
    return ( gameObject.GetComponent<PolyNavAgent>().movingDirection.x > 0 ? 1 : -1 );
  }
  
  public void TeleportTo( Vector3 dest ) {
    transform.position = dest;
  }
  
  public void MoveTo( Vector3 dest, Action<bool> c ) {
		gameObject.GetComponent<PolyNavAgent>().SetDestination(dest, c);
  }
  // 
  // public IEnumerator MoveCallback( Vector3 target ) {
  //   yield return new WaitForSeconds( 0.5f );
  //   
  //   while( InMotion() ) {
  //     yield return null;
  //   }
  //   
  //   FaceTarget( target );
  // 
  //   Game.ResumeClicks();
  //   
  //   moveCallback();
  //   moveCallback = null;
  // }

  public void MoveTo( Vector3 dest ) {
		gameObject.GetComponent<PolyNavAgent>().SetDestination(dest);
  }
  
  public void StopMove() {
		gameObject.GetComponent<PolyNavAgent>().Stop();
  }
  
  public bool InMotion() {
		return gameObject.GetComponent<PolyNavAgent>().hasPath;
  }
  
  public void FaceTarget( Vector3 target ) {
    if( ( target.x > transform.position.x && transform.localScale.x < 0 ) || ( target.x < transform.position.x && transform.localScale.x > 0 ) ) {
      transform.localScale = new Vector3( -1 * transform.localScale.x, transform.localScale.y, transform.localScale.z );
    }
  }
  
  public void FaceTarget( Vector3 target, float delay ) {
    StartCoroutine( DelayFaceTarget( target, delay ) );
  }
  
  public IEnumerator DelayFaceTarget( Vector3 target, float delay ) {
    yield return new WaitForSeconds( delay );
    if( ( target.x > transform.position.x && transform.localScale.x < 0 ) || ( target.x < transform.position.x && transform.localScale.x > 0 ) ) {
      transform.localScale = new Vector3( -1 * transform.localScale.x, transform.localScale.y, transform.localScale.z );
    }
  }
}
