using UnityEngine;
using System.Collections;

public class Step {
  public delegate void Callback();
  
  public Transform speaker;           // GO of the speaker character 
  public string text;                 // This is the text prompt that shows when the Step is loaded
  public Option[] options;            // These are the options that will show for the user while on this Step
  public Callback action;             // This is an action that will be called when this Step is loaded
  public bool endStep = false;
  public bool actionLast = false;
  
  public Step( Transform s, string t, Option[] o, Callback a ) {
    text = t;
    options = o;
    action = a;
    speaker = s;
  }
  
  public Step( Transform s, string t, Option[] o ) {
    text = t;
    options = o;
    speaker = s;
  }
  
  public Step( Transform s, string t, Callback a ) {
    text = t;
    action = a;
    speaker = s;
    options = null;
  }
  
  public Step( Transform s, string t, Callback a, bool last ) {
    text = t;
    action = a;
    speaker = s;
    options = null;
    actionLast = last;
  }
  
  public Step( Transform s, string t ) {
    text = t;
    speaker = s;
    options = null;
    endStep = true;
  }
  
  public Step( Transform s, string t, int idx ) {
    text = t;
    speaker = s;
    options = null;
    action = delegate() { Game.dialogueManager.ChangeStep( idx ); };
    actionLast = true;
  }
}
