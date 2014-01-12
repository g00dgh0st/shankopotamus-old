using UnityEngine;
using System.Collections;

public class DialogueGuard : MonoBehaviour {

  public Texture2D cursor;
  private Transform headTrans;
  private Bubble bub;

  public void Start() {
    headTrans = gameObject.transform.Find( "HeadTrans" ) as Transform;
  }
  
  public void OnMouseUp() {
    if( bub != null ) Game.dialogue.ClearBubble( bub );
    bub = Game.dialogue.ShowBubble( "Hello, mang.", headTrans, 5f );
  }
  
  public void OnMouseOver() {
    Cursor.SetCursor( cursor, Vector2.zero, CursorMode.Auto );
  }
  
  public void OnMouseExit() {
		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
	}
}
