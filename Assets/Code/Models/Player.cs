using UnityEngine;
using System.Collections;

public class Player : Pathfinding {
  public bool canMove = true;
  public float moveSpeed = 0.25f;
  
  private Animation anim;
  
  public void Start() {
    speed = moveSpeed; // override the Pathfinding speed
    anim = transform.Find( "TestShankFBX" ).gameObject.GetComponent<Animation>();
  }

	public void Update() {
    if( Path.Count > 0 ) {
      transform.localScale = new Vector3( direction, 1, 1 );
      Move();
      if( !anim.IsPlaying( "Walk" ) ) anim.CrossFade( "Walk", 0.2f );
    } else {
      if( !anim.IsPlaying( "Idle" ) ) anim.CrossFade( "Idle", 0.3f );
    }
  }
  
  public void TeleportTo( Vector3 dest ) {
    transform.position = dest;
  }
  
  public void MoveTo( Vector3 dest ) {
	  FindPath( transform.position, dest );
  }
  
  public bool InMotion() {
    return Path.Count > 0;
  }
}
