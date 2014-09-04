using UnityEngine;
using System.Collections;

/// Do whatever in here for specific Level 1 functionality

public class Level1 : MonoBehaviour {
  
  public bool lookingForCode = false;
  public bool knowBatCode = false;
  public bool seenWhale = false;
  public bool needBattery = false;

  void Start() {
    Game.script.AddItem( "glasses" );
    Game.script.AddItem( "wine_bottle" );
    Game.script.AddItem( "ham" );
  }
}
