using UnityEngine;
using System.Collections;

/// Do whatever in here for specific Level 1 functionality

public class Level1 : MonoBehaviour {

  void Start() {
    Game.script.AddItem( "test_item" );
    
    
    Game.script.ShowSpeechBubble( "Hello test", Game.player.gameObject.transform.Find( "BubTarget" ), 5f );
  }
}
