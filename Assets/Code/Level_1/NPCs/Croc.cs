using UnityEngine;
using System.Collections;

public class Croc : MonoBehaviour {
  
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    
    SetupDialogue();
  }

  void OnItemClick() {
    if( Game.heldItem.name == "item_glasses" ) {
      Game.script.UseItem();
      Game.script.ShowSpeechBubble( "Great. Here, take this bottle, and fill it up with some wine.", transform.parent.Find( "BubTarget" ), 3f );
      Game.script.AddItem( "empty_bottle" );
    }
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
      new Step( camTarget, "Hello.",
        new Option[] {
          new Option( "I heard you're the toughest guy in the prison.", 1 ),
          new Option( "You have a lot of books here.", 6 ),
          new Option( "I heard you have some wine?", 7 ),
          new Option( "Bye.", -1 )
        }
      ),
      // 1
      new Step( camTarget, "That may be true. I have allegedly shanked many a chump.",
        new Option[] {
          new Option( "Why have you allegedly shanked so many people?", 2 ),
          new Option( "You have a lot of books here.", 6 ), 
          new Option( "I heard you have some wine?", 7 )
        }
      ),
      // 2 
      new Step( camTarget, "For various reasons, allegedly. Sometimes for money, sometimes in anger. And sometimes just for fun.",
        new Option[] {
          new Option( "So you're a hitman? Allegedly?", 3 ),
          new Option( "You have a lot of books here.", 6 ),
          new Option( "I heard you have some wine?", 7 )
        }
      ),
      // 3
      new Step( camTarget, "One might say that. Then again, one might say that I just enjoy shanking. But that one is probably dead. Allegedly.",
        new Option[] {
          new Option( "Do you have any tips for shanking someone?", 4 ),
          new Option( "You have a lot of books here.", 6 ),
          new Option( "I heard you have some wine?", 7 )
        }
      ),
      // 4
      new Step( camTarget, "Well, I do allegedly have a lot of experience. I would recommend looking around the cafeteria. There's plenty of material there to fashion a solid shank.", 5 ),
      // 5
      new Step( camTarget, "And remember to stretch before the act. You wouldn't want to pull a hammy.",
        new Option[] {
          new Option( "Thanks for the advice. I may allegedly use it sometime.", -1 ),
          new Option( "You have a lot of books here.", 6 ),
          new Option( "I heard you have some wine?", 7 )
        }
      ),
      // 6
      new Step( camTarget, "I enjoy reading. It keeps my mind as sharp as my shanks. Alleged shanks.",
        new Option[] {
          new Option( "I heard you're the toughest guy in the prison", 1 ),
          new Option( "Do you have any wine?", 7 ),
          new Option( "Bye.", -1 )
        }
      ),
      // 7
      new Step( camTarget, "Yes, I make a fine cabernet in my toilet. Would you like a taste?",
        new Option[] {
          new Option( "Could I get a bottle of it?", 8 )
        }
      ),
      // 8
      new Step( camTarget, "Well that will come at a price. My eyes unfortunately aren't what they used to be, and it makes reading all the books quite difficult. If you could go find me some glasses, then I would be happy to part with a bottle of wine.",
        new Option[] {
          new Option( "Any idea where I can find some glasses?", 9 )
        }
      ),
      // 9
      new Step( camTarget, "One of the other inmates must have a pair, you should ask around.",
        new Option[] {
          new Option( "I'll go do that.", -1 )
        }
      )
    } );
  }  
}
