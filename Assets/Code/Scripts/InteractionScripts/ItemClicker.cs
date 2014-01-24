using UnityEngine;
using System.Collections;

// Simple interaction for an object that is a pick up item

public class ItemClicker : MonoBehaviour {
  
  public string itemName;
  private Texture2D cursor;

	public void Start() {
    cursor = Resources.Load( "Cursors/cursor_hand" ) as Texture2D;
	}
  
  public void OnClick() {
    // Game.inventory.AddItem( itemName );
    Destroy( gameObject );
  }
  
  public void OnHover( bool isOver ) {
    if( isOver )
      Cursor.SetCursor( cursor, new Vector2( 10, 10 ), CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }
}
