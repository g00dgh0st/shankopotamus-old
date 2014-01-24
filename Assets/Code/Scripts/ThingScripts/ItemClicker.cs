using UnityEngine;
using System.Collections;

///// I DONT LIKE THIS


[RequireComponent (typeof (SpriteRenderer))]
public class ItemClicker : MonoBehaviour {
  
  public GameObject item;

  private Texture2D cursor;

	public void Start() {
    cursor = Resources.Load( "Cursors/cursor_hand" ) as Texture2D;
	}
  
  public void OnClick() {
    Game.inventory.AddItem( item );
    Destroy( gameObject );
  }
  
  public void OnHover( bool isOver ) {
    if( isOver )
      Cursor.SetCursor( cursor, new Vector2( 10, 10 ), CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }
}
