using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {
  
  private ArrayList bubbleList;
  
  private Step currentStep;
  private Dialogue currentDialogue;
  private float dialogueFadeCounter = 999f;
  
  public void Start() {
    bubbleList = new ArrayList();
  }

  public void OnGUI() {
    
    
    if( currentStep != null ) {
      // Dialogue handling
      GUI.Box( new Rect( 0, 0, Screen.width, Screen.height / 5 ), currentStep.text );

      Option[] opts = currentStep.options;
      
      if( opts != null ) {
        for( int i=0; i < opts.Length; i++ ) {
          if( ( opts[i].condition == null || opts[i].condition() ) && GUI.Button( new Rect( 0, Screen.height - 30 - ( ( opts.Length - i - 1 ) * 35 ), Screen.width, 30 ), opts[i].text ) ) 
            opts[i].action();
        }
      }
      
      if( dialogueFadeCounter < 999f ) {
        if( dialogueFadeCounter > 0f ) {
          dialogueFadeCounter -= Time.deltaTime;
        } else {
          dialogueFadeCounter = 999f;
          currentStep = null;
          currentDialogue = null;
        }
      }
      
    } else {
      // Speech Bubble handling
      ArrayList bubDeletes = new ArrayList();
  
      foreach( Bubble bub in bubbleList ) {
        GUI.Box( bub.GetRect(), bub.text );
    
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
    currentDialogue = dlg;
    currentStep = currentDialogue.StartDialogue();
    if( currentStep.action != null ) currentStep.action();
  }
  
  public void ChangeStep( int idx ) {
    currentStep = currentDialogue.steps[idx];
    if( currentStep.action != null ) currentStep.action();
  }
  
  public void StopDialogue() {
    Game.ResumeClicks();
    dialogueFadeCounter = 5.0f;
  }
  
  public void ImmediateStopDialogue() {
    Game.ResumeClicks();
    currentStep = null;
    currentDialogue = null;
    dialogueFadeCounter = 999f;
  }
  
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
}

