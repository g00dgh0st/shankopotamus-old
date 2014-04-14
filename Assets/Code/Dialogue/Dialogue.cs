using UnityEngine;
using System.Collections;

public class Dialogue {
  public Step[] steps;                // List of Steps for this Dialogue
  
  public Dialogue() {
    Dialogue self = this;
  }

  public void SetSteps( Step[] s ) {
    steps = s;
  }
}
