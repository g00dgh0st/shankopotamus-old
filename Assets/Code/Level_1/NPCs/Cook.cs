using UnityEngine;
using System.Collections;

public class Cook : Clicker {
  
  public bool threeMeatOpen = false;
  public bool wantsIngredients = false;
  
  public bool hasChicken = false;
  public bool hasHam = false;
  public bool hasRat = false;
  
  public bool knowSwede = false;
  public bool wantsCola = false;
  
  private Dialogue dialogue;
  
  void Start() {
    cursorType = Clicker.CursorType.Chat;
    SetupDialogue();
  }


  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 0 ); } );
  }

  void OnItemDrop( string item ) {
    if( wantsCola && item == "diet_cola" ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        Game.script.ShowSpeechBubble( "Thanks, kid. Here, take the toy.", transform.parent.Find( "BubTarget" ), 5f );
        Game.script.AddItem( "action_swede" );
        wantsCola = false;
      });
    } else if( wantsIngredients && ( item == "ham" || item == "rat" || item == "chicken" ) ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        if( item == "ham" ) {
          hasHam = true;
          Game.script.ShowSpeechBubble( "That's a fine looking ham.", transform.parent.Find( "BubTarget" ), 5f );
        } else if( item == "chicken" ) {
          hasChicken = true;
          Game.script.ShowSpeechBubble( "Are you sure this is chicken? Whatever, it's good enough.", transform.parent.Find( "BubTarget" ), 5f );
        } else if( item == "rat" ) {
          hasRat = true;
          Game.script.ShowSpeechBubble( "Where'd you find this, the sewer?. That's good, it adds more flavor.", transform.parent.Find( "BubTarget" ), 5f );
        }
    
        if( hasRat && hasChicken && hasHam ) {
          Game.script.ShowSpeechBubble( "That's all the ingredients. Here's my world famous \"Three Meat Surprise\".", transform.parent.Find( "BubTarget" ), 5f );
          Game.script.AddItem( "three_meat_surprise" );
          wantsIngredients = false;
        }
      });
    } else {
      base.OnItemDrop( item );
    }
  }
  
  
  // All dialogue is "written" here
  public void SetupDialogue() {
    Transform camTarget = transform.parent.Find( "CamTarget" );
    
    dialogue = new Dialogue();
    
    dialogue.SetSteps(
    new Step[] {
      // 0
      new Step( camTarget, "What do you want?", 
        new Option[] {
          new Option( "What kind of food do you have?", 1 ),
          new Option( "A little bird told me your specialty is Three Meat Surprise.", 6, delegate() { return threeMeatOpen && !wantsIngredients; } ),
          new Option( "The Bat said you have the Action Swede figure.", 18, delegate() { return knowSwede && !wantsCola; } ),
          new Option( "Where can I find that Diet Cola?", 23, delegate() { return wantsCola; } ),
          new Option( "What ingredients do you need for the Three Meat Surprise?", 13, delegate() { return wantsIngredients; } ),
          new Option( "Nothing.", -1 )
        }
      ),
      // 1
      new Step( camTarget, "Uh, let's see here. I got some meatloaf. Which is probably more loaf than meat.", 
        new Option[] {
          new Option( "Why is it labeled \"Hepatitis\"?", 2 ),
          new Option( "Do you have anything edible?", 4 ),
          new Option( "Never mind.", -1 )
        }
      ),
      // 2
      new Step( camTarget, "Do you have Hepatitis?", 
        new Option[] {
          new Option( "No.", 3 )
        }
      ),
      // 3 
      new Step( camTarget, "Then don't eat the meatloaf.", 
        new Option[] {
          new Option( "Do you have anything edible?", 4 ),
          new Option( "Let me ask you something else.", 0 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 4
      new Step( camTarget, "That depends on your definition of \"edible\".", 
        new Option[] {
          new Option( "Something that I can eat that won't kill me.", 5 ),
          new Option( "Let me ask you something else.", 0 ),
          new Option( "See you later.", -1 )
        }
      ),
      // 5
      new Step( camTarget, "Sorry bud, I don't got anything like that.", 
        new Option[] {
          new Option( "Let me ask you something else.", 0 ),
          new Option( "See you later.", -1 )
        }
      ),
      // 6
      new Step( camTarget, "Was it Steve? That jackhole needs to keep his beak shut.", 
        new Option[] {
          new Option( "Do you have any?", 7 ),
          new Option( "I don't know a Steve.", 17 )
        }
      ),
      // 7
      new Step( camTarget, "Is this actually for you, or is it for one of those scum nuggets over there?", 
        new Option[] {
          new Option( "Does it matter?", 8 ),
          new Option( "It's for me.", 11 )
        }
      ),
      // 8
      new Step( camTarget, "I refuse to serve those two. They said some insulting things about my fat whore wife.", 
        new Option[] {
          new Option( "You just said some insulting things about your wife.", 9 ),
          new Option( "The Three Meat Surprise is for me.", 11 )
        }
      ),
      // 9
      new Step( camTarget, "Yeah well, she's my wife. To have and to hold y'know?", 10 ),
      // 10
      new Step( camTarget, "Except I can't hold her, 'cause she's so damn fat.", 
        new Option[] {
          new Option( "The Three Meat Surprise is for me.", 11 )
        }
      ),
      // 11
      new Step( camTarget, "I'm fresh out. If you get me the ingredients, I can make you some.", 
        new Option[] {
          new Option( "What do you need?", 13 ),
          new Option( "Isn't that your job?", 16 )
        },
        delegate() { wantsIngredients = true; }  
      ),
      // 12
      new Step( camTarget, "Yeah, but shut up. If you want the Three Meat Surprise, you gotta get me ingredients.", 
        new Option[] {
          new Option( "What ingredients do you need?", 13 ),
          new Option( "Let me ask you something else.", 0 ),
          new Option( "Ok, never mind then", -1 )
        }
      ),
      // 13
      new Step( camTarget, "I need chicken, ham, and the special secret ingredient...rat.", 
        new Option[] {
          new Option( "Rat? That sounds terrible.", 14 ),
          new Option( "Any idea where I can find those?", 15 ),
          new Option( "Ok, I'll go find them.", -1 ),
          new Option( "Let me ask you something else.", 0 )
        }
      ),
      // 14
      new Step( camTarget, "People used to say the same thing about Matthew McConaughey. Look at him now.", 
        new Option[] {
          new Option( "Good point. I'll go find those ingredients.", -1 ),
          new Option( "Let me ask you something else.", 0 )
        }
      ),
      // 15
      new Step( camTarget, "I dunno, check the freezer.", 
        new Option[] {
          new Option( "Ok, I'll go find the ingredients.", -1 ),
          new Option( "Let me ask you something else.", 0 )
        }
      ),
      // 16
      new Step( camTarget, "What are you, a cop?", 
        new Option[] {
          new Option( "What ingredients do you need?", 13 ),
          new Option( "Let me ask you something else.", 0 ),
          new Option( "I'll talk to you later.", -1 )
        }
      ),
      // 17
      new Step( camTarget, "You should keep it that way, that bird is a dick. So you want some Three Meat Surpise, huh?", 11 ),
      // 18
      new Step( camTarget, "Yeah, he traded it to me for some stuff I found in the sink drain.", 
        new Option[] {
          new Option( "I need that action figure", 20 ),
          new Option( "Can I trade you something for it?", 21 ),
          new Option( "He thought it was blood sausage.", 19 )
        }
      ),
      // 19
      new Step( camTarget, "He watched me pull it out of the drain.", 
        new Option[] {
          new Option( "I need that action figure.", 20 ),
          new Option( "Can I trade you something for that action figure?", 21 )
        }
      ),
      // 20
      new Step( camTarget, "Yeah, well I need to learn how to read. What's your point?", 
        new Option[] {
          new Option( "Can I trade you something for it?", 21 )
        }
      ),
      // 21
      new Step( camTarget, "Diet Cola.", 
        new Option[] {
          new Option( "Diet Cola?", 22 ),
          new Option( "Where can I find it?", 23 ),
          new Option( "I'll go look for it.", -1 ),
          new Option( "I have another question.", 0 )
        },
        delegate() { wantsCola = true; }  
      ),
      // 22
      new Step( camTarget, "Yep. Diet Cola. I'm on a diet, but I can't really bring myself to commit to a lifestyle change.", 
        new Option[] {
          new Option( "Where can I find it?", 23 ),
          new Option( "I'll go look for it.", -1 ),
          new Option( "I have another question.", 0 )
        }
      ),
      // 23
      new Step( camTarget, "The guard in the Guard Tower took the last bottle from the commissary.", 
        new Option[] {
          new Option( "I'll go look for it.", -1 ),
          new Option( "I have another question.", 0 )
        }
      )
    } );
  }  
}
