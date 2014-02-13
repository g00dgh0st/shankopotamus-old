using UnityEngine;
using System.Collections;

public class FuseBox : ZoomClicker {

  private bool isOpen = false;
  public bool isBroken = false;
  public Sprite openSprite;

  public void Start() {
    base.Start();
    cursor = Resources.Load( "Cursors/cursor_hand" ) as Texture2D;
  }
  
  public void OnClick() {
    if( !isOpen ) {
      isOpen = true;
      cursor = Resources.Load( "Cursors/cursor_eye" ) as Texture2D;
      transform.Find( "OpenBox" ).gameObject.SetActive( true ); 
      transform.Find( "ClosedBox" ).gameObject.SetActive( false ); 
    } else if( !isBroken ){
      base.OnClick();
    }
  }

  
  public void OnHover( bool isOver ) {
    if( isOver && !isBroken )
      Cursor.SetCursor( cursor, new Vector2( 10, 10 ), CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }
}
