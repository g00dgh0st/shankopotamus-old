using UnityEngine;
using System.Collections;

public class BasicDoor : MonoBehaviour {
  
  public class BasicDoorObj : Door {
  
    public BasicDoorObj( GameObject d, Transform i, Transform o ) : base( d, i, o ) {}
  
    public override void AnimateIn() {
      Game.player.MoveTo( outBlocking.position );
    }
  
    public override void AnimateOut() {
      Game.player.MoveTo( inBlocking.position );
    }
  }

  public GameObject destDoor;
  public BasicDoorObj door;
  
  private Texture2D cursor;

  public void Awake() {
    Transform inBlock = transform.Find( "inBlocking" );
    Transform outBlock = transform.Find( "outBlocking" );
    
    door = new BasicDoorObj( destDoor, inBlock, outBlock );
    cursor = Resources.Load( "Cursors/cursor_door" ) as Texture2D;
  }

  public void OnMouseOver() {
    Cursor.SetCursor( cursor, Vector2.zero, CursorMode.Auto );
    if( Input.GetMouseButtonDown(0) ) Game.script.StartCoroutine( door.GoIn( destDoor ) );
  }
  
  public void OnMouseExit() {
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
	}
}