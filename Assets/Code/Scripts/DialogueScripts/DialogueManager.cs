using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {
  
  private ArrayList bubbleList;
  
  private Step currentStep;
  
  public void Start() {
    bubbleList = new ArrayList();
  }

  public void OnGUI() {
    
    if( currentStep != null ) {
      GUI.Box( new Rect( 0, 0, Screen.width, Screen.height / 5 ), currentStep.text );
    }
    
    
    // speech bubble handling
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
  
  // dialogue functions
  public void StartDialogue( Dialogue dlg ) {
    currentStep = dlg.GetFirstStep();
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

