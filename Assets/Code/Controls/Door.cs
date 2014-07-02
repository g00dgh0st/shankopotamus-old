using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
  
  public static bool goingThrough = false;

  // dest door
  public Transform destination;
  // exit point for this door
  [HideInInspector]
  public Transform exitPoint;
  
  private GameObject room;
  private GameObject destRoom;
  
  protected Sprite cursor;
  
  public bool isLocked = false;
  
  void Awake() {
    string[] names = gameObject.name.Split( '_' );
    
    room = GameObject.Find( names[1] );
    destRoom = GameObject.Find( names[2] );
    
    if( isLocked ) {
      transform.Find( "blocker" ).gameObject.SetActive( true );
    }
  }
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_door" );
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
    
    if( isLocked ) return;
    if( transform.Find( "closed" ) && transform.Find( "open" ) ) {
      if( isOver ) {
        transform.Find( "closed" ).gameObject.SetActive( false );
        transform.Find( "open" ).gameObject.SetActive( true );
      } else if( !isOver && !Game.clicksPaused ){
        transform.Find( "closed" ).gameObject.SetActive( true );
        transform.Find( "open" ).gameObject.SetActive( false );
      }
    } 
        
  }
  
  void Update() {
    if( isLocked ) return;
    if( collider2D.OverlapPoint( Game.player.transform.position ) && !Game.clicksPaused && !Door.goingThrough ) OnClick();
  }
  
  public void OnClick() {
    if( isLocked ) {
      Game.script.ShowSpeechBubble( "It's locked.",  Game.player.transform.Find( "BubTarget" ), 2f );
    } else {
      if( Game.cookies.Contains( "stopDoor" ) ) {
        Game.cookies.Remove( "stopDoor" );
      }
    
      if( transform.Find( "closed" ) && transform.Find( "open" ) ) {
        transform.Find( "closed" ).gameObject.SetActive( false );
        transform.Find( "open" ).gameObject.SetActive( true );
      } 
    
      Game.PauseClicks();
        
      Game.player.MoveTo( transform.position );
    
      StartCoroutine( GoThroughDoor() );
    }
  }
  
  private IEnumerator GoThroughDoor() {
    
    yield return new WaitForSeconds( 0.5f );
    
    
    while( Game.player.InMotion() ) {
      yield return null;
    }
    
    Game.player.StopMove( false );
    
    // if stopped because of stopDoor
    if( Game.cookies.Contains( "stopDoor" ) ) {
      Game.cookies.Remove( "stopDoor" );
      Game.ResumeClicks();
      return false;
    }
    
    StartCoroutine( Game.FadeCamera( delegate() {
      
      if( transform.Find( "closed" ) && transform.Find( "open" ) ) {
        transform.Find( "closed" ).gameObject.SetActive( true );
        transform.Find( "open" ).gameObject.SetActive( false );
      } 
      
      Door.goingThrough = true;
      
      Game.currentRoom = destRoom;

      destRoom.SetActive( true );

      Game.player.TeleportTo( destination.position );
      
      if( destination.Find( "closed" ) && destination.Find( "open" ) ) {
        destination.Find( "closed" ).gameObject.SetActive( false );
        destination.Find( "open" ).gameObject.SetActive( true );
      } 
      
      Camera.main.GetComponent<CameraControl>().Reset();
    
      float cScale = destRoom.GetComponent<Room>().characterScale;
      Game.player.transform.localScale = new Vector3( ( Game.player.transform.localScale.x < 0 ? -cScale : cScale ), cScale, cScale );
      Game.ResumeClicks();
      
      Game.player.MoveTo( destination.gameObject.GetComponent<Door>().exitPoint.position, delegate( bool b ) { 
        Door.goingThrough = false; 
        if( destination.Find( "closed" ) && destination.Find( "open" ) ) {
          destination.Find( "closed" ).gameObject.SetActive( true );
          destination.Find( "open" ).gameObject.SetActive( false );
        } 
      } );
    
      room.SetActive( false );
    } ) );
  }
  
  public void Lock() {
    isLocked = true;
    transform.Find( "blocker" ).gameObject.SetActive( true );
  }
  
  public void Unlock() {
    isLocked = false;
    transform.Find( "blocker" ).gameObject.SetActive( false );
  }

}
