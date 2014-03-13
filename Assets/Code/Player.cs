using UnityEngine;
using System.Collections;

public class Player : Pathfinding {
  public bool canMove = true;
  public float moveSpeed = 0.25f;
  
  private int lastDirection;
  
  public void Start() {
    speed = moveSpeed; // override the Pathfinding speed
    lastDirection = direction;
  }

	public void Update() {
    if( Path.Count > 0 ) {
      if( direction != lastDirection ) {
        transform.localScale = new Vector3( -1 * transform.localScale.x, transform.localScale.y, transform.localScale.z );
        lastDirection = direction;
        
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
  
  public void MoveTo( Vector3 dest ) {
	  FindPath( transform.position, dest );
  }
  
  public bool InMotion() {
    return Path.Count > 0;
  }
}
