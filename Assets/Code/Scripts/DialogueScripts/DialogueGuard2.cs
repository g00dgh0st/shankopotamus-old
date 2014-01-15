using UnityEngine;
using System.Collections;

public class DialogueGuard2 : MonoBehaviour {

  public Texture2D cursor;
  // speech bubble stuff
  private Transform headTrans;
  private Bubble bub;

  public void Start() {
    headTrans = gameObject.transform.Find( "HeadTrans" ) as Transform;
  }
  
  public void OnMouseUp() {
    if( bub != null ) Game.dialogueManager.ClearBubble( bub );
    bub = Game.dialogueManager.ShowBubble( "Don't touch me.", headTrans, 5f );
  }
  
  public void OnMouseOver() {
    Cursor.SetCursor( cursor, new Vector2( 10, 10 ), CursorMode.Auto );
  }
  
  public void OnMouseExit() {
		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
	}

}
