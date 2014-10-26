using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
  
  private Transform playerTrans;
  private float mouseScrollThreshold = 50f;
  private float mouseScrollSpeed = 1.5f;
  
  public bool isPaused = false;
  public bool pauseScale = false;
  
  public Room.CameraType camType = Room.CameraType.FreeScroll;
  
  public float lookAhead = 0.5f;

	// Use this for initialization
	void Start() {
	  playerTrans = Game.player.gameObject.transform.Find( "CamTarget" );
    Reset();
	}
  
  public void Reset() {
    Room curRoom = Game.currentRoom.GetComponent<Room>();
    camType = curRoom.cameraType;
    lookAhead = curRoom.lookAhead;
    Vector3 plPos = playerTrans.position;
    
    switch( camType ) {
      case Room.CameraType.FreeScroll:
        transform.position = new Vector3( plPos.x, plPos.y, transform.position.z );
        break;
      case Room.CameraType.Static:
        transform.position = new Vector3( Game.currentRoom.transform.position.x, Game.currentRoom.transform.position.y, transform.position.z );
        break;
      case Room.CameraType.HorizontalScroll:
        transform.position = new Vector3( plPos.x, curRoom.YLock, transform.position.z );
        break;
      case Room.CameraType.VerticalScroll:
        transform.position = new Vector3( curRoom.XLock, plPos.y, transform.position.z );
        break;
      default:
        transform.position = new Vector3( plPos.x, plPos.y, transform.position.z );
        break;
    }
    
    // handle character scale here if static
    if( curRoom.characterScaleType == Room.ScaleType.Static ) {
      float cScale = curRoom.characterScale;
      Game.player.transform.localScale = new Vector3( ( Game.player.transform.localScale.x < 0 ? -cScale : cScale ), cScale, cScale );
    }
    
    // Change ambient light
    RenderSettings.ambientLight = curRoom.ambientLight;
    
  }
	
	// Update is called once per frame
	void Update() {    
    if( isPaused ) return;
    
    // handle character scale here if depth
    Room curRoom = Game.currentRoom.GetComponent<Room>();
    if( curRoom.characterScaleType == Room.ScaleType.Depth && !pauseScale ) {
      
      float inter = Mathf.InverseLerp( curRoom.backY, curRoom.frontY, Game.player.transform.position.y );
      float cScale = Mathf.Lerp( curRoom.backScale, curRoom.frontScale, inter );
      
      Game.player.transform.localScale = new Vector3( ( Game.player.transform.localScale.x < 0 ? -cScale : cScale ), cScale, cScale );
    }
    
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
      
      Vector3 diff = playerTrans.position - pos + ( (Vector3)Game.player.gameObject.GetComponent<PolyNavAgent>().movingDirection * lookAhead  );
      
      switch( camType ) {
        case Room.CameraType.HorizontalScroll:
          newPos = pos + ( new Vector3( diff.x, 0f, 0f ) * 0.05f );
          break;
        case Room.CameraType.VerticalScroll:
          newPos = pos + ( new Vector3( 0f, diff.y, 0f ) * 0.05f );
          break;
        case Room.CameraType.FreeScroll:
        default:
          newPos = pos + ( new Vector3( diff.x, diff.y, 0f ) * 0.05f );
          break;
      }
      
      if( CheckBounds( roomBounds, newPos, pos ) ) transform.position = newPos;
    } 
	}
  
  private bool CheckBounds( Bounds roomBounds, Vector3 newPos, Vector3 pos ) {
    Vector3 offset = new Vector2( Screen.width * 0.45f, Screen.height * 0.45f );
    
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
