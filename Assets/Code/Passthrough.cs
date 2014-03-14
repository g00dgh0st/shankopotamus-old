using UnityEngine;
using System.Collections;

public class Passthrough : WaypointClicker {

  public Transform pointA;
  public Transform pointB;
  
  void Start() {
    base.Start();
    cursor = Resources.Load( "Cursors/cursor_hand" ) as Texture2D;
  }
  
	void OnClick() {
    Vector3 playerPos = Game.player.gameObject.transform.position;
    
    if( Vector3.Distance( playerPos, pointA.position ) > Vector3.Distance( playerPos, pointB.position ) )
      Game.player.MoveTo( pointA.position );
    else
      Game.player.MoveTo( pointB.position );
	}

}
