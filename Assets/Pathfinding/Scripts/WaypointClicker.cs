using UnityEngine;
using System.Collections;

public class WaypointClicker : MonoBehaviour {
  protected Vector3 waypoint;
  
  protected Sprite cursor;
  
  protected void Start() {
    waypoint = transform.parent.position;
    cursor = Resources.Load<Sprite>( "Cursors/cursor_feet" );
  }
  
	protected void OnClick() {
    Game.player.MoveTo( waypoint );
	}
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
