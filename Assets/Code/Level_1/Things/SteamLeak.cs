using UnityEngine;
using System.Collections;

public class SteamLeak : MonoBehaviour {
  
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Game.script.ShowSpeechBubble( "Looks like steam is leaking from that pipe.\nI wonder where it's coming from.", Game.player.transform.Find( "BubTarget" ), 5f );
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
