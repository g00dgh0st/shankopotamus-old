using UnityEngine;
using System.Collections;

public class Cook : MonoBehaviour {
  
  public bool threeMeatOpen = false;
  public bool wantsIngredients = false;
  
  public bool hasChicken = false;
  public bool hasHam = false;
  public bool hasRat = false;
  
  private GameObject bub;
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  public Transform waypoint;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    
    SetupDialogue();
  }


  void OnClick() {
    Game.player.MoveTo( waypoint.position, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 0); } );
  }

  void OnItemClick() {
    if( !wantsIngredients ) return;
    
    if( Game.heldItem.name == "item_ham" || Game.heldItem.name == "item_rat" || Game.heldItem.name == "item_chicken" ) {
      Game.player.MoveTo( waypoint.position, delegate( bool b ) {
        if( Game.heldItem.name == "item_ham" ) {
          hasHam = true;
          Game.script.UseItem();
          if( bub != null ) Destroy( bub );
          bub = Game.script.ShowSpeechBubble( "That's a nice looking ham.", transform.parent.Find( "BubTarget" ), 5f );
        } else if( Game.heldItem.name == "item_chicken" ) {
          hasChicken = true;
          Game.script.UseItem();
          if( bub != null ) Destroy( bub );
          bub = Game.script.ShowSpeechBubble( "Are you sure this is chicken? Whatever it's good enough.", transform.parent.Find( "BubTarget" ), 5f );
        } else if( Game.heldItem.name == "item_rat" ) {
          hasRat = true;
          Game.script.UseItem();
          if( bub != null ) Destroy( bub );
          bub = Game.script.ShowSpeechBubble( "This rat smells like it's been living in a sewer. That's good, it adds more flavor.", transform.parent.Find( "BubTarget" ), 5f );
        }
    
        if( hasRat && hasChicken && hasHam ) {
          if( bub != null ) Destroy( bub );
          bub = Game.script.ShowSpeechBubble( "That's all the ingredients. Here's my world famous \"Three Meat Surprise\".", transform.parent.Find( "BubTarget" ), 5f );
          Game.script.AddItem( "three_meat_surprise" );
          wantsIngredients = false;
        }
      });
    }
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
      new Step( camTarget, "What do you want?", 
        new Option[] {
          new Option( "What kind of food do you have?", 1 ),
          new Option( "I hear your specialty is Three Meat Surprise. You have any?", 5, delegate() { return threeMeatOpen && !wantsIngredients; } ),
          new Option( "What ingredients do you need again?", 6, delegate() { return wantsIngredients; } ),
          new Option( "Nothing", -1 )
        }
      ),
      // 1
      new Step( camTarget, "We got some kind of meatloaf. It's actually more loaf than meat, though. And we got uh, some peas.",
        new Option[] {
          new Option( "Why are the peas labeled \"Hepatitis\"?", 2 ),
          new Option( "Do you have any Three Meat Surprise?", 5, delegate() { return threeMeatOpen; } )
        }
      ),
      // 2
      new Step( camTarget, "Do you have Hepatitis?", 
        new Option[] {
          new Option( "No", 3 )
        }
      ),
      // 3
      new Step( camTarget, "Then don't eat the peas.",
        new Option[] {
          new Option( "What else do you have?", 4 ),
          new Option( "Do you have any Three Meat Surprise?", 5, delegate() { return threeMeatOpen; } )
        }
      ),
      // 4
      new Step( camTarget, "What are you a cop?", 
        new Option[] {
          new Option( "Nevermind, I have another question.", 0 ),
          new Option( "I'm going to leave now.", -1 )
        }
      ),
      // 5
      new Step( camTarget, "Are you asking for one of those scum nuggets over there?",
        new Option[] {
          new Option( "No, it's for me.", 10 ),
          new Option( "Why does that matter?", 11 )
        }
      ),
      // 6
      new Step( camTarget, "I need Chicken, Ham, and the special ingredient...Rat.",
        new Option[] {
          new Option( "Where can I find those?", 8 ),
          new Option( "Rat? That sounds...awful.", 7 ),
          new Option( "Ok, I'll go look for the ingredients.", -1 )
        }
      ),
      // 7
      new Step( camTarget, "Hey, don't knock it till you try it. People used to say the same thing about Matthew McConaughey.",
        new Option[] {
          new Option( "Good point. I'll try to find those ingredients.", -1 ),
          new Option( "Any idea when I can find them?", 8 )
        }
      ),
      // 8
      new Step( camTarget, "I dunno, try looking in the freezer.",
        new Option[] {
          new Option( "I'll go check it out.", -1 )
        }
      ),
      // 9
      new Step( camTarget, "Yeah, but shut up.",
        new Option[] {
          new Option( "Ok, I'll go find the ingredients.", -1 ),
          new Option( "What ingredients do you need again?", 6 )
        }
      ),
      // 10
      new Step( camTarget, "Sorry, but I'm fresh out. But if you get me the ingredients, I can make you some.",
        new Option[] {
          new Option( "What ingredients do you need?", 6 ),
          new Option( "Isn't that your job?", 9 )
        },
        delegate() { wantsIngredients = true; }    
      ),
      // 11
      new Step( camTarget, "I refuse to serve those two. They said some insulting things about my fat whore wife.",
        new Option[] {
          new Option( "It's not for them, it's for me.", 10 ),
          new Option( "You just said some insulting things about your wife.", 12 )
        }
      ),
      // 12
      new Step( camTarget, "Yeah, well, she's my wife. To have and to hold, y'know?", 13 ),
      // 13
      new Step( camTarget, "Except I can't actually hold her, since she's so damn fat.",
        new Option[] {
          new Option( "I want the Three Meat Surprise for myself.", 10 )
        }
      )
    } );
  }  
}
