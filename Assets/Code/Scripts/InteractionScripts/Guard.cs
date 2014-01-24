using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

  
  private DialogueManager DM;
  
  private Texture2D cursor;
  private Bubble bub;
  private Transform bubbleTrans;

  // Begin Dialogue
  public Dialogue dialogue;

  public void Start() {
    DM = Game.dialogueManager;
    
    bubbleTrans = transform.Find("BubbleTrans");
  
    SetupDialogue();    
    
    cursor = Resources.Load( "Cursors/cursor_chat" ) as Texture2D;
  }
  
  public void OnClick() {
    if( (bool)dialogue.flags["isAngry"] ) {
      if( bub != null ) Game.dialogueManager.ClearBubble( bub );
      bub = Game.dialogueManager.ShowBubble( "You made me angry. Go away.", bubbleTrans, 5f );
    } else
      Game.dialogueManager.StartDialogue( dialogue );
    
  }
  
  public void OnHover( bool isOver ) {
    if( isOver )
      Cursor.SetCursor( cursor, new Vector2( 10, 10 ), CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }
  
  // All dialogue is "written" here
  public void SetupDialogue() {
    
    Hashtable flags = new Hashtable();
    flags.Add( "isAngry", false );
    
    dialogue = new Dialogue( flags, gameObject );
    
    dialogue.StartDialogue = delegate() {
      if( (bool)dialogue.flags["isAngry"] )
        return dialogue.steps[5];
      else
        return dialogue.steps[0];
    };
    
    dialogue.SetSteps(
    new Step[6] {
      
        // Step 0
        new Step( "Hello.",
          new Option[3] {
            new Option( "I don't want to talk to you.", 1 ),
            new Option( "How come you aren't animated?", 2, delegate() { return !(bool)dialogue.flags["isAngry"]; } ),
            new Option( "I have to go.", -1 )
          }
        ),
        // Step 1
        new Step( "Ok bye.", delegate() { DM.StopDialogue(); } ),
        // Step 2
        new Step( "Animation takes a lot of time. And the guy who made this is lazy.",
          new Option[2] {
            new Option( "That's a dumb excuse. ", 3 ),
            new Option( "I need to leave.", -1 )
          }
        ),
        // Step 3
        new Step( "You're a dumb excuse! Now I'm angry!",
          new Option[2] {
            new Option( "Sorry, didn't mean to offend you.", 4 ),
            new Option( "Ok bye.", -1 )
          },
          delegate() { dialogue.flags["isAngry"] = true; }    
        ),
        // Step 4
        new Step( "Well next time, shut up.", delegate() { DM.StopDialogue(); } ),
        new Step( "You made me angry. Go Away.", delegate() { DM.StopDialogue(); } )
      } 
    );
  }
}
