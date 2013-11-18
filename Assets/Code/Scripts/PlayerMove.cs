using UnityEngine;
using System.Collections;

public class PlayerMove : Pathfinding {
  
  void Start() {
    speed = 1;
  }

	void Update () {
    if( Path.Count > 0 ) {
      if( Path[0].x > 0f )
        transform.localScale = new Vector3( -1, 1, 1 );
      else if( Path[0].x < 0f ) 
        transform.localScale = new Vector3( 1, 1, 1 );
        
      Move();
    }
  }
}
