using UnityEngine;
using System.Collections;

public class WaypointClicker : MonoBehaviour {
  private Vector3 waypoint;
  
  void Start() {
    waypoint = transform.parent.position;
  }
  
	void OnMouseDown() {
	  Game.player.MoveTo( waypoint );
	}
}
