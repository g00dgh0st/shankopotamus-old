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
    if( step.action != null ) step.action();
    
    // Camera.main.orthographicSize = 0.4f;
    Game.TargetCam( step.speaker );
    bub.SetActive( true );
    bub.GetComponent<DialogueBubble>().SetText( step.text );
  }
  
  public void ChangeStep( int idx ) {
    step = dialogue.steps[idx];
    if( step.action != null ) step.action();
    
    Game.TargetCam( step.speaker );
    bub.SetActive( true );
    bub.GetComponent<DialogueBubble>().SetText( step.text );
  }
  
  public void ContinueDialogue() {
    if( step.options != null ) {
      bub.SetActive( false );
      
      GameObject newOpt = Instantiate( opt ) as GameObject;
      
      newOpt.transform.parent = opt.transform.parent;
      
      newOpt.SetActive( true );
      
      /// HERERERER
      
    } else {
      int stepIdx = Step[].IndexOf( dialogue.steps, step ); 
      if( dialogue.steps.Length > stepIdx + 1 )
        ChangeStep( stepIdx + 1 );
      else
        StopDialogue();
    }
  }
  
  public void StopDialogue() {
    // do sumfink
  }

  public void ChooseOption( int optionIndex ) {
    Debug.Log( "Fsdfd" );
    // step.options[optionIndex].action();
  }

}

