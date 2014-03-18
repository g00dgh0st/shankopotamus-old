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
    if( Game.heldItem != null ) return;
    if( isOver ) {
      Game.cursor.GetComponent<CustomCursor>().SetCursor( cursor );
      Game.cursor.SetActive( true );
      Screen.showCursor = false;
    } else {
      Game.cursor.SetActive( false );
      Screen.showCursor = true;
    }
  }
}
