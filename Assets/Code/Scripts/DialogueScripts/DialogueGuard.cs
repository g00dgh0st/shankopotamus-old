using UnityEngine;
using System.Collections;

public class DialogueGuard : MonoBehaviour {

  public Texture2D cursor;
  private Transform headTrans;
  private Bubble bub;
  
  // Begin Dialogue
  public Dialogue dialogue;

  public void Start() {
    headTrans = gameObject.transform.Find( "HeadTrans" ) as Transform;
  
    SetupDialogue();    
  }
  
  public void OnMouseUp() {
    // if( bub != null ) Game.dialogueManager.ClearBubble( bub );
    // bub = Game.dialogueManager.ShowBubble( "Don't touch me.", headTrans, 5f );
    Game.dialogueManager.StartDialogue( dialogue );
  }
  
  public void OnMouseOver() {
    Cursor.SetCursor( cursor, new Vector2( 10, 10 ), CursorMode.Auto );
  }
  
  public void OnMouseExit() {
		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
	}
  
  
  // All dialogue is "written" here
  public void SetupDialogue() {
    
    Hashtable flags = new Hashtable();
    flags.Add( "flag1", true );
    flags.Add( "flag2", false );
    flags.Add( "flag3", false );
    flags.Add( "flag4", true );
    
    dialogue = new Dialogue( flags );
    
    dialogue.GetFirstStep = delegate() {
      return dialogue.steps[0];
    };
    
    dialogue.SetSteps(
    new Step[2] {
      
        // Step 1
        new Step( "Hello",
          new Option[1] {
            // Option 1
            new Option( "I don't want to talk to you",
              delegate() { Debug.Log( "hi" ); },
              delegate() { return (bool)dialogue.flags["flag1"]; } 
            )
          },
          delegate() { Debug.Log( "Step 1" ); }
        ),
          
        // Step 2
        new Step( "Ok bye.",
          delegate() { Debug.Log( "Exit dialogue" ); } 
        ) 
      } 
    );
  }

}
