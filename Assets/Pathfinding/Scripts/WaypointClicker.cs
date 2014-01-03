using UnityEngine;
using System.Collections;

public class WaypointClicker : MonoBehaviour {
  private Vector3 waypoint;
  
  public WaypointClicker sibling;
  public int siblingOrder;
  
  private Texture2D cursor;
  
  void Start() {
    waypoint = transform.parent.position;
    cursor = Resources.Load( "Cursors/cursor_feet" ) as Texture2D;
  }
  
	void OnMouseDown() {
    
    if( !sibling )
  	  Game.player.MoveTo( waypoint );
    else {
      // Move to the lower ordered sibling
      if( siblingOrder < sibling.siblingOrder ) Game.player.MoveTo( waypoint );
      else Game.player.MoveTo( sibling.waypoint );
      

      // Switch order of siblings for next click
      int tempOrder = siblingOrder;
      siblingOrder = sibling.siblingOrder;
      sibling.siblingOrder = tempOrder;
    }
	}
  
  public void OnMouseOver() {
    Cursor.SetCursor( cursor, Vector2.zero, CursorMode.Auto );
  }
  
  public void OnMouseExit() {
		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
	}
}
