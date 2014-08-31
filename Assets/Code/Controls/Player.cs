using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour {
  
  public Animation anim;
  
  private bool interacting = false;
  
  public void Start() {
    anim = transform.Find( "Shanko" ).gameObject.GetComponent<Animation>();
  }

	public void Update() {
    if( InMotion() ) {
      if( !interacting ) anim.CrossFade( "walk" );
      GetComponent<LayerSetter>().SetOrder( Game.currentRoom.GetComponent<Room>().GetNewOrder( transform.position ) );
      if( ( transform.localScale.x > 0 && Direction() < 0 ) || ( transform.localScale.x < 0 && Direction() > 0 ) ) {
        transform.localScale = new Vector3( -1 * transform.localScale.x, transform.localScale.y, transform.localScale.z );
        
        foreach( MeshFilter filter in GetComponentsInChildren<MeshFilter>() ) {
          Game.ReverseNormals( filter.mesh );
        }
      }
    } else if( !interacting ) {
      anim.CrossFade( "static" ); // switch to idle
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

  public void MoveTo( Vector3 dest ) {
		gameObject.GetComponent<PolyNavAgent>().SetDestination(dest);
  }
  
  public void StopMove( bool stopDoor ) {
    if( !Game.cookies.Contains( "stopDoor" ) && stopDoor == true ) Game.cookies.Add( "stopDoor", true );
		gameObject.GetComponent<PolyNavAgent>().Stop();
  }
  
  public bool InMotion() {
		return gameObject.GetComponent<PolyNavAgent>().hasPath;
  }
  
  public void PauseNav() {
    gameObject.GetComponent<PolyNavAgent>().paused = true;
  }

  public void ResumeNav() {
    gameObject.GetComponent<PolyNavAgent>().paused = false;
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
  
  
  
  /// INTERACTION STUFFFF
  public void Interact( String animation, Action callback ) {
    interacting = true;
    switch( animation ) {
      case "press":
        anim.CrossFade( "press" );
        break;
      case "take_high":
        anim.CrossFade( "take_high" );
        break;
      case "take_low":
        anim.CrossFade( "take_low" );
        break;
      case "take":
      default:
        anim.CrossFade( "take" );
        break;
    }
    StartCoroutine( InterationThing( callback ) );
  }
  
  IEnumerator InterationThing( Action callback ) {
    yield return new WaitForSeconds( 0.8f );
    callback();
    interacting = false;
  }
}
