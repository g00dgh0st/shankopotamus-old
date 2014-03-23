﻿using UnityEngine;
using System.Collections;

public class DialogueBubble : MonoBehaviour {
  
  private string text;
  private UILabel label;
  private int counter;
  
  void Start() {
    label = transform.Find( "Label" ).gameObject.GetComponent<UILabel>();
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
    
    if( Input.GetMouseButton( 0 ) ) {
      if( counter < text.Length ) {
        counter = text.Length - 1;
        return;
      } else {
        Game.dialogueManager.ContinueDialogue();
      }
    }
  }
}
