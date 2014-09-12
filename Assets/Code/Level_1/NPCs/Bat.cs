using UnityEngine;
using System.Collections;

public class Bat : Clicker {
  
  private Dialogue dialogue;
  
  public Pig pig;
  public Cook cook;
  
  public bool admittedFigure = false;
  public bool admittedDoesntHave = false;
  public bool askedCode = false;
  
  void Start() {
    SetupDialogue();
    cursorType = Clicker.CursorType.Chat;
  }

  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 0 ); } );
  }
  
  // All dialogue is "written" here
  public void SetupDialogue() {
    Transform camTarget = transform.parent.Find( "CamTarget" );
    
    dialogue = new Dialogue();
    
    dialogue.SetSteps(
    new Step[] {
      // 0
      new Step( camTarget, "Who dares disturb my slumber?",
        new Option[] {
          new Option( "The Pig said you stole something from him.", 1, delegate() { return pig.wantsSwede && GameObject.Find( "item_action_swede" ) == null && admittedFigure == false; } ),
          new Option( "Are you going to give the Pig's toy back?", 4, delegate() { return pig.wantsSwede && GameObject.Find( "item_action_swede" ) == null && admittedFigure == true && admittedDoesntHave == false; } ),
          new Option( "Who has the Action Swede?", 13, delegate() { return pig.wantsSwede && GameObject.Find( "item_action_swede" ) == null && admittedFigure == true && admittedDoesntHave == true; } ),
          new Option( "A little bird told me you know the code to the Guard Tower door.", 20, delegate() { return Game.script.GetComponent<Level1>().knowBatCode == true && askedCode == false; } ),
          new Option( "What is the code to the Guard Tower door?", 22, delegate() { return askedCode == true; } ),
          new Option( "What are all the scratches on the walls?", 23 ),
          new Option( "My bad. Go back to sleep.", -1 )
        }
      ),
      // 1
      new Step( camTarget, "I have no idea what you're talking about.",
        new Option[] {
          new Option( "He said it was some action figure.", 2 ),
          new Option( "Oh. Bye, then.", -1 )
        }
      ),
      // 2
      new Step( camTarget, "ACTION FIGURE?! It is no mere action figure! It is a limited editon 1:4 scale Action Swede model, with fist-punching action!", 3 ),
      // 3
      new Step( camTarget, "...I assume. Because as I said, I have no idea what you are talking about.", 
        new Option[] {
          new Option( "Do you have the toy or not?", 4 ),
          new Option( "I'm prepared to punch you very hard.", 15 ),
          new Option( "Oh. Bye, then.", -1 )
        },
        delegate() { admittedFigure = true; }  
      ),
      // 4 
      new Step( camTarget, "TOY?! It is no mere toy! It is an ACTION FIGURE! And it belongs to ME!", 
        new Option[] {
          new Option( "Why did you steal it from him?", 5 ),
          new Option( "Aren't you a little old to be playing with action figures?", 16 ),
          new Option( "Oh. Bye, then.", -1 )
        }
      ),
      // 5 
      new Step( camTarget, "He does not deserve it! I have watched every episode of the Action Swede TV show, I have seen every movie, and I own every single comic book!", 6 ),
      // 6
      new Step( camTarget, "I even know every single one of his 37 catchphrases! I could not stand the thought of him touching all over it with his fat, greasy, pig hands!", 
        new Option[] {
          new Option( "Well stealing is wrong, and you should give it back.", 7 ),
          new Option( "What are some of his catchphrases?", 28 )
        }
      ),
      // 7
      new Step( camTarget, "I suppose you are right. I may be a mass murderer, an arsonist, and a Mormon, but a thief I am not.", 8 ),
      // 8 
      new Step( camTarget, "I shall give the Swede to you. But first, you must answer a riddle.", 
        new Option[] {
          new Option( "I don't have time for this.", 9 ),
          new Option( "Ok, what's the riddle?", 10 )
        }
      ),
      // 9
      new Step( camTarget, "YOU MUST ANSWER A RIDDLE!", 10 ),
      // 10
      new Step( camTarget, "What can you catch, but not throw?", 
        new Option[] {
          new Option( "A taco salad.", 18 ),
          new Option( "A single nose hair.", 18 ),
          new Option( "Hepatitis.", 11 ),
          new Option( "Giraffe butt.", 19 )
        }
      ),
      // 11
      new Step( camTarget, "Correct! I will now give you the Swede. Although I don't have it.", 
        new Option[] {
          new Option( "What? Who has it then?", 13 ),
          new Option( "Why would you make me go through all that if you don't have it?", 12 ),
          new Option( "Have you ever been punched to death?", 15 )
        },
        delegate() { admittedDoesntHave = true; }  
      ),
      // 12 
      new Step( camTarget, "Because I am insane and have very little impulse control.", 13 ),
      // 13
      new Step( camTarget, "I traded the Action Swede to the Frog in exchange for a delicious blood sausage.", 
        new Option[] {
          new Option( "Where can I find the Frog?", 14 ),
          new Option( "I guess I'll go talk to the Frog then.", -1 )
        },
        delegate() { cook.knowSwede = true; }  
      ),
      // 14
      new Step( camTarget, "He is the cook in the cafeteria. You must try his blood sausage. I believe he makes it out of the blood of the smaller inmates.", 
        new Option[] {
          new Option( "I'll go talk to the Frog then.", -1 ),
          new Option( "Let me ask you something else.", 0 )
        }
      ),
      // 15 
      new Step( camTarget, "HOW DARE YOU THREATEN THE GREAT ME! ATTAAAACK!", delegate() {
          //fly around 
          Game.dialogueManager.StopDialogue();
        },
        true
      ),
      // 16
      new Step( camTarget, "Aren't you a little short for a Stormtrooper?", 
        new Option[] {
          new Option( "What?", 5 ),
          new Option( "I'm...gonna go.", -1 )
        }
      ),
      // 17 
      new Step( camTarget, "And of course, my favorite, \"Stop that black man!\".",
        new Option[] {
          new Option( "Well stealing is wrong, and you should give it back", 7 ),
          new Option( "You are a pretty big fan. I guess you should keep it.", -1 )
        }
      ),
      // 18 
      new Step( camTarget, "That makes no sense! Try again dumpus!", 10 ),
      // 19 
      new Step( camTarget, "Lol giraffe butt. Try again.", 10 ),
      // 20 
      new Step( camTarget, "Did Steve tell you that, perchance? That little bird bastard! He can never keep a secret!", 
        new Option[] {
          new Option( "What is the code?", 22 ),
          new Option( "I don't know any Steve", 21 )
        },
        delegate() { askedCode = true; }  
      ),
      // 21
      new Step( camTarget, "Oh. Then I was...joking. I do not know any code.", 
        new Option[] {
          new Option( "What is the code?", 22 ),
          new Option( "Dern. Well I guess I'll leave.", -1 )
        }
      ),
      // 22 
      new Step( camTarget, "I shall tell you nothing!", 
        new Option[] {
          new Option( "How about a clue?", 22 ),
          new Option( "What if I punch you on the face?",  15 ),
          new Option( "Let me ask you something else.", 0 ),
          new Option( "Dern. Well I guess I'll leave.", -1 )
        }
      ),
      // 23 
      new Step( camTarget, "I am keeping count.",
        new Option[] {
          new Option( "Of what?", 24 ),
          new Option( "Neat. See ya later.", -1 )
        }
      ),
      // 24
      new Step( camTarget, "I just like to count.",
        new Option[] {
          new Option( "But what are you counting?", 25 ),
          new Option( "Neat. See ya later.", -1 )
        }
      ),
      // 25
      new Step( camTarget, "Is it a crime to simply count? Why do all of you liberal, pot-smoking, gay-married hippies always have to count things?!", 26 ),
      // 26
      new Step( camTarget, "Can we not just enjoy counting for the sake of counting?",
        new Option[] {
          new Option( "Is this some kind of Sesame Street joke?", 27 ),
          new Option( "I guess. Let me ask you something else", 0 ),
          new Option( "Good point. I'm going to go now.", -1 )
        }
      ),
      // 27
      new Step( camTarget, "I do not know what that is! Go away!" ),
      // 28
      new Step( camTarget, "Well there is the classic, \"Action Swede is here to save the day!\".", 29 ),
      // 29
      new Step( camTarget, "Then there is the lesser known, \"I must save our beloved IKEA!\",", 17 )
    } );
  }  
}
