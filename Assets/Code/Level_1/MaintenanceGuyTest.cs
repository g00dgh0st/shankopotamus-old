using UnityEngine;
using System.Collections;

public class MaintenanceGuyTest : MonoBehaviour {
  
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

  void OnItemClick() {
    if( Game.heldItem.name == "item_three_meat_surprise" ) {
      Game.player.MoveTo( transform.position, delegate() {
        Game.script.UseItem();
        if( bub != null ) Destroy( bub );
        bub = Game.script.ShowSpeechBubble( "This smells like raw sewage. My mouth is already watering.\nAlso this is the end of this test demo. Go away.", transform.parent.Find( "BubTarget" ), 5f );
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
    new Step[5] {
      
        // Step 0
        new Step( camTarget, "Hey, can you do me a favor?",
          new Option[2] {
            new Option( "What kind of favor?", 1 ),
            new Option( "No.", -1 )
          }
        ),
        new Step( camTarget, "I need you to get that cook to make me a \"Three Meat Surprise\".",
          new Option[2] {
            new Option( "Why can't you ask him yourself?", 2 ),
            new Option( "Sure, I'll talk to him." , 4 )
          },
          delegate() { GameObject.Find( "CookTest" ).transform.Find( "Clicker" ).GetComponent<CookTest>().threeMeatOpen = true; }    
        ),
        new Step( camTarget, "I stabbed him once. He didn't take too kindly to it.",
          new Option[2] {
            new Option( "Why did you stab him?", 3 ),
            new Option( "I'll go ask him for some \"Three Meat Surprise\".", 4 )
          }
        ),
        new Step( camTarget, "It seemed like a decent idea at the time.", 
          new Option[1] {
            new Option( "Well you can't argue with that logic. I'll see what I can do.", 4 )
          }
        ),
        new Step( camTarget, "Thanks. And don't tell him that it's for me." )
      } 
    );
  }
}
