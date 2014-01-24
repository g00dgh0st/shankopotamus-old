using UnityEngine;
using System.Collections;

public class Guard2 : MonoBehaviour {

  private Texture2D cursor;
  // speech bubble stuff
  private Transform headTrans;
  private Bubble bub;

  public void Start() {
    headTrans = gameObject.transform.Find( "HeadTrans" ) as Transform;
    cursor = Resources.Load( "Cursors/cursor_chat" ) as Texture2D;
  }
  
  public void OnClick() {
    if( bub != null ) Game.dialogueManager.ClearBubble( bub );
    bub = Game.dialogueManager.ShowBubble( "Don't touch me.", headTrans, 5f );
  }
  
  public void OnHover( bool isOver ) {
    if( isOver )
      Cursor.SetCursor( cursor, new Vector2( 10, 10 ), CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }

}
