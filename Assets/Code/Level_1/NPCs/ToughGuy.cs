using UnityEngine;
using System.Collections;

public class ToughGuy : Clicker {
  
  public bool wantsMeat = false;
  public bool talkedOnce = false;
  public bool onFire = false;
  
  public Dialogue dialogue;
  
  void Awake() {
    cursorType = Clicker.CursorType.Chat;
    SetupDialogue();
  }


  void OnClick() {
    if( Game.GetScript<WimpyGuy>().onFire ) {
      Game.script.ShowSpeechBubble( "Nothing like eating good food in front of a warm fire.", transform.parent.Find( "BubTarget" ), 3f );
      return;
    } 
    
    if( !onFire ) {
      int index;
    
      if( wantsMeat ) {
        index = 32;
      } else if( talkedOnce ) {
        index = 8;
      } else {
        index = 0;
      }
    
      Game.player.MoveTo( movePoint, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, index ); } );
    } else {
      Game.script.ShowSpeechBubble( "Ah crap, I'm on fire again.", transform.parent.Find( "BubTarget" ), 3f );
    }
  }

  void OnItemDrop( string item ) {
    if( wantsMeat && item == "three_meat_surprise" ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        GameObject.Find( "WimpyGuy" ).transform.Find( "fire" ).gameObject.SetActive( true );
        Game.GetScript<WimpyGuy>().onFire = true;
        Game.script.ShowSpeechBubble( "I set him on fire.", transform.parent.Find( "BubTarget" ), 3f );
        Game.script.ShowSpeechBubble( "AAAAHHHH!", GameObject.Find( "WimpyGuy" ).transform.Find( "BubTarget" ), 3f );
        StartCoroutine( Game.GetScript<CafeteriaGuard>().Distraction() );
      });
    } else {
      base.OnItemDrop( item );
    } 
  }
  
  // All dialogue is "written" here
  public void SetupDialogue() {
    Transform toughTarget = transform.parent.Find( "CamTarget" );
    Transform wimpyTarget = GameObject.Find( "WimpyGuy" ).transform.Find( "CamTarget" );
    
    dialogue = new Dialogue();
    
    dialogue.SetSteps(
    new Step[] {
      // 0
      new Step( toughTarget, "Hey guy, can you do me a favor?", 1 ),
      // 1 
      new Step( wimpyTarget, "Don't listen to him, he's an idiot. I'm the one who needs a favor.", 2 ),
      // 2
      new Step( toughTarget, "Oh, I'm the idiot? You drink your own piss every day.", 3 ),
      // 3
      new Step( wimpyTarget, "And I've yet to develop cancer, so clearly it works! Listen, about that favor...",
        new Option[] {
          new Option( "What do you need?", 18 ),
          new Option( "Any idea where I can get a weapon in here?", 39 ),
          new Option( "Do you know where I can find the code for the Guard Tower door?", 37, delegate() { return Game.script.GetComponent<Level1>().lookingForCode; } ),
          new Option( "Why do you guys hate each other?", 10 ),
          new Option( "Sorry, I'm busy", -1 )
        }, 
        delegate() { talkedOnce = true; }
      ),
      // 4
      new Step( wimpyTarget, "Excuse me could I ask you to do me a favor?", 5 ),
      // 5
      new Step( toughTarget, "Don't listen to that dumpus, I'm the one who needs a favor.", 6 ),
      // 6
      new Step( wimpyTarget, "Why do you always have to interrupt me? This is just like the Fall talent show all over again!", 7 ),
      // 7
      new Step( toughTarget, "You were corpsing! You ruined the scene like some damn Jimmy Fallon! Listen, buddy, about that favor...",
        new Option[] {
          new Option( "What do you need?", 18 ),
          new Option( "Any idea where I can get a weapon in here?", 39 ),
          new Option( "Do you know where I can find the code for the Guard Tower door?", 37, delegate() { return Game.script.GetComponent<Level1>().lookingForCode; } ),
          new Option( "Why do you guys hate each other?", 10 ),
          new Option( "Sorry, I'm busy", -1 )
        }, 
        delegate() { talkedOnce = true; }
      ),
      // 8
      new Step( toughTarget, "What do you want?",
        new Option[] {
          new Option( "What was that favor you needed?", 18 ),
          new Option( "Any idea where I can get a weapon in here?", 39 ),
          new Option( "Do you know where I can find the code for the Guard Tower door?", 37, delegate() { return Game.script.GetComponent<Level1>().lookingForCode; } ),
          new Option( "Why do you guys seem to hate each other?", 10 ),
          new Option( "Sorry, I'm busy", -1 )
        }
      ),
      // 9
      new Step( wimpyTarget, "What's up?",
        new Option[] {
          new Option( "What was that favor you needed?", 18 ),
          new Option( "Any idea where I can get a weapon in here?", 39 ),
          new Option( "Do you know where I can find the code for the Guard Tower door?", 37, delegate() { return Game.script.GetComponent<Level1>().lookingForCode; } ),
          new Option( "Why do you guys seem to hate each other?", 10 ),
          new Option( "Sorry, I'm busy", -1 )
        }
      ),
      // 10
      new Step( toughTarget, "We've been cellmates for the past 10 years. You can't spend that much time with a person and not hate them.", 11 ),
      // 11
      new Step( wimpyTarget, "We might as well be married.",
        new Option[] {
          new Option( "Why doesn't one of you transfer to a different cell?", 12 ),
          new Option( "Why not try talking through your differences?", 16 )
        }
      ),
      // 12
      new Step( wimpyTarget, "Trust me, we've tried. This prison is pretty full.", 13 ),
      // 13
      new Step( toughTarget, "The only way to move to a new cell, is to kill a guy. Then, if you're lucky, you might get to take his spot.", 36 ),
      // 14
      new Step( wimpyTarget, "Eh, it's not so bad. They have Bingo on Tuesdays.", 15 ),
      // 15
      new Step( toughTarget, "And Fight Club on Thursdays. Very relaxing.",
        new Option[] {
          new Option( "What was that favor you needed?", 18 ),
          new Option( "I have another topic of discussion.", 8 ),
          new Option( "Well, I need to go", -1 ),
        }
      ),
      // 16
      new Step( toughTarget, "Don't try to psychoanalyze me buddy. The last guy who did ended up sleepin' with the fishes, if you know what I mean.", 17 ),
      // 17
      new Step( wimpyTarget, "He joined the Navy, and is now stationed on a submarine.", 
        new Option[] {
          new Option( "What was that favor you needed?", 18 ),
          new Option( "I have another topic of discussion.", 9 ),
          new Option( "Well, I need to go", -1 ),
        }
      ),
      // 18
      new Step( wimpyTarget, "I need you to get a Three Meat Surprise from that cook over there.", 19 ),
      // 19
      new Step( toughTarget, "Don't get it for him, I can help you out if you get it for me.", 20 ),
      // 20
      new Step( wimpyTarget, "I'm smarter than him, I can help better!", 
        new Option[] {
          new Option( "What can you do for me?", 22 ),
          new Option( "What exactly is Three Meat Surprise?", 21 ),
          new Option( "Why can't you just get it yourself?", 27 ),
          new Option( "Sorry, I'm busy.", -1 )
        }
      ),
      // 21
      new Step( wimpyTarget, "It's the cook's specialty. It's the best food you can get in this prison. Get me some, and I'll return the favor.",
        new Option[] {
          new Option( "What can you do for me?", 22 ),
          new Option( "Why can't you guys just get it yourselves?", 27 ),
          new Option( "Sorry, I'm busy.", -1 )
        }
      ),
      // 22
      new Step( wimpyTarget, "What do you need?", 
        new Option[] {
          new Option( "Can you distract the guard?", 23 ),
          new Option( "Do you have a copy of \"Ernest Goes to Africa\"?", 34 ),
          new Option( "Nothing. I gotta go.", -1 )
        }
      ),
      // 23
      new Step( toughTarget, "Hey, buddy, I've had my fair share of experience in distracting guards. Get me the Three Meat Surprise, and I got you covered.", 24 ),
      // 24
      new Step( wimpyTarget, "You can't trust him! Give it to me, and I'll guarantee that the guard is distracted for as long as you need.", 
        new Option[] {
          new Option( "What is Three Meat Surprise?", 21 ),
          new Option( "Why can't you just get it yourself?", 27 ),
          new Option( "I'll go ask the cook for some.", -1 ),
        }, 
        delegate() { wantsMeat = true; Game.GetScript<Cook>().threeMeatOpen = true; Game.dialogueManager.ChangeStep( 25 ); }
      ),
      // 25
      new Step( toughTarget, "Remember, bring it to me.", 26 ),
      // 26
      new Step( wimpyTarget, "No, bring it to me!" ),
      // 27
      new Step( wimpyTarget, "I called his wife fat, and he's held a grudge against me ever since.", 28 ),
      // 28
      new Step( toughTarget, "Yeah, and I called his wife a whore, and now he refuses to serve me.", 29 ),
      // 29
      new Step( wimpyTarget, "We also tried to stab him once, but that's water under the bridge.", 
        new Option[] {
          new Option( "What can you do for me?", 22 ),
          new Option( "What is Three Meat Surprise?", 21 ),
          new Option( "Sorry, I'm busy.", -1 )
        }
      ),
      // 30 
      new Step( wimpyTarget, "Any progress on the Three Meat Surprise?", 
        new Option[] {
          new Option( "Let me go get some.", 31 ),
          new Option( "I have another topic of discussion.", 9 ),
          new Option( "No, I'm busy.", -1 ),
        }
      ),
      // 31
      new Step( toughTarget, "Make sure you bring it to me." ),
      // 32
      new Step( toughTarget, "Any progress on that Three Meat Surprise?", 
        new Option[] {
          new Option( "Let me go get some.", 33 ),
          new Option( "I have another topic of discussion.", 8 ),
          new Option( "No, I'm busy.", -1 ),
        }
      ),
      // 33
      new Step( wimpyTarget, "Make sure you bring it to me." ),
      // 34
      new Step( wimpyTarget, "Um...No...", 
        new Option[] {
          new Option( "Can you distract the guard?", 23 ), 
          new Option( "How about \"Ernest Saves Christmas\"?", 35 ),
          new Option( "Then, no deal!", -1 )
        }
      ),
      // 35
      new Step( toughTarget, "We are not fans of the \"Ernest\" film franchise.", 
        new Option[] {
          new Option( "Can you distract the guard?", 23 ), 
          new Option( "Then, no deal!", -1 )
        }
      ),
      // 36
      new Step( wimpyTarget, "It's the same for getting a nice apartment in Manhattan.", 
        new Option[] {
          new Option( "This sounds like a tough place.", 14 ),
          new Option( "I have another topic of discussion.", 9 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 37
      new Step( toughTarget, "A little bird told me the Bear knows the code.", 38 ),
      // 38
      new Step( wimpyTarget, "You know you can't trust Steve! That dumbass bird doesn't know anything!", 
        new Option[] {
          new Option( "I have another topic of discussion.", 9 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 39
      new Step( toughTarget, "Weapon? Just use your fists like a man.", 40 ),
      // 40
      new Step( wimpyTarget, "Not everyone took steroids as a baby, moron. I can get you a gun, but it would take 6 to 45 weeks to get here.", 
        new Option[] {
          new Option( "I have another topic of discussion.", 9 ),
          new Option( "Thanks, I gotta go.", -1 )
        }
      )
    } );
  }  
}
