using UnityEngine;
using System.Collections;

public class Option {
  public delegate void Callback();
  
  public string text;
  public Callback action;

  public Option( string t, Callback a ) {
    text = t;
    action = a;
  }
}
