using UnityEngine;
using System.Collections;

public class MaintenanceGuy : MonoBehaviour {

  
  private DialogueManager DM;
  
  private Texture2D cursor;
  // private Bubble bub;
  // private Transform bubbleTrans;

  private Animator animator;

  // Begin Dialogue
  public Dialogue dialogue;

  public void Start() {
    DM = Game.dialogueManager;
    
    // bubbleTrans = transform.Find("BubbleTrans");
  
    SetupDialogue();    
    
    cursor = Resources.Load( "Cursors/cursor_chat" ) as Texture2D;
    animator = transform.parent.gameObject.GetComponent<Animator>();
  }
  
  public void Update() {
    // if( animator.GetBool( "RandomHead" ) ) {
//       // animator.SetBool( "RandomHead", false ); 
//     }
    
    Debug.Log ("fsdfds");
    
    Debug.Log( Random.Range( 1.0f, 2.0f ) );
    
    if( Random.Range( 1.0f, 2.0f ) == 1.0f ) {
      Debug.Log( "ereer" );
      animator.SetBool( "RandomHead", true ); 
    }
    
  }
  
  public void OnClick() {
    // if( (bool)dialogue.flags["isAngry"] ) {
    //   if( bub != null ) Game.dialogueManager.ClearBubble( bub );
    //   bub = Game.dialogueManager.ShowBubble( "You made me angry. Go away.", bubbleTrans, 5f );
    // } else
      Game.dialogueManager.StartDialogue( dialogue );
    
  }
  
  public void OnHover( bool isOver ) {
    if( isOver )
      Cursor.SetCursor( cursor, new Vector2( 10, 10 ), CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }
  
  // All dialogue is "written" here
  public void SetupDialogue() {
    
    Hashtable flags = new Hashtable();
    flags.Add( "isFishing", true );
    
    dialogue = new Dialogue( flags, gameObject );
    
    
    dialogue.SetSteps(
    new Step[2] {
      
        // Step 0
        new Step( "Hello.",
          new Option[2] {
            new Option( "I don't want to talk to you.", 1 ),
            new Option( "I have to go.", -1 )
          }
        ),
          
        // Step 1
        new Step( "Ok bye.", delegate() { DM.StopDialogue(); } )
      } 
    );
  }
}
