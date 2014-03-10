using UnityEngine;
using System.Collections;

public class SewersMaintenanceGuy : MonoBehaviour {

  private DialogueManager DM;
  
  private Texture2D cursor;

  private Animator animator;

  // Begin Dialogue
  public Dialogue dialogue;
  
  private Bubble bub;
  
  private Transform bubTrans;
  

  public void Start() {
    DM = Game.dialogueManager;
        
    SetupDialogue();    
    
    cursor = Resources.Load( "Cursors/cursor_chat" ) as Texture2D;
    animator = transform.parent.gameObject.GetComponent<Animator>();
    bubTrans = transform.Find( "BubbleTrans" );
  }
  
  public void Update() {
    if( animator.GetBool( "RandomHead" ) ) animator.SetBool( "RandomHead", false ); 
    
    if( (bool)dialogue.flags["isFishing"] && Random.Range( 0, 200 ) == 1 ) animator.SetBool( "RandomHead", true );
    
    if( Game.cookies.Contains( "GetUpSewerGuy" ) ) {
      StartCoroutine( GetUp() );
    }
    
    
    if( animator.GetCurrentAnimatorStateInfo( 0 ).IsName( "Base Layer.GetUpFromFishing" ) && !animator.IsInTransition( 0 ) ) {
      dialogue.flags["isFishing"] = false;
    } else if( (bool)dialogue.flags["isFishing"] == false && (bool)dialogue.flags["isUp"] == false ) {
      GameObject rod = transform.parent.Find( "MaintenanceGuy" ).Find( "Holder" ).Find( "FishingRod" ).gameObject;
      
      Destroy( rod );
      
      dialogue.flags["isUp"] = true;
    }    
  }
  
  public void OnClick() {
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
    flags.Add( "isUp", false );
    
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
  
  private IEnumerator GetUp() {
    Game.cookies.Remove( "GetUpSewerGuy" );
    
    yield return new WaitForSeconds( 1f );

    bub = Game.dialogueManager.ShowBubble( "Damn disco ball's broken.", bubTrans, 5f );
    
    animator.Play( "GetUpFromFishing" );    
  }
}
