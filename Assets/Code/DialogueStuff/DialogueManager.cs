using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {
  
  public bool inDialogue = false;
  private Step step;
  private Dialogue dialogue;
  
  public GameObject bub;
  public GameObject opt;
  
  void Update() {
    if( inDialogue ) {
      
    }
  }
  
  public void StartDialogue( Dialogue dlg, int idx ) {
    inDialogue = true;
    
    Game.PauseClicks();
    Game.PauseCam();
    
    dialogue = dlg;
    
    step = dialogue.steps[idx];
    if( step.action != null && !step.actionLast ) step.action();
    
    Camera.main.orthographicSize = 0.4f;
    Game.TargetCam( step.speaker );
    bub.SetActive( true );
    bub.GetComponent<DialogueBubble>().SetText( step.text );
  }
  
  public void ChangeStep( int idx ) {
    ClearOptions();
    
    step = dialogue.steps[idx];
    if( step.action != null && !step.actionLast ) step.action();
    
    Game.TargetCam( step.speaker );
    bub.SetActive( true );
    bub.GetComponent<DialogueBubble>().SetText( step.text );
    
    Game.player.FaceTarget( step.speaker.transform.position, 0.1f );
  }
  
  public void ContinueDialogue() {
    Step startedStep = step;
    
    if( step.actionLast ) step.action();
    if( step == null ) return;
    if( step != startedStep ) return;
    // end dialogue if last step
    if( step.endStep ) {
      StopDialogue();
      return;
    }
    
    if( step.options != null ) {
      // check for options
      bub.SetActive( false );
      
      Game.TargetCam( Game.player.transform.Find( "CamTarget" ) );
      
      for( int i=0; i < step.options.Length; i++ ) {
        if( step.options[i].condition != null && step.options[i].condition() == false ) continue;
        
        GameObject newOpt = Instantiate( opt ) as GameObject;
        newOpt.SetActive( true );
        newOpt.transform.parent = opt.transform.parent;
        newOpt.transform.localScale = new Vector3( 1f, 1f, 1f );
        newOpt.transform.position = Vector3.zero;
        newOpt.GetComponent<OptionButton>().Setup( step.options[i].text, i );
      }
      
      GameObject.Find( "OptsContainer" ).GetComponent<UIScrollView>().ResetPosition();
      opt.transform.parent.GetComponent<UIGrid>().Reposition();
    } else {
      // if no options, go to the next step in the array
      int stepIdx = Step[].IndexOf( dialogue.steps, step ); 
      if( dialogue.steps.Length > stepIdx + 1 )
        ChangeStep( stepIdx + 1 );
      else
        StopDialogue();
    }
  }
  
  public void StopDialogue() {
    inDialogue = false;
    
    Game.ResumeClicks();
    Game.ResumeCam();
    
    dialogue = null;
    step = null;
    
    Camera.main.orthographicSize = 1f;
    Game.TargetCam( Game.player.transform.Find( "CamTarget" ) );
    bub.SetActive( false );
    ClearOptions();
    
    Game.cursor.SetActive( false );
    Screen.showCursor = true;
  }

  public void ChooseOption( int optionIndex ) {
    Game.cursor.SetActive( false );
    Screen.showCursor = true;
    step.options[optionIndex].action();
  }
  
  private void ClearOptions() {
    foreach( GameObject obj in GameObject.FindGameObjectsWithTag( "OptionButton" ) ) {
      Destroy( obj );
    }
  }

}

