using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour {
  [HideInInspector]
  public Animation anim;

  [HideInInspector]
  public bool interacting = false;
  
  public void Start() {
    anim = transform.Find( "Shanko" ).gameObject.GetComponent<Animation>();
  }

	public void Update() {
    if( InMotion() ) {
      if( !interacting ) anim.CrossFade( "walk" );
      if( Game.currentRoom ) GetComponent<LayerSetter>().SetOrder( Game.currentRoom.GetComponent<Room>().GetNewOrder( transform.position ) );
      if( Direction() != 0 && ( ( transform.localScale.x > 0 && Direction() < 0 ) || ( transform.localScale.x < 0 && Direction() > 0 ) ) ){
        transform.localScale = new Vector3( -1 * transform.localScale.x, transform.localScale.y, transform.localScale.z );
        
        foreach( MeshFilter filter in GetComponentsInChildren<MeshFilter>() ) {
          Game.ReverseNormals( filter.mesh );
        }
      }
    } else if( !interacting ) {
      anim.CrossFade( "idle" );
    } 
  }
  
  public int Direction() {
    float dir = gameObject.GetComponent<PolyNavAgent>().movingDirection.x;
    return ( dir > 0 ? 1 : ( dir == 0 ? 0 : -1 ) );
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
  public void Interact( String animation, Action callback, float waitTime ) {
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
      case "push_box_start":
        anim.CrossFade( "push_box_start", 0.01f );
        break;
      case "push_box_stop":
        anim.CrossFade( "push_box_stop", 0.1f );
        break;
      case "push_box":
        anim.CrossFade( "push_box" );
        break;
      case "take":
      default:
        anim.CrossFade( "take" );
        break;
    }
    StartCoroutine( InteractionThing( animation, callback, waitTime ) );
  }
  
  public void Interact( String animation, Action callback ) {
    Interact( animation, callback, 0.8f );
  }
  
  IEnumerator InteractionThing( String animation, Action callback, float waitTime ) {
    yield return new WaitForSeconds( waitTime );
    callback();
    while( anim.IsPlaying( animation ) ) {
      yield return null;
    }
    interacting = false;
  }
}
