using UnityEngine;
using System.Collections;

public class PlayerMove : Pathfinding {
  
  public void Start() {
    speed = 1;
  }

	public void Update () {
    if( Path.Count > 0 ) {
      transform.localScale = new Vector3( direction, 1, 1 );
      Move();
    }
  }
}
