using UnityEngine;
using System.Collections;

public class CookTest : MonoBehaviour {
  
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
    // if( bub != null ) Destroy( bub );
    // bub = Game.script.ShowSpeechBubble( "Don't touch me.", transform.parent.Find( "BubTarget" ), 5f );
    Game.player.MoveTo( waypoint.position, delegate() { Game.dialogueManager.StartDialogue( dialogue, 0); } );
  }

  void OnItemClick() {
    if( !wantsIngredients ) return;
    
    if( Game.heldItem.name == "item_ham" || Game.heldItem.name == "item_rat" || Game.heldItem.name == "item_chicken" ) {
      Game.player.MoveTo( waypoint.position, delegate() {
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
    new Step[9] {
      
        // Step 0
        new Step( camTarget, "What do you want?",
          new Option[4] {
            new Option( "What kind of food do you have?", 1 ),
            new Option( "Can you make me a \"Three Meat Surprise\"?", 2, delegate() { return threeMeatOpen && !wantsIngredients; } ),
            new Option( "What ingredients do you need for the \"Three Meat Surprise\"?", 7, delegate() { return wantsIngredients; } ),
            new Option( "Nothing.", -1 )
          }
        ),
        new Step( camTarget, "We got some meatloaf. I think it's more loaf than meat though.",
          new Option[2] {
            new Option( "Oh...uh nevermind then.", -1 ),
            new Option( "Can you make me a \"Three Meat Surprise\"?", 2, delegate() { return threeMeatOpen && !wantsIngredients; } ),
          }
        ),
        new Step( camTarget, "This isn't for that stabby guy over there is it?",
          new Option[2] {
            new Option( "It's for me.", 3 ),
            new Option( "Why does it matter?", 8 )
          }
        ),
        new Step( camTarget, "Well, if you get me the ingredients, I'll make whatever.",
          new Option[2] {
            new Option( "What ingredients do you need?", 4 ),
            new Option( "Isn't that your job?", 5 )
          }, 
          delegate() { wantsIngredients = true; }
        ),
        new Step( camTarget, "I need chicken, ham, and the secret ingredient...rat.", 
          new Option[2] {
            new Option( "Sounds tasty. I'll go find those", -1 ),
            new Option( "Where can I find them?", 6 )
          }
        ),
        new Step( camTarget, "What are you, a cop?" ),
        new Step( camTarget, "What do I look like, some guy who knows the answer to that?" ),
        new Step( camTarget, "I need chicken, ham, and rat. Now quit bothering me." ),
        new Step( camTarget, "He stabbed me once. It was very rude. I refuse to cook anything for him.",
          new Option[1] {
            new Option( "It's for me. I'm a big fan of meats and surprises.", 3 )
          }
        )
      } 
    );
  }  
}
