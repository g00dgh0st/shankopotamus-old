using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour {
  
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  public SadGuy sadguy;
  
  public bool wantsSwede = false;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    
    SetupDialogue();
  }
  
  void OnItemClick() {
    if( Game.heldItem.name == "item_action_swede" && wantsSwede ) {
      Game.script.UseItem();
      Game.script.ShowSpeechBubble( "Thanks! Here, take my hat.", transform.parent.Find( "BubTarget" ), 3f );
      Game.script.AddItem( "hat" );
      transform.parent.Find( "pig_hat" ).gameObject.SetActive( false );
      wantsSwede = false;
    }
  }


  void OnClick() {
    if( wantsSwede )
      Game.player.MoveTo( transform.position, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 12 ); } );
    else
      Game.player.MoveTo( transform.position, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 0 ); } );
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
      new Step( camTarget, "Hello there, good sir. ",
        new Option[] {
          new Option( "What's with all the sausage?", 1 ),
          new Option( "Can I have your hat?", 7, delegate() { return sadguy.wantsHat && GameObject.Find( "item_hat" ) == null; } ),
          new Option( "Bye.", -1 )
        }
      ),
      // 1
      new Step( camTarget, "Would you like to try some? I'm testing out a new recipe.",
        new Option[] {
          new Option( "What's in it?", 2 ),
          new Option( "No thanks.", 6 )
        }
      ),
      // 2
      new Step( camTarget, "Let's see, there's garlic, onions, bay leaves, thyme, parsley, salt, pepper, cayenne, allspice, and, of course, a good helping of ground human.",
        new Option[] {
          new Option( "Did you say human?", 3 )
        }
      ),
      // 3
      new Step( camTarget, "Yeah human. It's the best meat for sausage. Lot's of fat, so it's real succulent and juicy.",
        new Option[] {
          new Option( "How do you even get human meat?", 4 )
        }
      ),
      // 4
      new Step( camTarget, "On the outside, it was much easier. I got most of my product through Craigslist. Internet people are so gullible.", 5 ),
      // 5
      new Step( camTarget, "Then one day, some FBI agents set up a sting, and caught me trying to grind up a spicy Puerto Rican guy. And now I'm stuck in here, making sausage out of the sex offenders.",
        new Option[] {
          new Option( "I think I'll pass on the sausage.", 6 )
        }
      ),
      // 6
      new Step( camTarget, "Your loss. Best sausage you'll ever eat.",
        new Option[] {
          new Option( "Can I have your hat?", 7, delegate() { return sadguy.wantsHat && GameObject.Find( "item_hat" ) == null; } ),
          new Option( "See you later.", -1 )
        }
      ),
      // 7
      new Step( camTarget, "My fedora? That's my most prominent feature. People look at me and they say \"Look at that hat guy. He's so damn hat!\"",
        new Option[] {
          new Option( "I don't think they were saying \"hat\".", 8 )
        }
      ),
      // 8
      new Step( camTarget, "I think I could part with my hat, if you got something for me.",
        new Option[] {
          new Option( "What do you need?", 9 )
        }
      ),
      // 9
      new Step( camTarget, "I need you to get my Action Swede back. I'd do it myself, but my stubby legs can't carry my heftiness.", 
        new Option[] {
          new Option( "What is an Action Swede?", 10 ),
          new Option( "Where can I find it?", 11 )
        },
        delegate() { wantsSwede = true; }   
      ),
      // 10
      new Step( camTarget, "Action Swede? He's only the coolest, most awesome superhero in all of Scandinavia! I'm his biggest fan, and I paid a lot of money for a limited edition mint condition Action Swede figure.",
        new Option[] {
          new Option( "What happened to it?", 11 ),
          new Option( "I'll look for it.", -1 )
        }
      ),
      // 11
      new Step( camTarget, "That bat on the first floor stole it from me. If you get it back for me, I'll let you have my sweet fedora.",
        new Option[] {
          new Option( "I'll go talk to him.", -1 )
        }
      ),
      // 12
      new Step( camTarget, "Did you get my Action Swede back?",
        new Option[] {
          new Option( "No, I'll be right back", -1 ),
          new Option( "What's with all the sausage?", 1 ),
          new Option( "Can I have your hat?", 7, delegate() { return sadguy.wantsHat && GameObject.Find( "item_hat" ) == null; } )
        }
      ),
    } );
  }  
}
