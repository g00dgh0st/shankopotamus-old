using UnityEngine;
using System.Collections;

public class GuardTowerGuard : MonoBehaviour {
  
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  public Door exitDoor;
  
  public bool firstTime = true;
  
  public bool atScreens = false;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    
    SetupDialogue();
  }

  public void OnClick() {
    if( firstTime )
      Game.player.MoveTo( transform.position, delegate() { Game.dialogueManager.StartDialogue( dialogue, 0 ); } );
    else
      Game.player.MoveTo( transform.position, delegate() { Game.dialogueManager.StartDialogue( dialogue, 2 ); } );
  }

  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
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
          new Option( "Is this not the bathroom?", 8 ),
          new Option( "If I'm in here, then I'm probably authorized.", 1 ),
          new Option( "I'll just be going now.", delegate() { Game.dialogueManager.StopDialogue(); exitDoor.OnClick(); } )
        }
      ),
      // 1
      new Step( camTarget, "Huh, I guess you're right.",
        new Option[] {
          new Option( "What are all those screens for?", 3 ),
          new Option( "Can I borrow your radio?", 7 ),
          new Option( "I gotta go.", -1 )
        },
        delegate() { firstTime = false; }  
      ),
      // 2 
      new Step( camTarget, "Don't touch me.",
        new Option[] {
          new Option( "What are all those screens for?", 3 ),
          new Option( "Can I borrow your radio?", 7 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 3
      new Step( camTarget, "Don't touch them! My job depends on those screens.",
        new Option[] {
          new Option( "Why are they so important?", 4 ),
          new Option( "You don't seem to be paying much attention to them now.", 6 ),
        }
      ),
      // 4
      new Step( camTarget, "That's the security camera feed. I'm already on thin ice after using one of the screens to marathon through every \"House of Cards\" episode.", 5 ),
      // 5
      new Step( camTarget, "If those screens were to go down now, I'd be fired. Out of a cannon. And then I'd lose my job.",
        new Option[] {
          new Option( "You don't seem to be paying much attention to them now.", 6 ),
          new Option( "Can I borrow your radio?", 7 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 6
      new Step( camTarget, "I don't have to pay attention to them, I just need to make sure they stay up. Now go away!", 
        new Option[] {
          new Option( "Can I borrow your radio?", 7 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 7
      new Step( camTarget, "Can't you see I'm using it? I'm trying to listen to the same 20 songs over and over again. Leave me alone!",
        new Option[] {
          new Option( "What are all those screens for?", 3 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 8
      new Step( camTarget, "Get out!", delegate() { Game.dialogueManager.StopDialogue(); exitDoor.OnClick(); Game.script.ShowSpeechBubble( "Yup.", Game.player.transform.Find( "BubTarget" ), 0.5f ); }, true )
    } );
  }  
}
