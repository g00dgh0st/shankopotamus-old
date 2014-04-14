using UnityEngine;
using System.Collections;

public class Door : WaypointClicker {

  public Transform destination;
  public Transform exitPoint;
  
  private GameObject room;
  private GameObject destRoom;
  
  void Awake() {
    string[] names = transform.parent.gameObject.name.Split( '_' );
    
    room = GameObject.Find( names[1] );
    destRoom = GameObject.Find( names[2] );
  }
  
  void Start() {
    base.Start();
    cursor = Resources.Load<Sprite>( "Cursors/cursor_door" );
        
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
    
    StartCoroutine( Game.FadeCamera( delegate() {
      
      destRoom.SetActive( true );
      
      Game.currentRoom = destRoom;
      
      Game.player.TeleportTo( destination.position );
      
      Vector3 plPos = Game.player.transform.Find( "CamTarget" ).position;
      Camera.main.transform.position = new Vector3( plPos.x, plPos.y, -20f );
    
      room.SetActive( false );
      
      float cScale = destRoom.GetComponent<Room>().characterScale;
      
      Game.player.transform.localScale = new Vector3( ( Game.player.transform.localScale.x < 0 ? -cScale : cScale ), cScale, cScale );
    
      Game.player.MoveTo( exitPoint.position );
    
      Game.ResumeClicks();
    } ) );
    
    
  }

}
