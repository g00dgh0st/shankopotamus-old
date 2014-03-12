using UnityEngine;
using System.Collections;

public class Door : WaypointClicker {

  public Transform destination;
  public Transform exitPoint;
  
  void Start() {
    base.Start();
    cursor = Resources.Load( "Cursors/cursor_door" ) as Texture2D;
  }
  
  void OnClick() {
    // Game.PauseClicks();
        
    // StartCoroutine( GoThroughDoor() );
    
    base.OnClick();
  }
  
  // private IEnumerator GoThroughDoor() {
    // Game.player.GetComponent<Player>().
  // }

}
