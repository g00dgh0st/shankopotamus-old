using UnityEngine;
using System.Collections;

public class Option {
  public delegate void Callback();
  public delegate bool Condition();
  
  public string text;         // This is the text for the Option which shows in the button
  public Callback action;     // This is the action that is called when the Option is selected
  public Condition condition; // This is a function that returns true/false depending on whether this Option should be shown

  public Option( string t, Callback a ) {
    text = t;
    action = a;
    condition = null;
  }
  
  public Option( string t, int idx ) {
    text = t;
    if( idx >= 0 ) {
      action = delegate() { Game.dialogueManager.ChangeStep( idx ); };
    } else {
      action = delegate() { Game.dialogueManager.StopDialogue(); };
    }
    condition = null;
  }
  
  public Option( string t, Callback a, Condition c ) {
    text = t;
    action = a;
    condition = c;
  }
  
  public Option( string t, int idx, Condition c ) {
    text = t;
    if( idx >= 0 ) {
      action = delegate() { Game.dialogueManager.ChangeStep( idx ); };
    } else {
      action = delegate() { Game.dialogueManager.StopDialogue(); };
    }
    condition = c;
  }
}
