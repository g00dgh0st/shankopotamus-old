using UnityEngine;
using System.Collections;

public class Rat : MonoBehaviour {
  
  public Transform[] positions;
  
  private int currentPos = 0;
  private int counter = 0;
  
  public bool inDialogue = false;

  private Sprite cursor;
  
  private Dialogue dialogue;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    SetupDialogue();
  }
  
  void OnClick() {
    inDialogue = true;
    Game.player.MoveTo( transform.position, delegate() { Game.dialogueManager.StartDialogue( dialogue, 0); inDialogue = false; } );
  }
  
  void Update() {
    if( inDialogue || Game.dialogueManager.inDialogue ) return;
    
    if( Vector3.Distance( transform.position, positions[currentPos].position ) > 0.01f ) {
      transform.position += ( positions[currentPos].position - transform.position ) * 0.1f;
      gameObject.GetComponent<Collider2D>().enabled = ( currentPos == 0 ? false : true );
    } else {
      counter++;
      
      if( counter > 50 ) {
        currentPos = ( currentPos == 0 ? 1 : 0 );
        gameObject.transform.localScale = new Vector3( -1 * gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z );
        counter = 0;
      }
    }
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
  
  public void SetupDialogue() {
    Transform camTarget = transform.Find( "CamTarget" );
    
    dialogue = new Dialogue();
    
    dialogue.SetSteps(
    new Step[4] {
        new Step( camTarget, "Hey, you got any cheese?",
          new Option[3] {
            new Option( "Why do you need cheese?", 1 ),
            new Option( "Would you like to be cooked into some food?", 3, delegate() { return GameObject.Find( "CookTest" ).transform.Find( "Clicker" ).gameObject.GetComponent<CookTest>().wantsIngredients; } ),
            new Option( "No.", 2 )
          }
        ),
        new Step( camTarget, "I'm a rat. That stuff's like crack for us. I'm tweaking real bad.",
          new Option[2] {
            new Option( "I don't have any cheese.", 2 ),
            new Option( "Want to be made into food?", 3, delegate() { return GameObject.Find( "CookTest" ).transform.Find( "Clicker" ).gameObject.GetComponent<CookTest>().wantsIngredients; } ),
          }
        ),
        new Step( camTarget, "Then your existence is meaningless to me." ),
        new Step( camTarget, "Eh, why not. YOLO, right?", delegate() { Game.dialogueManager.StopDialogue(); Game.script.AddItem( "rat" ); Destroy( gameObject ); }, true )
      } 
    );
  }
}
