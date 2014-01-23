using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class BasicItem : MonoBehaviour {

  public string name;
  public Texture2D cursor;
  public Texture2D image;
  
  private Item item;

	public void Start() {
	  item = new Item( name, image );
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
