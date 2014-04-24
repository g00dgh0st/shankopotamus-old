using UnityEngine;
using System.Collections;

public class Hoarder : MonoBehaviour {
  
  private Sprite cursor;
  private Dialogue dialogue;

  public SewersMaintenanceGuy sewerDude;
  public bool wantsHoney = false;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    
    SetupDialogue();
  }
  
  void OnItemClick() {
    if( wantsHoney && Game.heldItem.name == "item_honey" ) {
      Game.script.UseItem();
      Game.script.ShowSpeechBubble( "Thanks, guy. Here's that Pancake Stew I promised you.", transform.parent.Find( "BubTarget" ), 3f );
      Game.script.AddItem( "pancake_stew" );
      wantsHoney = false;
    }
  }


  void OnClick() {
    if( wantsHoney )
      Game.player.MoveTo( transform.position, delegate() { Game.dialogueManager.StartDialogue( dialogue, 15 ); } );
    else
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
      new Step( camTarget, "Hey, guy, you want some potato skins?",
        new Option[] {
          new Option( "Not really.", 1 ),
          new Option( "Do you have any Pancake Stew?", 5, delegate() { return sewerDude.wantsStew && GameObject.Find( "item_pancake_stew" ) == null;  } ),
          new Option( "Why do you have so much stuff?", 9 ),
          new Option( "Do you have anything useful?", 3 )
        }
      ),
      // 1
      new Step( camTarget, "These are top quality. Peeled 'em myself. You're missing out on some good product here.",
        new Option[] {
          new Option( "What did you you with the rest of the potatoes?", 2 ),
          new Option( "Do you have anything else?", 3 )
        }
      ),
      // 2 
      new Step( camTarget, "That's irrelevant. I'm talking about primo potato skins here.", 
        new Option[] {
          new Option( "I think I'll pass, What else do you have?", 3 ),
          new Option( "Why do you have so much stuff?", 9 )
        }
      ),
      // 3
      new Step( camTarget, "I got pretty much anything you need. You name it, I got it.",
        new Option[] {
          new Option( "Do you have a deflated basketball signed by Phill Collins?", 4 ),
          new Option( "Do you have any Pancake Stew?", 5, delegate() { return sewerDude.wantsStew && GameObject.Find( "item_pancake_stew" ) == null; } ),
          new Option( "Why do you have so much stuff?", 9 )
        }
      ),
      // 4
      new Step( camTarget, "Yeah, but it's not for sale.",
        new Option[] {
          new Option( "How about a sweater knitted from the beard hairs of a homeless guy from Euguene, Oregon?", 12 ),
          new Option( "Do you have any Pancake Stew?", 5, delegate() { return sewerDude.wantsStew && GameObject.Find( "item_pancake_stew" ) == null; } ),
          new Option( "Why do you have so much stuff?", 9 )
        }
      ),
      // 5
      new Step( camTarget, "I just happen to have the last can in this prison.",
        new Option[] {
          new Option( "Can I have it?", 6 )
        }
      ),
      // 6
      new Step( camTarget, "Sure you can. But I'll need you to trade me something. A jar of honey.",
        new Option[] {
          new Option( "Just a jar of honey?",  7 )
        }
      ),
      // 7
      new Step( camTarget, "Not just a jar of honey. Russian honey. That bear on the first floor has the best honey I ever tasted. But he won't trade it to me for some reason.",
        new Option[] {
          new Option( "How do you expect me to get any?", 8 ),
          new Option( "I'll try to get some.", -1 )
        },
        delegate() { wantsHoney = true; }    
      ),
      // 8
      new Step( camTarget, "Not my problem. But if you want a can of Pancake Stew, you'll get me a jar of honey.",
        new Option[] {
          new Option( "I'll see what I can do.", -1 )
        }
      ),
      // 9
      new Step( camTarget, "On the outside, I was a serial entrepreneur. Business is all I know. It's like a drug for me.",
        new Option[] {
          new Option( "How did you end up here?", 10 ),
          new Option( "What do you have?", 3 )
        }
      ),
      // 10
      new Step( camTarget, "I also happened to be a serial killer.",
        new Option[] {
          new Option( "What kind of stuff do you have?", 3 ),
          new Option( "How did you get caught?", 11 )        
        }
      ),
      // 11
      new Step( camTarget, "I forgot to hide the location on my Instagram posts.",
        new Option[] {
          new Option( "What kind of stuff do you have?", 3 ),
          new Option( "I need to go.", -1 )        
        }
      ),
      // 12
      new Step( camTarget, "I got seven. He also makes hats.",
        new Option[] {
          new Option( "What about a can of Surge soda?", 13 ),
          new Option( "Do you have any Pancake Stew?", 5, delegate() { return sewerDude.wantsStew && GameObject.Find( "item_pancake_stew" ) == null; } ),
          new Option( "Why do you have so much stuff?", 9 )
        }
      ),
      // 13
      new Step( camTarget, "Sadly, no. That is my holy grail. The greatest soda ever made. Would that I could feel its electrifying caress on my lips just one last time. Alas, that is but a fevered dream of a hopeless madman.", 14 ),
      // 14
      new Step( camTarget, "So you want the potato skins or what?",
        new Option[] {
          new Option( "Do you have any Pancake Stew?", 5, delegate() { return sewerDude.wantsStew && GameObject.Find( "item_pancake_stew" ) == null; } ),
          new Option( "No thanks. I'll talk to you later.", -1 )
        }
      ),
      // 15
      new Step( camTarget, "You got that honey?",
        new Option[] {
          new Option( "I'll go get some.", -1 ),
          new Option( "Why do you have so much stuff?", 9 ),
          new Option( "Do you have anything useful?", 3 )
        }
      )
    } );
  }  
}
