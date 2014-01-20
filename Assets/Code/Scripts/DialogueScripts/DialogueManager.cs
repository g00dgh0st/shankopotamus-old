using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {
  
  private ArrayList bubbleList;
  
  private Step currentStep;
  private Dialogue currentDialogue;
  private bool showOptions;
  private bool queueStop;
  private string ctdText = "...";
  private int paceCounter = 0;
  private string displayText;
  
  public GUIStyle dialogPromptStyle;
  public GUIStyle dialogButtonStyle;
  public GUIStyle dialogueContinueStyle;
  public GUIStyle dialogueContinueTextStyle;
  
  public GUIStyle speechBubbleStyle;
  
  public void Start() {
    bubbleList = new ArrayList();
  }

  public void OnGUI() {
    if( paceCounter >= 5000 ) paceCounter = 0;
    else paceCounter++;
    
    if( currentStep != null ) {
      // Dialogue handling
      
      // Speaker cam, show text for time
      if( !showOptions ) {
        GUI.Box( new Rect( 0, 0, Screen.width, Screen.height / 5 ), displayText, dialogPromptStyle );

        if( displayText.Length < currentStep.text.Length ) {
          if( paceCounter % 3 == 0 ) displayText = currentStep.text.Substring( 0, displayText.Length + 1 );
        } else {
          if( paceCounter % 50 == 0 )
            if( ctdText.Length < 3 ) ctdText += ".";
            else ctdText = ".";
        
          GUI.Box( new Rect( 0, 0, Screen.width, Screen.height / 5 ), ctdText, dialogueContinueTextStyle );
        }
        
        if( GUI.Button( new Rect( 0, 0, Screen.width, Screen.height ), "", dialogueContinueStyle ) ) {
          if( !showOptions ) {
            showOptions = true;
            TargetCamera( Game.player.transform.Find( "HeadTrans" ) );
          }
          if( queueStop ) {
            queueStop = false;
            TrueStopDialogue();
          }
        }
        
      } else {
        // Player cam, show options
        Option[] opts = currentStep.options;
        if( opts != null ) {
          for( int i=0; i < opts.Length; i++ ) {
            if( ( opts[i].condition == null || opts[i].condition() ) && GUI.Button( new Rect( 0, 0 + ( i  * 30 ), Screen.width, 30 ), "• " + opts[i].text, dialogButtonStyle ) ) 
              opts[i].action();
          }
        }
      }


    } else if( bubbleList.Count > 0 ){
      // Speech Bubble handling
      
      ArrayList bubDeletes = new ArrayList();
  
      foreach( Bubble bub in bubbleList ) {
        GUI.Box( bub.GetRect(), bub.text, speechBubbleStyle );
    
        if( bub.time < 999f ) {
          bub.time -= Time.deltaTime;
          if( bub.time <= 0f ) bubDeletes.Add( bub );
        } 
      }
  
      foreach( Bubble bub in bubDeletes ) bubbleList.Remove( bub );
    }
  }
  
  // dialogue functions
  public void StartDialogue( Dialogue dlg ) {
    Game.PauseClicks();
    Game.PauseCam();
    
    showOptions = false;
    queueStop = false;
    displayText = "";

    currentDialogue = dlg;
    currentStep = currentDialogue.StartDialogue();
    if( currentStep.action != null ) currentStep.action();
    
    Camera.main.orthographicSize = 2f;
    
    TargetCamera( currentDialogue.speaker.transform.Find( "HeadTrans" ) );
  }
  
  public void ChangeStep( int idx ) {
    showOptions = false;
    displayText = "";

    currentStep = currentDialogue.steps[idx];
    if( currentStep.action != null ) currentStep.action();
    
    TargetCamera( currentDialogue.speaker.transform.Find( "HeadTrans" ) );
  }
  
  public void StopDialogue() {
    queueStop = true;
  }
  
  public void ImmediateStopDialogue() {
    TrueStopDialogue();
  }
  
  private void TrueStopDialogue() {
    Game.ResumeClicks();
    Game.ResumeCam();
    CleanUpDialogue();
  }
  
  private void CleanUpDialogue() {
    currentStep = null;
    currentDialogue = null;
  }
  // end dialogue functions
  
  
  // speech bubble functions
  public Bubble ShowBubble( string tx, Transform tr ) {
    Bubble bub = new Bubble( tx, tr );
    bubbleList.Add( bub );
    return bub;
  }

  public Bubble ShowBubble( string tx, Transform tr, float ti ) {
    Bubble bub = new Bubble( tx, tr, ti );
    bubbleList.Add( bub );
    return bub;
  }
  
  public void ClearBubble( Bubble bub ) {
    bubbleList.Remove( bub );
  }
  // end speech bubble functions
  
  
  // cam targeting
  private void TargetCamera( Transform t ) {
    Camera.main.transform.position = new Vector3( t.position.x, t.position.y, -10f );
  }
}

