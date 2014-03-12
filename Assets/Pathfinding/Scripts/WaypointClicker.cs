using UnityEngine;
using System.Collections;

public class WaypointClicker : MonoBehaviour {
  protected Vector3 waypoint;
  
  protected Texture2D cursor;
  
  protected void Start() {
    waypoint = transform.parent.position;
    cursor = Resources.Load( "Cursors/cursor_feet" ) as Texture2D;
  }
  
	protected void OnClick() {
    Game.player.MoveTo( waypoint );
	}
  
  void OnHover( bool isOver ) {
    if( isOver )
      Cursor.SetCursor( cursor, Vector2.zero, CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }
}
