using UnityEngine;
using System.Collections;

public class SewersMaintenanceGuy : MonoBehaviour {
  
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    
    SetupDialogue();
  }


  void OnClick() {
    Game.player.MoveTo( transform.position, delegate() { Game.dialogueManager.StartDialogue( dialogue, 0 ); } );
  }

  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
  
  // All dialogue is "written" here
  public void SetupDialogue() {
    Transform camTarget = transform.parent.Find( "CamTarget" );
    
    dialogue = new Dialogue();
    
    dialogue.SetSteps(
    new Step[] {
      //0
      new Step( camTarget, "Don't touch me.", 
        new Option[] {
          new Option( "What are you doing?", 1 ),
          new Option( "Can I use your ladder?", 9 ),
          new Option( "Can I borrow your fishing rod?", 3 ),
          new Option( "Sorry.", -1 )
        }
      ),
      // 1
      new Step( camTarget, "Fishing.",
        new Option[] {
          new Option( "Shouldn't you be working?", 4 ),
          new Option( "What can you catch down here?", 2 ),
          new Option( "Can I borrow your fishing rod?", 3 ),
          new Option( "Can I use your ladder?", 9 )
        }
      ),
      // 2
      new Step( camTarget, "Fish. Also hepatitis.",
        new Option[] {
          new Option( "Are you sure that fish is safe to eat?", 6 ),
          new Option( "Can I borrow your fishing rod?", 3 ),
          new Option( "Can I use your ladder?", 9 )
        }
      ),
      // 3
      new Step( camTarget, "I'm using it.",
        new Option[] {
          new Option( "What can you catch down here?", 2 ),
          new Option( "Shouldn't you be working?", 4 ),
          new Option( "What about your ladder?", 9 ),
          new Option( "Ok. I'll leave then.", -1 )
        }
      ),
      // 4
      new Step( camTarget, "I'm a maintenance guy. There ain't nothing broken, so there ain't nothing to maintain.",
        new Option[] {
          new Option( "Isn't maintenance more about preventing things from breaking?", 8 ),
          new Option( "What if something broke right now?", 5 )
        }
      ),
      // 5
      new Step( camTarget, "Well then, I guess I would have to go fix it wouldn't I?",
        new Option[] {
          new Option( "Ok, I'm going to go.", -1 )
        }
      ),
      // 6
      new Step( camTarget, "I don't eat them. I just like to watch them plead with their God for another chance at life.",
        new Option[] {
          new Option( "That seems kind of cruel.", 7 )
        }
      ),
      // 7
      new Step( camTarget, "These fish are die-hard \"Toddlers & Tiaras\" fans. They deserve it.",
        new Option[] {
          new Option( "Can I borrow your fishing rod?", 3 ),
          new Option( "Can I use your ladder?", 9 ),
          new Option( "Bye.", -1 )
        }
      ),
      // 8
      new Step( camTarget, "Right now I'm preventing myself from breaking your face. How's that for maintenance?",
        new Option[] {
          new Option( "Can I borrow your fishing rod?", 3 ),
          new Option( "Can I use your ladder?", 9 ),
          new Option( "I think I'll leave you alone.", -1 )
        }
      ),
      // 9
      new Step( camTarget, "I'm using it.",
        new Option[] {
          new Option( "Why don't you sit on something else?", 12 ),
          new Option( "What if I trade you for it?", 10 )
        }
      ),
      // 10
      new Step( camTarget, "I tell you what, you get me a can of Pancake Stew, you can do whatever you want with this ladder.",
        new Option[] {
          new Option( "What's Pancake Stew?", 11 ),
          new Option( "Can I have your fishing rod?", 3 ),
          new Option( "Ok, I'll try to find one.", -1 )
        }
      ),
      // 11
      new Step( camTarget, "That's a stupid question. The prison's been running out of cans lately, so if you can find one, I'll give you the ladder.",
        new Option[] {
          new Option( "Can I have your fishing rod?", 3 ), 
          new Option( "Ok I'll try to find a can.", -1 )
        }
      ),
      // 12
      new Step( camTarget, "How about I sit on your face?", 13 ),
      // 13
      new Step( camTarget, "That didn't come out right.",
        new Option[] {
          new Option( "What if I trade you for it?", 10 )
        }
      )
    } );
  }  
}
