using UnityEngine;
using System.Collections;

public class Step {
  public delegate void Callback();
  
  public string text;
  public Option[] options;
  public Callback action;
  
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
