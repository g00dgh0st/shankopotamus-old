using UnityEngine;
using System.Collections;

public class Bear : Clicker {
  
  private Dialogue dialogue;
  
  public bool wantsWine = false;
  public bool knowsMilitary = false;
  
  public Hoarder hoarder;
  
  void Start() {
    SetupDialogue();
    cursorType = Clicker.CursorType.Chat;
  }
  
  void OnItemDrop( string item ) {
    if( item == "wine_bottle" && wantsWine ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        Game.script.ShowSpeechBubble( "Praise be to Allah. Please, take honey.", transform.parent.Find( "BubTarget" ), 3f );
        Game.script.AddItem( "honey" );
        wantsWine = false;
      } );
    } else {
      base.OnItemDrop( item );
    }
  }

  void OnClick() {
    if( wantsWine )
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
      new Step( camTarget, "Hello. What brings you to Bosco's cell?", 
        new Option[] {
          new Option( "Can I have some of your honey?", 12, delegate() { return hoarder.wantsHoney && GameObject.Find( "item_honey" ) == null && !wantsWine; } ),
          new Option( "Can I have some of your honey?", 18, delegate() { return hoarder.wantsHoney && GameObject.Find( "item_honey" ) == null && wantsWine; } ),
          new Option( "Do you know where I can find the code for the Guard Tower door?", 19, delegate() { return Game.script.GetComponent<Level1>().lookingForCode == true; } ),
          new Option( "Were you in the military?", 1 ),
          new Option( "Do you know how I can get a weapon in here?", 11, delegate() { return knowsMilitary == true; } ),
          new Option( "That's a badass eyepatch. How did you lose your eye?", 6 ),
          new Option( "Nothing.", -1 )
        }
      ),
      // 1 
      new Step( camTarget, "Bosco was once greatest military commander in all of Soviet Russia.", 
        new Option[] {
          new Option( "How did you end up in here?", 2 ),
          new Option( "Did you lose your eye in battle?", 6 ),
          new Option( "So you must have an idea of how I can get a weapon in here.", 11 )
        },
        delegate() { knowsMilitary = true; }
      ),
      // 2 
      new Step( camTarget, "Bosco once try to take over world. But only a little bit. World very touchy about that kind of thing. They throw Bosco in jail.", 
        new Option[] {
          new Option( "Yeah, the world generally doesn't like being taken over", 3 ),
          new Option( "Bummer. I gotta go.", -1 )
        }
      ),
      // 3
      new Step( camTarget, "Bosco would have been great leader. Instead of shooting dissidents, Bosco would launch them into space. Much cooler way to die. ", 
        new Option[] {
          new Option( "Have you ever thought about not killing people?", 4 ),
          new Option( "That does sound pretty cool", 5 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 4
      new Step( camTarget, "Telling Bosco not to kill is like telling chimpanzee not to eat baby.", 
        new Option[] {
          new Option( "Let me ask you something else.", 0 ),
          new Option( "Oh. I will leave now.", -1 )
        }
      ),
      // 5
      new Step( camTarget, "You seem like smart man. When Bosco become king of world, Bosco will kill you swiftly.", 
        new Option[] {
          new Option( "Thanks. Let me ask you something else.", 0 ),
          new Option( "I have to go now.", -1 )
        }
      ),
      // 6
      new Step( camTarget, "It was terrible incident. Bosco was with comrades, having some fun, playing some games. Then BAM! Fork in eye.", 
        new Option[] {
          new Option( "How did a fork get in your eye?", 7 ),
          new Option( "At least you have that cool eyepatch now.", 9 )
        }
      ),
      // 7 
      new Step( camTarget, "Bosco was playing traditional game \"Fork In Eye Game\". Object of game is to put fork opponent eye.", 8 ),
      // 8
      new Step( camTarget, "Bosco is bad at game.", 
        new Option[] {
          new Option( "That sounds like a terrible game.", 10 ),
          new Option( "At least you have that cool eyepatch now.", 9 ),
          new Option( "I have another question", 0 ),
          new Option( "Welp, see ya.", -1 )
        }
      ),
      // 9 
      new Step( camTarget, "Also Bosco is able to keep loose change in eye cavity.", 
        new Option[] {
          new Option( "I have another question", 0 ),
          new Option( "Welp, see ya.", -1 )
        }
      ),
      // 10 
      new Step( camTarget, "Game is more fun after much vodka.", 
        new Option[] {
          new Option( "At least you have that cool eyepatch now.", 9 ),
          new Option( "I have another question", 0 ),
          new Option( "Welp, see ya.", -1 )
        }
      ),
      // 11 
      new Step( camTarget, "Bosco does not use weapons. Bosco tears enemies apart with bear hands.", 
        new Option[] {
          new Option( "I get it.", 5 ),
          new Option( "I have another question", 0 ),
          new Option( "Welp, see ya.", -1 )
        }
      ),
      // 12 
      new Step( camTarget, "Bosco is bear. Bosco need honey.", 
        new Option[] {
          new Option( "Can I trade you for it?", 14 ),
          new Option( "Don't you need something with more protein?", 13 ),
          new Option( "I guess I'll go then.", -1 )
        }
      ),
      // 13
      new Step( camTarget, "Are you bear? No. Bosco is bear. Bosco need honey.", 
        new Option[] {
          new Option( "Can I trade you for it?", 14 ),
          new Option( "I guess I'll go then.", -1 )
        }
      ),
      // 14
      new Step( camTarget, "Bosco is bear. Bosco is also crippling alcoholic. Bring Bosco some alcohol, and you take delicious Russian honey.", 
        new Option[] {
          new Option( "Where can I get some alcohol?", 15 ),
          new Option( "Ok. I have another question.", 0 ),
          new Option( "I'll go find some alcohol.", 16 )
        },
        delegate() { wantsWine = true; }
      ),
      // 15
      new Step( camTarget, "Crocodile man on second floor makes wine in toilet.", 
        new Option[] {
          new Option( "Ok. I have another question.", 0 ),
          new Option( "I'll go get some wine.", 16 )
        }
      ),
      // 16
      new Step( camTarget, "Go quickly. Bosco is getting the shakes something fierce.", delegate() { Game.dialogueManager.StopDialogue(); }, true ),
      // 17
      new Step( camTarget, "Do you have wine for Bosco? Bosco's palms are getting sweaty.", 
        new Option[] {
          new Option( "Where can I find some wine?", 15 ),
          new Option( "I have another question for you.", 0 ),
          new Option( "No, I'll go get it.", -1 )
        }
      ),
      // 18
      new Step( camTarget, "Bosco already tell you. Bring wine, then you take honey. Go now, before Bosco get angry.", delegate() { Game.dialogueManager.StopDialogue(); }, true ),
      // 19
      new Step( camTarget, "Bosco hear that crazy Bat man knows. Not Batman, but Bat man. Very different people.", 
        new Option[] {
          new Option( "I have another question for you.", 0 ),
          new Option( "Thanks. I gotta go.", -1 )
        },
        delegate() { Game.script.GetComponent<Level1>().knowBatCode = true; }
      )
    } );
  }  
}
