using UnityEngine;
using System.Collections;

public class DialogueGuard : MonoBehaviour {

  public Texture2D cursor;
  private Transform headTrans;
  private Bubble bub;
  
  // public Dialogue dialogue = new Dialogue(
  //   new Step[1] {
  //     
  //     new Step( // begin step 1
  //       "Hello",
  //         new Option[1] {
  //           new Option(
  //             "I don't want to talk to you",
  //             delegate() { Debug.Log( "Hello" ); }
  //           )
  //         }
  //     ) // end step 1
  //   }
  // );
  
  
  
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
