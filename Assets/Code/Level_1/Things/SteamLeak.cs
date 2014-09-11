using UnityEngine;
using System.Collections;

public class SteamLeak : MonoBehaviour {
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, "EyeCursor" );
  }
  
  void OnClick() {
    Game.script.ShowSpeechBubble( "Looks like steam is leaking from that pipe.\nI wonder where it's coming from.", Game.player.transform.Find( "BubTarget" ), 5f );
  }
  
}
