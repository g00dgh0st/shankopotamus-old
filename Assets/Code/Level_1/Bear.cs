using UnityEngine;
using System.Collections;

public class Bear : MonoBehaviour {
  
  private GameObject bub;
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    
    SetupDialogue();
  }


  void OnClick() {
    Game.player.MoveTo( transform.position, delegate() { Game.dialogueManager.StartDialogue( dialogue, 0); } );
  }

  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
  
  // All dialogue is "written" here
  public void SetupDialogue() {
    Transform camTarget = transform.parent.Find( "CamTarget" );
    
    dialogue = new Dialogue();
    
    dialogue.SetSteps(
    new Step[13] {
      // 0
      new Step( camTarget, "Hello. What brings you to Bosco's cell?", 
        new Option[4] {
          new Option( "Can I have some of your honey?", 9 ),
          new Option( "Are those military medals?", 1 ),
          new Option( "Are you going to eat that cake?", 12 ),
          new Option( "Nothing.", -1 )
        }
      ),
      // 1
      new Step( camTarget, "Yes, Bosco was once greatest military commander in all Soviet Russia.", 
        new Option[2] {
          new Option( "How did you end up here?", 5 ),
          new Option( "Did you lose your eye in a battle?", 2 )
        }
      ),
      // 2
      new Step( camTarget, "No, Bosco's eye was lost in unrelated incident. Bosco was with comrades, having some fun, playing some games, then BAM! Fork in eye.",
        new Option[2] {
          new Option( "Well, they do say it's all fun and games until someone loses an eye.", 3 ),
          new Option( "How did you end up in here?", 5 )
        }
      ),
      // 3
      new Step( camTarget, "The object of game was to stab your opponent in the eye. Bosco is bad at game.",
        new Option[3] {
          new Option( "That sounds like a terrible game.", 4 ),
          new Option( "Can I have some honey?", 9 ),
          new Option( "How did you end up in here?", 5 )
        }
      ),
      // 4
      new Step( camTarget, "Vodka helps.", 
        new Option[3] {
          new Option( "How did you end up in here?", 5 ),
          new Option( "Can I have some of your honey?", 9 ),
          new Option( "I'll see you later.", -1 )
        }
      ),
      // 5 
      new Step( camTarget, "Bosco try to take over the world. World get angry. World throw Bosco in jail.",
        new Option[1] {
          new Option( "Yeah the world generally doesn't like that.", 6 )
        }
      ),
      // 6
      new Step( camTarget, "Bosco would have been kind leader. Instead of shooting dissidents, Bosco would launch them into space. Much cooler way to die.", 
        new Option[2] {
          new Option( "That sounds fair.", 7 ),
          new Option( "How about not killing people?", 8 )
        }
      ),
      // 7
      new Step( camTarget, "You seem like nice person. When Bosco rules the world, Bosco will be sure to give you a swift death.",
        new Option[2] {
          new Option( "That sounds great. Can I have some honey?", 9 ),
          new Option( "Um...I have to go.", -1 )
        }
      ),
      // 8
      new Step( camTarget, "Telling Bosco not to kill is like telling chimpanzee not to eat baby. He may try to change, but in the end. Chimpanzee always eat baby.", 
        new Option[2] {
          new Option( "Well, that's fair. Can I have some honey?", 9 ),
          new Option( "I'll talk to you later.", -1 )
        }
      ),
      // 9 
      new Step( camTarget, "Bosco is bear. Bosco need honey.",
        new Option[1] {
          new Option( "Can I trade you something for it?", 10 )
        }
      ),
      // 10
      new Step( camTarget, "Bosco is bear. Bosco is also crippling alcoholic. Bring Bosco some wine, and you can have some delicious Russian honey.",
        new Option[2] {
          new Option( "Where can I get some?", 11 ),
          new Option( "I'll go get you some wine.", -1 )
        }
      ),
      // 11
      new Step( camTarget, "Crocodile on second floor makes wine in toilet. Go quickly. Bosco is getting shakes something fierce.",
        new Option[1] {
          new Option( "I'll be back with some wine.", -1 )
        }
      ),
      // 12 
      new Step( camTarget, "Bosco has gluten allergy. It is Bosco's only weakness. That and alcoholism. Cake is yours." )
    } );
  }  
}