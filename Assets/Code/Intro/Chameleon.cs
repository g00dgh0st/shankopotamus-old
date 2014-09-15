using UnityEngine;
using System.Collections;

public class Chameleon : Clicker {
    
  private Dialogue dialogue;
  public Transform moveTo;
  
  void Start() {
    cursorType = Clicker.CursorType.Chat;
    SetupDialogue();
  }

  void OnClick() {
    Game.player.MoveTo( moveTo.position, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 0 ); } );
  }

  // All dialogue is "written" here
  public void SetupDialogue() {
    Transform camTarget = transform.Find( "CamTarget" );
    
    dialogue = new Dialogue();
    
    dialogue.SetSteps(
    new Step[] {
      // 0
      new Step( camTarget, "Well, hello.",
        new Option[] {
          new Option( "Bye.", -1 ),
          new Option( "Bye.", -1 ),
          new Option( "Bye.", -1 ),
          new Option( "Bye.", -1 ),
          new Option( "Bye.", -1 )
        }
      )
    } );
  }  
}
