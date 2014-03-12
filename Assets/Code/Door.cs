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
    Game.PauseClicks();
        
    base.OnClick();
    
    StartCoroutine( GoThroughDoor() );
  }
  
  private IEnumerator GoThroughDoor() {
    
    yield return new WaitForSeconds( 0.5f );
    
    while( Game.player.InMotion() ) {
      yield return null;
    }
    
    Game.player.TeleportTo( destination.position );
    
    Game.player.MoveTo( exitPoint.position );
    
    Game.ResumeClicks();
    
  }

}
