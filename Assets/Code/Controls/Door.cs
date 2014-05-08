using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

  // dest door
  public Transform destination;
  // exit point for this door
  public Transform exitPoint;
  
  private GameObject room;
  private GameObject destRoom;
  
  protected Sprite cursor;
  
  void Awake() {
    string[] names = gameObject.name.Split( '_' );
    
    room = GameObject.Find( names[1] );
    destRoom = GameObject.Find( names[2] );
  }
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_door" );
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
  
  public void OnClick() {
    if( Game.cookies.Contains( "stopDoor" ) ) {
      Game.cookies.Remove( "stopDoor" );
    }
    
    Game.PauseClicks();
        
    Game.player.MoveTo( transform.position );
    
    StartCoroutine( GoThroughDoor() );
  }
  
  private IEnumerator GoThroughDoor() {
    
    yield return new WaitForSeconds( 0.5f );
    
    
    while( Game.player.InMotion() ) {
      yield return null;
    }
    
    // if stopped because of stopDoor
    if( Game.cookies.Contains( "stopDoor" ) ) {
      Game.cookies.Remove( "stopDoor" );
      Game.ResumeClicks();
      return false;
    }
    
    StartCoroutine( Game.FadeCamera( delegate() {
      
      Game.currentRoom = destRoom;

      destRoom.SetActive( true );

      Game.player.TeleportTo( destination.position );
      
      Vector3 plPos = Game.player.transform.Find( "CamTarget" ).position;
      Camera.main.transform.position = new Vector3( plPos.x, plPos.y, Camera.main.transform.position.z );
    
      float cScale = destRoom.GetComponent<Room>().characterScale;
      
      Game.player.transform.localScale = new Vector3( ( Game.player.transform.localScale.x < 0 ? -cScale : cScale ), cScale, cScale );
    
      Game.player.MoveTo( destination.gameObject.GetComponent<Door>().exitPoint.position );
    
      Game.ResumeClicks();
      
      room.SetActive( false );
    } ) );
    
    
  }

}
