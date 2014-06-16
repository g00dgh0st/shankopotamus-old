using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
  
  private Transform playerTrans;
  private float mouseScrollThreshold = 50f;
  private float mouseScrollSpeed = 1.5f;
  
  public bool isPaused = false;
  
  public Room.CameraType camType = Room.CameraType.FreeScroll;

	// Use this for initialization
	void Start() {
	  playerTrans = Game.player.gameObject.transform.Find( "CamTarget" );
    Reset();
	}
  
  public void Reset() {
    camType = Game.currentRoom.GetComponent<Room>().cameraType;
    Vector3 plPos = playerTrans.position;
    
    switch( camType ) {
      case Room.CameraType.FreeScroll:
        transform.position = new Vector3( plPos.x, plPos.y, transform.position.z );
        break;
      case Room.CameraType.Static:
        transform.position = new Vector3( Game.currentRoom.transform.position.x, Game.currentRoom.transform.position.y, transform.position.z );
        break;
      case Room.CameraType.HorizontalScroll:
        transform.position = new Vector3( plPos.x, Game.currentRoom.GetComponent<Room>().XYLock, transform.position.z );
        break;
      case Room.CameraType.VerticalScroll:
        transform.position = new Vector3( Game.currentRoom.GetComponent<Room>().XYLock, plPos.y, transform.position.z );
        break;
      default:
        transform.position = new Vector3( plPos.x, plPos.y, transform.position.z );
        break;
    }
  }
	
	// Update is called once per frame
	void Update() {    
    if( isPaused ) return;
    
    ArrayList sprites = new ArrayList();
    
    foreach( Transform child in Game.currentRoom.transform.Find( "Sprites" ) ) {
      if( child.gameObject.CompareTag( "MainSprite" ) ) sprites.Add( child.gameObject );
    }
    
    Bounds roomBounds = ((GameObject)sprites[0]).GetComponent<SpriteRenderer>().bounds;
    
    for( int i = 1; i < sprites.Count; i++ ) {
      roomBounds.Encapsulate( ((GameObject)sprites[i]).GetComponent<SpriteRenderer>().bounds );
    }
    
    Vector3 pos = transform.position;
    Vector3 newPos;

    if( Game.player.InMotion() && camType != Room.CameraType.Static ) {
      
      Vector3 diff;
      
      switch( camType ) {
        case Room.CameraType.FreeScroll:
          diff = new Vector3( playerTrans.position.x, playerTrans.position.y, transform.position.z ) - pos;
          break;
        case Room.CameraType.HorizontalScroll:
          diff = new Vector3( playerTrans.position.x, transform.position.y, transform.position.z ) - pos;
          break;
        case Room.CameraType.VerticalScroll:
          diff = new Vector3( transform.position.x, playerTrans.position.y, transform.position.z ) - pos;
          break;
        default:
          diff = new Vector3( playerTrans.position.x, playerTrans.position.y, transform.position.z ) - pos;
          break;
      }
      
      newPos = pos + ( diff * 0.05f );
      
      if( CheckBounds( roomBounds, newPos, pos ) )
        transform.position = newPos;
    } 
    // else if( Input.mousePosition.x < Screen.width && Input.mousePosition.x > 0 && Input.mousePosition.y > 0 && Input.mousePosition.y < Screen.height ){
    //   
    //   newPos = pos;
    //   
    //   if( Input.mousePosition.x > Screen.width - mouseScrollThreshold || Input.GetKey( KeyCode.RightArrow ) ) 
    //     newPos = new Vector3( newPos.x + ( mouseScrollSpeed * Time.deltaTime ), newPos.y, newPos.z );
    //   
    //   if( Input.mousePosition.x < 0 + mouseScrollThreshold || Input.GetKey( KeyCode.LeftArrow ) ) 
    //     newPos = new Vector3( newPos.x - ( mouseScrollSpeed * Time.deltaTime ), newPos.y, newPos.z );
    //   
    //   if( Input.mousePosition.y > Screen.height - mouseScrollThreshold || Input.GetKey( KeyCode.UpArrow ) ) 
    //     newPos = new Vector3( newPos.x, newPos.y + ( mouseScrollSpeed * Time.deltaTime ), newPos.z );
    //   
    //   if( Input.mousePosition.y < 0 + mouseScrollThreshold || Input.GetKey( KeyCode.DownArrow ) ) 
    //     newPos = new Vector3( newPos.x, newPos.y - ( mouseScrollSpeed * Time.deltaTime ), newPos.z );
    //   
    //   if( newPos != pos && CheckBounds( roomBounds, newPos, pos ) )
    //     transform.position = newPos;
    // }
	}
  
  private bool CheckBounds( Bounds roomBounds, Vector3 newPos, Vector3 pos ) {
    Vector3 offset = new Vector2( Screen.width * 0.25f, Screen.height * 0.25f );
    
    Vector3 top = Camera.main.ScreenToWorldPoint( new Vector3( Screen.width / 2f, ( Screen.height / 2f ) + offset.y, 0f ) );
    Vector3 bot = Camera.main.ScreenToWorldPoint( new Vector3( Screen.width / 2f, ( Screen.height / 2f ) - offset.y, 0f ) );
    Vector3 rgt = Camera.main.ScreenToWorldPoint( new Vector3( ( Screen.width / 2f ) + offset.x, Screen.height / 2f, 0f ) );
    Vector3 lft = Camera.main.ScreenToWorldPoint( new Vector3( ( Screen.width / 2f ) - offset.x, Screen.height / 2f, 0f ) );

    Vector3 diff = newPos - pos;
    
    Vector3 newTop = top + diff;
    Vector3 newBot = bot + diff;
    Vector3 newRgt = rgt + diff;
    Vector3 newLft = lft + diff;
    
    return ( newTop.y <= roomBounds.center.y + roomBounds.extents.y || newTop.y <= top.y ) && 
    ( newBot.y >= roomBounds.center.y - roomBounds.extents.y || newBot.y >= bot.y ) &&
    ( newRgt.x <= roomBounds.center.x + roomBounds.extents.x || newRgt.x <= rgt.x ) &&
    ( newLft.x >= roomBounds.center.x - roomBounds.extents.x || newLft.x >= lft.x );
  }
}
