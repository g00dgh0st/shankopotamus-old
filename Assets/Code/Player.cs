using UnityEngine;
using System.Collections;

public class Player : Pathfinding {
  public delegate void Callback();
  private Callback moveCallback = null;
  
  public bool canMove = true;
  public float moveSpeed = 0.25f;
  
  public void Start() {
    speed = moveSpeed; // override the Pathfinding speed
  }

	public void Update() {
    if( Path.Count > 0 ) {
      if( ( transform.localScale.x > 0 && direction < 0 ) || ( transform.localScale.x < 0 && direction > 0 ) ) {
        transform.localScale = new Vector3( -1 * transform.localScale.x, transform.localScale.y, transform.localScale.z );
        
        foreach( MeshFilter filter in GetComponentsInChildren<MeshFilter>() ) {
          Game.ReverseNormals( filter.mesh );
        }
      }
      Move();
    } 
  }
  
  public void TeleportTo( Vector3 dest ) {
    transform.position = dest;
  }
  
  public void MoveTo( Vector3 dest, Callback c ) {
    FindPath( transform.position, dest );
    moveCallback = c;
    Game.PauseClicks();
    
    StartCoroutine( MoveCallback( dest ) );
  }
  
  public IEnumerator MoveCallback( Vector3 target ) {
    yield return new WaitForSeconds( 0.5f );
    
    while( InMotion() ) {
      yield return null;
    }
    
    FaceTarget( target );

    Game.ResumeClicks();
    
    moveCallback();
    moveCallback = null;
  }

  public void MoveTo( Vector3 dest ) {
    FindPath( transform.position, dest );
  }
  
  public bool InMotion() {
    return Path.Count > 0;
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
