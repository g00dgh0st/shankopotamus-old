using UnityEngine;
using System.Collections;

public class Croc : Clicker {
  
  private Dialogue dialogue;
  
  public Bear bear;
  
  public bool wantsGlasses = false;
  public bool knowShank = false;
  
  void Start() {
    cursorType = Clicker.CursorType.Chat;
    SetupDialogue();
  }
  
  void OnItemDrop( string item ) {
    if( wantsGlasses && item == "glasses" ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        Game.script.ShowSpeechBubble( "Great. Here, take this bottle, and fill it up with some wine.", transform.parent.Find( "BubTarget" ), 3f );
        Game.script.AddItem( "empty_bottle" );
        wantsGlasses = false;
      } );
    } else if( Game.heldItem != null && item == "icicle" ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        Game.script.ShowSpeechBubble( "Oh I'm dead! There's no animation, but pretend like I'm dead.", transform.parent.Find( "BubTarget" ), 5f ); 
      } );
    } else if( Game.heldItem != null && item == "sharpened_spoon" ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        Game.script.ShowSpeechBubble( "Oh I'm dead! There's no animation, but pretend like I'm dead.", transform.parent.Find( "BubTarget" ), 5f );
      } );
    } else {
      base.OnItemDrop( item );
    }
  }

  void OnClick() {
    if( wantsGlasses )
      Game.player.MoveTo( movePoint, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 15 ); } );
    else
      Game.player.MoveTo( movePoint, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 0 ); } );
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
          new Option( "I heard you make wine.", 10, delegate() { return bear.wantsWine && GameObject.Find( "item_wine_bottle" ) == null && GameObject.Find( "item_empty_bottle" ) == null; } ),
          new Option( "Do you know how I can get a shank in here?", 6, delegate() { return knowShank; } ),
          new Option( "A little bird told me you're the toughest guy in the prison.", 1, delegate() { return !knowShank; }),
          new Option( "A little bird told me you're the toughest guy in the prison.", 3, delegate() { return knowShank; }),
          new Option( "What's with all the books?", 7 ),
          new Option( "Bye.", -1 )
        }
      ),
      // 1
      new Step( camTarget, "Did Steve tell you that? I'll have to make a tiny shank for him.", 
        new Option[] {
          new Option( "Steve?", 2 ),
          new Option( "I heard that you shanked a lot of people.", 3 )
        }
      ),
      // 2
      new Step( camTarget, "He's a bird that talks too much. But yes, many people say that I'm the toughest guy in this prison.", 3 ),
      // 3
      new Step( camTarget, "I have allegedly shanked many a chump.", 
        new Option[] {
          new Option( "Why have you allegedly shanked so many people?", 4 ),
          new Option( "So you're a hitman? Allegedly?", 5 ),
          new Option( "Do you know how I can get a shank in here?", 6 )
        },
        delegate() { knowShank = true; }  
      ),
      // 4
      new Step( camTarget, "For various reasons, allegedly. Sometimes for money, sometimes in anger. And sometimes just for fun.", 
        new Option[] {
          new Option( "So you're a hitman? Allegedly?", 5 ),
          new Option( "Do you know how I can get a shank in here?", 6 )
        }
      ),
      // 5
      new Step( camTarget, "One might say that. Then again, one might say that I just enjoy shanking. But that one is probably dead. Allegedly.", 
        new Option[] {
          new Option( "Do you know how I can get a shank in here?", 6 ),
          new Option( "Let me ask you something else.", 0 ),
          new Option( "I'll see you later", -1 )
        }
      ),
      // 6
      new Step( camTarget, "You can make a shank out of anything, allegedly. The only limit is your imagination.", 
        new Option[] {
          new Option( "Let me ask you something else.", 0 ),
          new Option( "I'll see you later", -1 )
        }
      ),
      // 7
      new Step( camTarget, "I enjoy reading. It keeps my mind as sharp as my shanks. Alleged shanks.", 
        new Option[] {
          new Option( "Have you read all of these?", 8 ),
          new Option( "Let me ask you something else.", 0 ),
          new Option( "I'll see you later", -1 )
        }
      ),
      // 8 
      new Step( camTarget, "Why yes. Many times. I can recite most of these from memory.", 
        new Option[] {
          new Option( "What's your favorite?", 9 ),
          new Option( "Let me ask you something else.", 0 ),
          new Option( "I'll see you later", -1 )
        }
      ),
      // 9
      new Step( camTarget, "My favorite is \"Please Stop, Amelia Bedelia\". Amelia snaps and murders the Rogers. But she does so in a hilariously clumsy way.", 
        new Option[] {
          new Option( "Let me ask you something else.", 0 ),
          new Option( "I'll see you later", -1 )
        }
      ),
      // 10
      new Step( camTarget, "Yes I make a fine cabernet in my toilet. Would you like a taste?", 
        new Option[] {
          new Option( "How do you make it?", 11 ),
          new Option( "Can I get a bottle of it?", 13 )
        }
      ),
      // 11
      new Step( camTarget, "I painstakingly source only the finest, best quality grape juice concentrate from the prison commissary. ", 12 ), 
      // 12
      new Step( camTarget, "And then I dump it into my toilet with sugar and yeast.", 
        new Option[] {
          new Option( "Classy. Can I get a bottle of it?", 13 )
        }
      ),
      // 13
      new Step( camTarget, "You may trade me for it. My eyes unfortunately aren't what they used to be, and it makes reading quite difficult. If you could go find me some glasses, then I would be happy to part with a bottle of wine.",
        new Option[] {
          new Option( "Any idea where I can find some glasses?", 14 ),
          new Option( "I'll go look for some glasses.", -1 ),
          new Option( "Let me ask you something else.", 0 )
        },
        delegate() { wantsGlasses = true; }
      ),
      // 14
      new Step( camTarget, "One of the inmates must have a pair. Ask around.", 
        new Option[] {
          new Option( "I'll go look for some glasses.", -1 ),
          new Option( "Let me ask you something else.", 0 )
        }
      ),
      // 15
      new Step( camTarget, "Were you able to find some glasses?",
        new Option[] {
          new Option( "No, I'll go look for them.", -1 ),
          new Option( "Do you know how I can get a shank in here?", 6, delegate() { return knowShank; } ),
          new Option( "A little bird told me you're the toughest guy in the prison.", 1, delegate() { return !knowShank; }),
          new Option( "A little bird told me you're the toughest guy in the prison.", 3, delegate() { return knowShank; }),
          new Option( "What's with all the books?", 7 )
        }
      )
    } );
  }  
}
