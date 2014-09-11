using UnityEngine;
using System.Collections;

public class GuardTowerGuard : Clicker {
  
  private Dialogue dialogue;
  
  public Door exitDoor;
  
  public bool firstTime = true;
  
  public bool atScreens = false;
  
  void Start() {
    cursorType = Clicker.CursorType.Chat;
    SetupDialogue();
  }

  public void OnClick() {
    if( atScreens ) {
      Game.script.ShowSpeechBubble( "Dont't touch me, I'm busy.", transform.parent.Find( "BubTarget" ), 2f );
      return;
    }
    if( firstTime )
      Game.player.MoveTo( movePoint, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 0 ); } );
    else
      Game.player.MoveTo( movePoint, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 2 ); } );
  }

  public void DontTouchBubble() {
    Game.script.ShowSpeechBubble( "Dont't touch that!", transform.parent.Find( "BubTarget" ), 1f );
  }
  
  // All dialogue is "written" here
  public void SetupDialogue() {
    Transform camTarget = transform.parent.Find( "CamTarget" );
    
    dialogue = new Dialogue();
    
    dialogue.SetSteps(
    new Step[] {
      // 0
      new Step( camTarget, "How did you get in here?! This is for authorized personnel only!",
        new Option[] {
          new Option( "Is this not the bathroom?", 6 ),
          new Option( "If I'm in here, then I'm probably authorized.", 1 ),
          new Option( "I'll just be going now.", delegate() { Game.dialogueManager.StopDialogue(); exitDoor.OnClick(); } )
        }
      ),
      // 1
      new Step( camTarget, "Huh, I guess you're right.",
        new Option[] {
          new Option( "What are all those screens for?", 3 ),
          new Option( "What's in the box?", 4 ),
          new Option( "Can I borrow that bottle of Diet Cola?", 5 ),
          new Option( "What are you doing?", 7 ),
          new Option( "I gotta go.", -1 )
        },
        delegate() { firstTime = false; }  
      ),
      // 2 
      new Step( camTarget, "Don't touch me.",
        new Option[] {
          new Option( "What are all those screens for?", 3 ),
          new Option( "What's in the box?", 4 ),
          new Option( "Can I borrow that bottle of Diet Cola?", 5 ),
          new Option( "What are you doing?", 7 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 3
      new Step( camTarget, "Don't touch them. It's the security camera feed.", 
        new Option[] {
          new Option( "What's in the box?", 4 ),
          new Option( "Can I borrow that bottle of Diet Cola?", 5 ),
          new Option( "What are you doing?", 7 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 4
      new Step( camTarget, "Don't touch it. It's got confiscated items in it.", 
        new Option[] {
          new Option( "What are all those screens for?", 3 ),
          new Option( "Can I borrow that bottle of Diet Cola?", 5 ),
          new Option( "What are you doing?", 7 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 5
      new Step( camTarget, "Don't touch it. Criminals like you don't deserve to enjoy the crisp, refreshing taste of Diet Cola.", 
        new Option[] {
          new Option( "What are all those screens for?", 3 ),
          new Option( "What's in the box?", 4 ),
          new Option( "What are you doing?", 7 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 6
      new Step( camTarget, "Get out!", delegate() { Game.dialogueManager.StopDialogue(); exitDoor.OnClick(); Game.script.ShowSpeechBubble( "Yup.", Game.player.transform.Find( "BubTarget" ), 0.5f ); }, true ),
      // 7
      new Step( camTarget, "Don't touch me. I'm working on my dance moves.", 
        new Option[] {
          new Option( "What are all those screens for?", 3 ),
          new Option( "What's in the box?", 4 ),
          new Option( "Can I borrow that bottle of Diet Cola?", 5 ),
          new Option( "I gotta go.", -1 )
        }
      )
    } );
  }  
}
