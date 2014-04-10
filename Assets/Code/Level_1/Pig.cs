using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour {
  
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
    new Step[12] {
      // 0
      new Step( camTarget, "Hello there, good sir. ",
        new Option[2] {
          new Option( "What's with all the sausage?", 1 ),
          new Option( "Can I have your hat?", 7 )
        }
      ),
      // 1
      new Step( camTarget, "Would you like to try some? I'm testing out a new recipe.",
        new Option[2] {
          new Option( "What's in it?", 2 ),
          new Option( "No thanks.", 6 )
        }
      ),
      // 2
      new Step( camTarget, "Let's see, there's garlic, onions, bay leaves, thyme, parsley, salt, pepper, cayenne, allspice, and, of course, a good helping of ground human.",
        new Option[1] {
          new Option( "Did you say human?", 3 )
        }
      ),
      // 3
      new Step( camTarget, "Yeah human. It's the best meat for sausage. Lot's of fat, so it's real succulent and juicy.",
        new Option[1] {
          new Option( "How do you even get human meat?", 4 )
        }
      ),
      // 4
      new Step( camTarget, "On the outside, it was much easier. I got most of my product through Craigslist. Internet people are so gullible.", 5 ),
      // 5
      new Step( camTarget, "Then one day, some FBI agents set up a sting, and caught me trying to grind up a spicy Puerto Rican guy. And now I'm stuck in here, making sausage out of the sex offenders.",
        new Option[1] {
          new Option( "I think I'll pass on the sausage.", 6 )
        }
      ),
      // 6
      new Step( camTarget, "Your loss. Best sausage you'll ever eat.",
        new Option[1] {
          new Option( "Can I have your hat?", 7 )
        }
      ),
      // 7
      new Step( camTarget, "My fedora? That's my most prominent feature. People look at me and they say \"Look at that hat guy. He's so damn hat!\"",
        new Option[1] {
          new Option( "I don't think they were saying \"hat\".", 8 )
        }
      ),
      // 8
      new Step( camTarget, "I think I could part with my hat, if you got something for me.",
        new Option[1] {
          new Option( "What do you need?", 9 )
        }
      ),
      // 9
      new Step( camTarget, "I need you to get my Action Swede back. I'd do it myself, but my stubby legs can't carry my heftiness.", 
        new Option[2] {
          new Option( "What is an Action Swede?", 10 ),
          new Option( "Where can I find it?", 11 )
        }
      ),
      // 10
      new Step( camTarget, "Action Swede? He's only the coolest, most awesome superhero in all of Scandinavia! I'm his biggest fan, and I paid a lot of money for a limited edition mint condition Action Swede figure.",
        new Option[1] {
          new Option( "What happened to it?", 11 )
        }
      ),
      // 11
      new Step( camTarget, "That bat on the first floor stole it from me. If you get it back for me, I'll let you have my sweet fedora.",
        new Option[1] {
          new Option( "I'll go talk to him.", -1 )
        }
      )
    } );
  }  
}
