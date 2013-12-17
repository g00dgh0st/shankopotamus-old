using UnityEngine;
using System.Collections;

public class Player : Pathfinding {
  public bool canMove = true;
  public float moveSpeed = 1;
  
  public void Start() {
    speed = moveSpeed; // override the Pathfinding speed
  }

	public void Update () {
    if( Path.Count > 0 ) {
      transform.localScale = new Vector3( direction, 1, 1 );
      Move();
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
