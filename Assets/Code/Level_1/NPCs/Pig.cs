using UnityEngine;
using System.Collections;

public class Pig : Clicker {
  
  private Dialogue dialogue;
  
  public SadGuy sadguy;
  
  public bool wantsSwede = false;
  public bool firstTalk = false;
  
  void Start() {
    cursorType = Clicker.CursorType.Chat;
    SetupDialogue();
  }
  
  void OnItemDrop( string item ) {
    if( item == "action_swede" && wantsSwede ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        Game.script.ShowSpeechBubble( "Thanks! Here, take my fat.", transform.parent.Find( "BubTarget" ), 3f );
        Game.script.AddItem( "hat" );
        transform.parent.Find( "pig_hat" ).gameObject.SetActive( false );
        wantsSwede = false;
      } );
    } else {
      base.OnItemDrop( item );
    }
  }

  void OnClick() {
    if( firstTalk && !wantsSwede )
      Game.player.MoveTo( movePoint, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 3 ); } );
    else if( wantsSwede )
      Game.player.MoveTo( movePoint, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 17 ); } );
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
      new Step( camTarget, ". . .",
        new Option[] {
          new Option( "Hello?", 1 ),
          new Option( "Good talk. Bye.", -1 )
        }
      ),
      // 1
      new Step( camTarget, "Hey, you got any bacon?", 
        new Option[] {
          new Option( "No.", 3 ),
          new Option( "Should you be eating bacon?", 2 )
        },
        delegate() { firstTalk = true; }  
      ),
      // 2
      new Step( camTarget, "Why, 'cause I'm Jewish?", 
        new Option[] {
          new Option( "Never mind. I have a question.", 3 ),
          new Option( "Imma go now.", -1 )
        }
      ),
      // 3
      new Step( camTarget, "How can I...uh...do for you?", 
        new Option[] {
          new Option( "That's a nice hat. Can I have it?", 13, delegate() { return !wantsSwede && sadguy.wantsHat && GameObject.Find( "item_hat" ) == null; } ),
          new Option( "Where can I find your Action Swede?", 17, delegate() { return wantsSwede; } ),
          new Option( "Is that a super powered toilet?", 10 ),
          new Option( "What's with all the sausage?", 4 ),
          new Option( "Bye.", -1 )
        }
      ),
      // 4
      new Step( camTarget, "What sausage? Oh that sausage. I make it myself. Wanna try some? ", 
        new Option[] {
          new Option( "Are they pork sausage?", 5 ),
          new Option( "Sure.", 8 )
        }
      ),
      // 5
      new Step( camTarget, "Pork? I don't eat pork, I'm Jewish. They're made out of human.", 
        new Option[] {
          new Option( "That sounds horrible.", 6 ),
          new Option( "Where do you get human meat?", 7 )
        }
      ),
      // 6
      new Step( camTarget, "On the contr...mary. Human meat is the best for sausage. Lots of fat.", 
        new Option[] {
          new Option( "Where do you get human meat?", 7 ),
          new Option( "Well, I'm sold. I'll try a piece.", 8 )
        }
      ),
      // 7
      new Step( camTarget, "Craigslist.", 
        new Option[] {
          new Option( "Well, I'm sold. I'll try a piece.", 8 ),
          new Option( "I have another question.", 3 ),
          new Option( "I'll see you later.", -1 )
        }
      ),
      // 8
      new Step( camTarget, "That'll be $500.", 
        new Option[] {
          new Option( "I don't have that much money.", 9 ),
          new Option( "Never mind. I have another question.", 3 ),
          new Option( "I'll see you later.", -1 )
        }
      ),
      // 9
      new Step( camTarget, "Oh. You got any bacon?", 
        new Option[] {
          new Option( "Never mind. I have another question.", 3 ),
          new Option( "I'll see you later.", -1 )
        }
      ),
      // 10
      new Step( camTarget, "Yup. It's a Big Ass\u2122 toilet. It uses nuc...mlear power to flush my Big Ass\u2122 dumps. It uses some kinda wormhole technology.", 
        new Option[] {
          new Option( "Isn't that kind of dangerous?", 11 ),
          new Option( "Maybe you should try a diet.", 12 )
        }
      ),
      // 11
      new Step( camTarget, "It's only at 50% power. What's really dangerous is my cholest...merol level.", 
        new Option[] {
          new Option( "Maybe you should try a diet.", 12 )
        }
      ),
      // 12
      new Step( camTarget, "What is that? Does it taste good?", 
        new Option[] {
          new Option( "Never mind. I have another question.", 3 ),
          new Option( "I'll see you later.", -1 )
        }
      ),
      // 13
      new Step( camTarget, "My hat is my most noticable feature. Everyone always says, \"Look at that hat guy, he's so damn hat!\"", 
        new Option[] {
          new Option( "What if I trade you for it?", 14 )
        }
      ),
      // 14
      new Step( camTarget, "I'll tell you what. Someone stole my Action Swede action fig...mure. Bring it back, and you can take the hat.", 
        new Option[] {
          new Option( "What is an Action Swede?", 15 ),
          new Option( "Where can I find it?", 16 ),
          new Option( "I'll go find it.", -1 ),
          new Option( "I have another question.", 3 )
        },
        delegate() { wantsSwede = true; }
      ),
      // 15
      new Step( camTarget, "He's only the best superhero of all ever. That figure was my most prized possess...uh...thing.", 
        new Option[] {
          new Option( "Where can I find it?", 16 ),
          new Option( "I'll go find it.", -1 ),
          new Option( "I have another question.", 3 )
        }
      ),
      // 16
      new Step( camTarget, "I'm pretty sure that Bat guy stole it from me. Mainly because I saw him do it. But you never know.", 
        new Option[] {
          new Option( "I'll go find it.", -1 ),
          new Option( "I have another question.", 3 )
        }
      ),
      // 17
      new Step( camTarget, "You got my...uh...Action Swebe?", 
        new Option[] {
          new Option( "No, I'll go get it.", -1 ),
          new Option( "No, but I have another question.", 3 )
        }
      )
    } );
  }  
}
