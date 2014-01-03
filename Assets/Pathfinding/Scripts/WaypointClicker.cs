using UnityEngine;
using System.Collections;

public class WaypointClicker : MonoBehaviour {
  private Vector3 waypoint;
  
  public WaypointClicker sibling;
  public int siblingOrder;
  
  void Start() {
    waypoint = transform.parent.position;
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
}
