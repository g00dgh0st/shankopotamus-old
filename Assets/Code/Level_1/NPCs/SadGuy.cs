using UnityEngine;
using System.Collections;

public class SadGuy : MonoBehaviour {
  
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  public GameObject roger;
  public GameObject roger_block;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    
    SetupDialogue();
  }
  
  void OnItemClick() {
    if( Game.heldItem.name == "item_hat" ) {
      Game.script.UseItem();
      Game.script.ShowSpeechBubble( "Why, thank you! Here, take my glasses. My head shall never be cold again!", transform.parent.Find( "BubTarget" ), 3f );
      Game.script.AddItem( "glasses" );
      transform.parent.Find( "pig_hat" ).gameObject.SetActive( true );
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
    Transform chickenTarget = GameObject.Find( "ChickenTarget" ).transform;
    
    dialogue = new Dialogue();
    
    dialogue.SetSteps(
    new Step[] {
      // 0
      new Step( camTarget, "Look, Roger, we have a visitor!",
        new Option[] {
          new Option( "Who's Roger?", 1 )
        }
      ),
      // 1
      new Step( camTarget, "This is Roger. He's my pet chicken and my best friend. Isn't that right Roger?", 2 ),
      // 2
      new Step( chickenTarget, "cluck.", 3 ),
      // 3
      new Step( camTarget, "Oh, Roger, behave! I apologize, he has has a fowl mouth! Ohoho! A joke!",
        new Option[] {
          new Option( "Why do you have a pet chicken?", 7 ),
          new Option( "Those are nice glasses.", 18 ),
          new Option( "You seem really old, have you been in here a while?", 23 ),
          new Option( "I'll talk to you later.", -1 )
        }
      ),
      // 4
      new Step( camTarget, "Look, Roger, our friend is back!", 5 ),
      // 5
      new Step( chickenTarget, "cluck.", 6 ),
      // 6
      new Step( camTarget, "What would you like to talk about, friend?",
        new Option[] {
          new Option( "Why do you have a pet chicken?", 7 ),
          new Option( "Would you mind if I borrowed Roger for a bit?", 15 ),
          new Option( "Those are nice glasses.", 18 ),
          new Option( "You seem really old, have you been in here a while?", 23 ),
          new Option( "Actually, I need to go.", -1 )
        }
      ),
      // 7
      new Step( camTarget, "Contrary to popular belief, chickens are the most loving and loyal animals.", 8 ),
      // 8
      new Step( chickenTarget, "cluck.", 9 ),
      // 9
      new Step( camTarget, "Oh, I love you too, Roger. Roger has been my best friend for quite some time now. We get into all sorts of hijinks, don't we Roger?", 10 ),
      // 10
      new Step( chickenTarget, "cluck.", 11 ),
      // 11
      new Step( camTarget, "Oh I do remember that, it was one shell of a good time! Ohoho a joke! Because of the eggs, you see!",
        new Option[] {
          new Option( "Can you understand what he says?", 12 )
        }
      ),
      // 12
      new Step( camTarget, "Of course, Roger and I have formed a very powerful bond over the past few years. We can almost read each other's...", 13 ),
      // 13
      new Step( chickenTarget, "cluck.", 14 ),
      // 14
      new Step( camTarget, "See! He knows exactly what I'm going to say!",
        new Option[] {
          new Option( "Would you mind if I borrowed Roger for a bit?", 15 )
        }
      ),
      // 15
      new Step( camTarget, "Hmm, I don't know if I could part with Roger even for a second. What do you think, Roger?", 16 ),
      // 16 
      new Step( chickenTarget, "If I spend another minute with this man, I might kill myself.", 17 ),
      // 17
      new Step( camTarget, "What was that, Roger? I couldn't understand you. Well, I suppose it would be good for Roger to get some fresh air. Just be sure to bring him back!",
        new Option[] {
          new Option( "Yeah, sure.", delegate() { Destroy( roger ); Destroy( roger_block ); Game.script.AddItem( "chicken" ); ; Game.dialogueManager.StopDialogue(); } )
        }
      ),
      // 18
      new Step( camTarget, "Why thank you, I made them myself.",
        new Option[] {
          new Option( "Could you make another pair of glasses?", 19 ),
          new Option( "Can I borrow your glasses for a bit?", 20 )
        }
      ),
      // 19
      new Step( camTarget, "Unfortunately, I don't have the tools anymore. I let someone borrow them, and they never gave them back.",
        new Option[] {
          new Option( "Can I borrow your glasses for a bit?", 20 )
        }
      ),
      // 20
      new Step( camTarget, "Well, I hate to be so forward, but may I propose a trade?",
        new Option[] {
          new Option( "Sure, what do you need?", 21 )
        }
      ),
      // 21
      new Step( camTarget, "This prison can get pretty drafty, and my bald head can get really cold at night. It would be great if I had a nice hat to keep me warm.",
        new Option[] {
          new Option( "Where am I supposed to find a hat?", 22 ),
          new Option( "I'll go get you a hat.", -1 )
        }
      ),
      // 22
      new Step( camTarget, "Maybe go to Hats 'R' Us! Oho! Another joke!",
        new Option[] {
          new Option( "Yeah, uh, funny joke. I'll go find a hat.", -1 )
        }
      ),
      // 23
      new Step( camTarget, "Why, yes, I've been in here for nearly 60 years.",
        new Option[] {
          new Option( "What did you do to get such a long sentence?", 24 )
        }
      ),
      // 24
      new Step( camTarget, "I didn't pay my taxes for 30 years.",
        new Option[] {
          new Option( "That doesn't seem bad enough to have to spend so much time in here.", 25 )
        }
      ),
      // 25
      new Step( camTarget, "I also did a lot of pedophile stuff.",
        new Option[] {
          new Option( "Oh.", -1 )
        }
      )
    } );
  }  
}
