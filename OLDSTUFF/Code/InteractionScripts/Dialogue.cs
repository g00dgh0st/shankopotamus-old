using UnityEngine;
using System.Collections;

public class Dialogue {
  public delegate Step StartFunc();
  
  public Step[] steps;                // List of Steps for this Dialogue
  public Hashtable flags;             // Hashtable of string keys with boolean values 
  public GameObject speaker;          // GO of the speaker character 
  public StartFunc StartDialogue;     // Function that handles starting the dialogue and loading the first Step
  
  public Dialogue( Hashtable f, GameObject s ) {
    flags = f;
    speaker = s;
    
    Dialogue self = this;
    StartDialogue = delegate() { return self.steps[0]; };
  }

  public void SetSteps( Step[] s ) {
    steps = s;
  }
}
