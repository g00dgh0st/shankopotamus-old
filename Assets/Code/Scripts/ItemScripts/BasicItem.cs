using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class BasicItem : MonoBehaviour {

  public string name;
  public Texture2D cursor;
  
  private Item item;

	public void Start() {
    Texture2D tex = ( (SpriteRenderer)gameObject.GetComponent( "SpriteRenderer" ) ).sprite as Texture2D;
    if( tex == null ) Debug.LogError( "Add a sprite to this item." );
    
	  item = new Item( name, tex );
	}
  
  public void OnMouseUp() {
    Game.inventory.AddItem( item );
    Destroy( gameObject );
  }
  
  public void OnMouseOver() {
    Cursor.SetCursor( cursor, new Vector2( 10, 10 ), CursorMode.Auto );
  }
  
  public void OnMouseExit() {
		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
	}
}
