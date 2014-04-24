using UnityEngine;
using System.Collections;

public class Bat : MonoBehaviour {
  
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  public Pig pig;
  
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
      // 0
      new Step( camTarget, "Who dares disturb my slumber?",
        new Option[] {
          new Option( "I heard that you know the code for the guard tower door.", 14 ),
          new Option( "The pig said you stole an action figure from him.", 1, delegate() { return pig.wantsSwede && GameObject.Find( "item_action_swede" ) == null; } ),
          new Option( "What are all the scratches on the walls?", 10 )
        }
      ),
      // 1
      new Step( camTarget, "Not just any action figure, a limited edition Action Swede, in mint condition.",
        new Option[] {
          new Option( "Why did you steal it from him?", 2 )
        }
      ),
      // 2
      new Step( camTarget, "You see, I'm an immense fan of Action Swede myself, and I just couldn't bear to see such a pristene figure be destroyed by that porker's fat, greasy fingers. I had to steal it to keep it safe.",
        new Option[] {
          new Option( "He's a pretty big fan himself, I think he'll take good care of it.", 3 ),
          new Option( "Aren't you a little old to be playing with toys", 20 )
        }
      ),
      // 3
      new Step( camTarget, "He's not a bigger fan than me! I've ready every singe graphic novel, I've seen every episode of the TV show. I even know every single one of his 35 catchphrases!",
        new Option[] {
          new Option( "What are some of his catchphrases?", 4 )
        }
      ),
      // 4
      new Step( camTarget, "Well there's the classic, \"Action Swede is here to save the day!\", then there's the lesser known, \"I must save our beloved IKEA!\", and of course, my favorite, \"Stop that black man!\".",
        new Option[] {
          new Option( "You are a pretty big fan. But I told the pig I'd get his toy for him.", 5 )
        }
      ),
      // 5
      new Step( camTarget, "Oh, alright. His fist-punching action isn't as powerful as I'd thought it would be anyway.", 
        new Option[] {
          new Option( "So will you give it back?", 6 )
        }
      ),
      // 6
      new Step( camTarget, "You can have the figure. But first, you must answer a riddle!",
        new Option[] {
          new Option( "What kind of riddle?", 7 )
        }
      ),
      // 7
      new Step( camTarget, "What can you catch, but not throw?", 
        new Option[] {
          new Option( "A bullet.", 9 ),
          new Option( "A single grain of rice.", 9 ),
          new Option( "Hepatitis.", 8 ),
          new Option( "All of the above.", 9 )
        }
      ),
      // 8
      new Step( camTarget, "That is correct! You may take the Action Swede.", delegate() {
        Game.script.AddItem( "action_swede" );
        Game.dialogueManager.StopDialogue();
      }, true ),
      // 9
      new Step( camTarget, "No! You moron, that doesn't even make sense. You can try again.", 7 ),
      // 10
      new Step( camTarget, "I'm just keeping count.",
        new Option[] {
          new Option( "Of what?", 11 )
        }
      ),
      // 11
      new Step( camTarget, "I just like to count.",
        new Option[] {
          new Option( "You're not counting anything?", 12 )
        }
      ),
      // 12
      new Step( camTarget, "Can't a guy just count? Why do all you liberal, pot-smoking, communists think that you have to count things? Why can't we just enjoy counting for counting's sake?",
        new Option[] {
          new Option( "Is this some kind of Sesame Street joke?", 13 )
        }
      ),
      // 13
      new Step( camTarget, "I don't know what that is! Go away!" ),
      // 14
      new Step( camTarget, "Why yes, I know many numbers.",
        new Option[] {
          new Option( "Can you tell me the code?", 15 )
        }
      ),
      // 15
      new Step( camTarget, "No.",
        new Option[] {
          new Option( "Can I trade you something for it?", 16 )
        }
      ),
      // 16
      new Step( camTarget, "No.",
        new Option[] {
          new Option( "Is there nothing I can do to make you give me that code?", 17 )
        }
      ),
      // 17
      new Step( camTarget, "No.",
        new Option[] {
          new Option( "How about a clue?", 18 )
        }
      ),
      // 18
      new Step( camTarget, "Here's a clue...", 19 ),
      // 19
      new Step( camTarget, "GET OUT!" ),
      // 20
      new Step( camTarget, "Well, aren't you a little short for a stormtrooper?!", 
        new Option[] {
          new Option( "What?", 3 )
        }
      )
    } );
  }  
}
