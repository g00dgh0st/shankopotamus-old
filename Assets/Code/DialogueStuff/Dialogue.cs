using UnityEngine;
using System.Collections;

public class Dialogue {
  public Step[] steps;                // List of Steps for this Dialogue
  public Hashtable flags;             // Hashtable of string keys with boolean values 
  
  public Dialogue( Hashtable f ) {
    flags = f;
    Dialogue self = this;
  }

  public void SetSteps( Step[] s ) {
    steps = s;
  }
}
