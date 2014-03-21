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
        GUI.Box( new Rect( 0, 0, Screen.width, Screen.height / 6 ), displayText, dialogPromptStyle );

        if( displayText.Length < currentStep.text.Length ) {
          // animate step text
          if( paceCounter % 3 == 0 ) displayText = currentStep.text.Substring( 0, displayText.Length + 1 );
        } else { 
          // animate ellipsis
          if( paceCounter % 40 == 0 )
            if( ctdText.Length < 3 ) ctdText += ".";
            else ctdText = ".";
        
          GUI.Box( new Rect( 0, 0, Screen.width, Screen.height / 6 ), ctdText, dialogueContinueTextStyle );
        }
        
        // invisible button to continue dialogue
        if( GUI.Button( new Rect( 0, 0, Screen.width, Screen.height ), "", dialogueContinueStyle ) ) {
          if( displayText.Length == currentStep.text.Length ) {
            if( !showOptions ) {
              showOptions = true;
              TargetCamera( Game.player.transform.Find( "HeadTrans" ) );
            } 
            if( queueStop ) {
              queueStop = false;
              TrueStopDialogue();
            }
          } else {
            displayText = currentStep.text;
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
      // end dialogue handling
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
    
    Camera.main.orthographicSize = 0.4f;
    
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
  
  
  // cam targeting
  private void TargetCamera( Transform t ) {
    Camera.main.transform.position = new Vector3( t.position.x, t.position.y, -10f );
  }
}

