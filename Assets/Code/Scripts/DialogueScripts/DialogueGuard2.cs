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
  
  public void OnClick() {
    if( bub != null ) Game.dialogueManager.ClearBubble( bub );
    
    if( Game.inventory.currentItem != null && Game.inventory.currentItem.name == "CakeBot" ) {
      bub = Game.dialogueManager.ShowBubble( "Thanks, I've always wanted a robot", headTrans, 5f );
      Game.inventory.RemoveItem( Game.inventory.currentItem );
    } else
      bub = Game.dialogueManager.ShowBubble( "Don't touch me.", headTrans, 5f );
  }
  
  public void OnHover( bool isOver ) {
    if( isOver )
      Cursor.SetCursor( cursor, new Vector2( 10, 10 ), CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }

}
