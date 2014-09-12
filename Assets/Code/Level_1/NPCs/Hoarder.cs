using UnityEngine;
using System.Collections;

public class Hoarder : Clicker {
  
  private Dialogue dialogue;

  public SewersMaintenanceGuy sewerDude;
  public bool wantsHoney = false;
  public bool wantsRadio = false;
  private bool firstTalk = false;
  
  void Start() {
    cursorType = Clicker.CursorType.Chat;
    SetupDialogue();
  }
  
  void OnItemDrop( string item ) {
    if( wantsHoney && item == "honey" ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        Game.script.ShowSpeechBubble( "Thanks, man. Here's that Pancake Stew I promised you.", transform.parent.Find( "BubTarget" ), 3f );
        Game.script.AddItem( "pancake_stew" );
        wantsHoney = false;
      } );
    } else if( wantsRadio && item == "radio" ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        Game.script.ShowSpeechBubble( "Aww yeah! Here, take this battery from my wazoo.", transform.parent.Find( "BubTarget" ), 3f );
        Game.script.AddItem( "battery" );
        wantsRadio = false;
        Game.script.GetComponent<Level1>().needBattery = false;
      } );
    } else {
      base.OnItemDrop( item );
    }
  }


  void OnClick() {
    if( firstTalk )
      Game.player.MoveTo( movePoint, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 11 ); } );
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
      new Step( camTarget, "Hey man, you wanna buy some potato skins?",
        new Option[] {
          new Option( "Why would I want potato skins?", 1 ),
          new Option( "No thanks. Do you have anything useful?", 4 ),
          new Option( "Why do you have all this stuff?", 8 ),
          new Option( "No, I gotta go.", -1 )
        },
        delegate() { firstTalk = true; }
      ),
      // 1
      new Step( camTarget, "Whaaaat?! Are you serious? These are primo potato skins! Look at 'em!", 
        new Option[] {
          new Option( "What did you do with the rest of the potatoes?", 2 ),
          new Option( "Do you have anything useful?", 4 )
        }
      ),
      // 2
      new Step( camTarget, "Man, I don't think you even ascertain the gravity of this opportunity.", 
        new Option[] {
          new Option( "Are you high?", 3 ),
          new Option( "Do you have anything useful?", 4 )
        }
      ),
      // 3
      new Step( camTarget, "Oh most definitely.", 
        new Option[] {
          new Option( "Do you have anything useful?", 4 ),
          new Option( "Why do you have all this stuff?", 8 ),
          new Option( "I have another quesiton.", 11 ),
          new Option( "No thanks, I gotta go.", -1 )
        }
      ),
      // 4
      new Step( camTarget, "Man, I got anything you could ever need.", 
        new Option[] {
          new Option( "Do you have a deflated basketball signed by Phil Collins?", 5 ),
          new Option( "You got anything I can use to sharpen metal?", 18, delegate() { return GameObject.Find( "item_spoon" ); } ),
          new Option( "I'm looking for a can of Pancake Stew. You got any?", 12, delegate() { return sewerDude.wantsStew && GameObject.Find( "item_pancake_stew" ) == null;  } ),
          new Option( "Do you have any batteries?", 18, delegate(){ return Game.script.GetComponent<Level1>().needBattery; } ),
          new Option( "Do you have any weapons?", 16 ),
          new Option( "Why do you have all this stuff?", 8 )
        }
      ),
      // 5
      new Step( camTarget, "Yup. I also have a soup can that he once threw at a hobo.", 
        new Option[] {
          new Option( "Do you have a can of Surge soda from 1997?", 6 ),
          new Option( "You got anything I can use to sharpen metal?", 18, delegate() { return GameObject.Find( "item_spoon" ); } ),
          new Option( "How about a can of Pancake Stew?", 12, delegate() { return sewerDude.wantsStew && GameObject.Find( "item_pancake_stew" ) == null;  } ),
          new Option( "Do you have any batteries?", 18, delegate(){ return Game.script.GetComponent<Level1>().needBattery; } ),
          new Option( "Do you have any weapons?", 16 ),
          new Option( "Why do you have all this stuff?", 8 )
        }
      ),
      // 6
      new Step( camTarget, "Yeah, but you think I'm gonna let you touch it?", 
        new Option[] {
          new Option( "How about the original Bible?", 7 ),
          new Option( "You got anything I can use to sharpen metal?", 18, delegate() { return GameObject.Find( "item_spoon" ); } ),
          new Option( "You have a can of Pancake Stew?", 12, delegate() { return sewerDude.wantsStew && GameObject.Find( "item_pancake_stew" ) == null;  } ),
          new Option( "Do you have any batteries?", 18, delegate(){ return Game.script.GetComponent<Level1>().needBattery; } ),
          new Option( "Do you have any weapons?", 16 ),
          new Option( "Why do you have all this stuff?", 8 )
        }
      ),
      // 7
      new Step( camTarget, "I got you one better. I have the graphic novel it was based on.", 
        new Option[] {
          new Option( "You got anything I can use to sharpen metal?", 18, delegate() { return GameObject.Find( "item_spoon" ); } ),
          new Option( "Do you have a can of Pancake Stew?", 12, delegate() { return sewerDude.wantsStew && GameObject.Find( "item_pancake_stew" ) == null;  } ),
          new Option( "Do you have any batteries?", 18, delegate(){ return Game.script.GetComponent<Level1>().needBattery; } ),
          new Option( "Do you have any weapons?", 16 ),
          new Option( "Why do you have all this stuff?", 8 ),
          new Option( "I have another question.", 11 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 8
      new Step( camTarget, "On the outside, I was a serial entrepreneur. Business is all I know. It's like a drug for me.",
        new Option[] {
          new Option( "How did you end up here?", 9 ),
          new Option( "I have another question.", 11 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 9
      new Step( camTarget, "I also happened to be a serial killer.", 
        new Option[] {
          new Option( "How did you get caught?", 10 ),
          new Option( "I have another question.", 11 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 10
      new Step( camTarget, "I posted it on social media. #DoItForTheVine.", 
        new Option[] {
          new Option( "I have another question.", 11 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 11
      new Step( camTarget, "Whatchu need? Potato skins?", 
        new Option[] {
          new Option( "You got anything I can use to sharpen metal?", 22, delegate() { return GameObject.Find( "item_spoon" ); } ),
          new Option( "What else do you have?", 4 ),
          new Option( "Why do you have all this stuff?", 8 ),
          new Option( "No, I gotta go.", -1 )
        }
      ),
      // 12
      new Step( camTarget, "I just happen to have the last can in this prison. ", 
        new Option[] {
          new Option( "Can I have it?", 13 )
        }
      ),
      // 13
      new Step( camTarget, "Whatchu think this is? A charity? You gotta trade me something for it.", 
        new Option[] {
          new Option( "What do you need?", 14 ),
          new Option( "I have another question.", 11 ),
          new Option( "Never mind. I gotta go.", -1 )
        }
      ),
      // 14
      new Step( camTarget, "I need you to get me a jar of honey.", 
        new Option[] {
          new Option( "Where can I find that?", 15 ),
          new Option( "I'll go find one.", -1 ),
          new Option( "I have another question.", 11 )
        },
        delegate() { wantsHoney = true; }
      ),
      // 15
      new Step( camTarget, "I don't know, man. I'm really high right now. Did you realize that your eyes are bleeding jelly?", 
        new Option[] {
          new Option( "I'll go look for some honey.", -1 ),
          new Option( "I have another question.", 11 )
        }
      ),
      // 16
      new Step( camTarget, "No. When I was arrested, I decided to become a pacifist. I refuse any form of violence. ", 17 ),
      // 17
      new Step( camTarget, "But if you tell anyone that, I WILL beat yo ass to death.", 
        new Option[] {
          new Option( "I have another question.", 11 ),
          new Option( "Never mind. I gotta go.", -1 )
        }
      ),
      // 18
      new Step( camTarget, "Man, I got batteries comin' out the wazoo. That's what I call the box I keep my batteries in.", 
        new Option[] {
          new Option( "Can I get one?", 19 )
        }
      ),
      // 19
      new Step( camTarget, "Whatchu think this is? Socialism? You gotta trade me something for it.", 
        new Option[] {
          new Option( "What do you need?", 20 ),
          new Option( "I have another question.", 11 ),
          new Option( "Never mind. I gotta go.", -1 )
        }
      ),
      // 20
      new Step( camTarget, "I traded some guy my radio for some drugs, but now I'm out of drugs, and I want my radio back.", 
        new Option[] {
          new Option( "Where can I find it?", 21 ),
          new Option( "I'll go look for it.", -1 ),
          new Option( "I have another question.", 11 )
        },
        delegate() { wantsRadio = true; }
      ),
      // 21
      new Step( camTarget, "Man, I don't think you even ascertain the gravity of how high I am.", 
        new Option[] {
          new Option( "I'll look for your radio.", -1 ),
          new Option( "I have another question.", 11 )
        }
      ),
      // 22
      new Step( camTarget, "I had a metal file, but one of the Guards confiscated it and took it up to the Guard Tower.", 
        new Option[] {
          new Option( "Thanks, I gotta go.", -1 ),
          new Option( "I have another question.", 11 )
        }
      )
    } );
  }  
}
