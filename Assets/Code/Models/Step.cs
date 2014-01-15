using UnityEngine;
using System.Collections;

public class Step {
  public delegate void Callback();
  
  public string text;       // This is the text prompt that shows when the Step is loaded
  public Option[] options;  // These are the options that will show for the user while on this Step
  public Callback action;   // This is an action that will be called when this Step is loaded
  
  public Step( string t, Option[] o, Callback a ) {
    text = t;
    options = o;
    action = a;
  }
  
  public Step( string t, Option[] o ) {
    text = t;
    options = o;
  }
  
  public Step( string t, Callback a ) {
    text = t;
    action = a;
  }
}
