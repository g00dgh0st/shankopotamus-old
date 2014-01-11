using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {
  
  private ArrayList bubbleList;
  
  public void Start() {
    bubbleList = new ArrayList();
  }

  public void OnGUI() {
    ArrayList deletes = new ArrayList();
    
    foreach( Bubble bub in bubbleList ) {
      GUI.Box( bub.GetRect(), bub.text );
      
      if( bub.time < 999f ) {
        bub.time -= Time.deltaTime;
        if( bub.time <= 0f ) deletes.Add( bub );
      } 
    }
    
    foreach( Bubble bub in deletes ) bubbleList.Remove( bub );
  }
  
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

