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
    
    Option[] firstOpts = new Option[] {
      new Option( "Who are you?", 1 ),
      new Option( "What up?", 1 ),
      new Option( "You talkin' to me?", 1 )
    };
    
    for( int t=0; t < firstOpts.Length; t++ ) {
      Option tmp = firstOpts[t];
      int r = Random.Range( t, firstOpts.Length );
      firstOpts[t] = firstOpts[r];
      firstOpts[r] = tmp;
    }
    
    dialogue.SetSteps(
    new Step[] {
      // 0
      new Step( camTarget, "Well, hello.", firstOpts ),
      new Step( camTarget, "There's gonna be some meaningful dialogue here. But it's not here now.", delegate() { Application.LoadLevel( 2 ); }, true )
    } );
  }  
}