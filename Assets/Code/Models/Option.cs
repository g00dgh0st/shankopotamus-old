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
  }
  
  public Option( string t, Callback a, Condition c ) {
    text = t;
    action = a;
    condition = c;
  }
}
