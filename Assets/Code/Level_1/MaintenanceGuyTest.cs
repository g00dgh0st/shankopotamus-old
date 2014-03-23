using UnityEngine;
using System.Collections;

public class MaintenanceGuyTest : MonoBehaviour {
  
  private GameObject bub;
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    
    SetupDialogue();
  }


  void OnClick() {
    // if( bub != null ) Destroy( bub );
    // bub = Game.script.ShowSpeechBubble( "Don't touch me.", transform.parent.Find( "BubTarget" ), 5f );
    Game.dialogueManager.StartDialogue( dialogue, 0);
  }

  void OnItemClick() {
    if( bub != null ) Destroy( bub );
    
    if( Game.heldItem.name == "item_cheese" ) {
      Game.script.UseItem();
      bub = Game.script.ShowSpeechBubble( "I love cheese. \n Thanks.", transform.parent.Find( "BubTarget" ), 5f );
    } else
      bub = Game.script.ShowSpeechBubble( "Get that crap away from me.", transform.parent.Find( "BubTarget" ), 5f );
  }
  
  void OnHover( bool isOver ) {
    if( Game.heldItem != null ) return;
    if( isOver ) {
      Game.cursor.GetComponent<CustomCursor>().SetCursor( cursor );
      Game.cursor.SetActive( true );
      Screen.showCursor = false;
    } else {
      Game.cursor.SetActive( false );
      Screen.showCursor = true;
    }
  }
  
  
  // All dialogue is "written" here
  public void SetupDialogue() {
    
    Hashtable flags = new Hashtable();
    
    dialogue = new Dialogue( flags );
    
    dialogue.SetSteps(
    new Step[6] {
      
        // Step 0
        new Step( transform.parent.Find( "CamTarget" ), "Hello hippo weirdo man!",
          new Option[2] {
            new Option( "I don't want to talk to you.", 1 ),
            new Option( "I have to go.", -1 )
          }
        ),
        // Step 1
        new Step( transform.parent.Find( "CamTarget" ), "Ok bye.", delegate() { Game.dialogueManager.StopDialogue(); } ),
        // Step 2
        new Step( transform.parent.Find( "CamTarget" ), "Animation takes a lot of time. And the guy who made this is lazy.",
          new Option[2] {
            new Option( "That's a dumb excuse. ", 3 ),
            new Option( "I need to leave.", -1 )
          }
        ),
        // Step 3
        new Step( transform.parent.Find( "CamTarget" ), "You're a dumb excuse! Now I'm angry!",
          new Option[2] {
            new Option( "Sorry, didn't mean to offend you.", 4 ),
            new Option( "Ok bye.", -1 )
          },
          delegate() { dialogue.flags["isAngry"] = true; }    
        ),
        // Step 4
        new Step( transform.parent.Find( "CamTarget" ), "Well next time, shut up.", delegate() { Game.dialogueManager.StopDialogue(); } ),
        new Step( transform.parent.Find( "CamTarget" ), "You made me angry. Go Away.", delegate() { Game.dialogueManager.StopDialogue(); } )
      } 
    );
  }
}
