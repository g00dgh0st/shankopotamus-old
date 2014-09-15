using UnityEngine;
using System.Collections;

public class DialogueBubble : MonoBehaviour {
  
  private string text;
  private UILabel label;
  private int counter;
  
  void Start() {
    label = gameObject.GetComponent<UILabel>();
  }
  
  public void SetText( string t ) {
    text = t;
    counter = 0;
  }
  
  void Update() {
    if( counter < text.Length ) {
      counter++;
      label.text = text.Substring( 0, counter );
    }
    
    if( Input.GetMouseButtonUp( 0 ) && Game.dialogueManager.inDialogue ) {
      if( counter < text.Length ) {
        counter = text.Length - 1;
        return;
      } else {
        Game.dialogueManager.ContinueDialogue();
      }
    }
  }
}
