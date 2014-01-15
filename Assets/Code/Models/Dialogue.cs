using UnityEngine;
using System.Collections;

public class Dialogue {
  public delegate Step FirstStepFunc();
  
  public Step[] steps;                // List of Steps for this Dialogue
  public Hashtable flags;             // Hashtable of string keys with boolean values 
  public FirstStepFunc GetFirstStep;  // Function that handles loading the first Step
  
  public Dialogue( Hashtable f ) {
    flags = f;
  }

  public void SetSteps( Step[] s ) {
    steps = s;
  }
}
