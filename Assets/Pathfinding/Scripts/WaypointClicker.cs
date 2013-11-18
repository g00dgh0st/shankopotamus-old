using UnityEngine;
using System.Collections;

public class WaypointClicker : MonoBehaviour {
  private Vector3 waypoint;
  private PlayerMove playerMove;
  
  void Start() {
    waypoint = transform.parent.position;
    playerMove = GameObject.FindWithTag( "Player" ).GetComponent<PlayerMove>();
  }
  
	void OnMouseDown() {
	  playerMove.FindPath( playerMove.gameObject.transform.position, waypoint );
	}
}
