using UnityEngine;
using System.Collections;

public class SteamLeak : MonoBehaviour {
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, "EyeCursor" );
  }
  
  private void OnDragOver( GameObject obj ) {
    if( obj.CompareTag( "Item" ) ) {
      obj.GetComponent<UISprite>().alpha = 0.5f;
    }
  }
  
  private void OnDragOut( GameObject obj ) {
    if( obj.CompareTag( "Item" ) ) {
      obj.GetComponent<UISprite>().alpha = 1f;
    }
  }
  
  void OnItemDrop( string item ) {
    Game.script.ShowSpeechBubble( "That won't do anything.", Game.player.transform.Find( "BubTarget" ), 2f );
  }
  
  void OnClick() {
    Game.script.ShowSpeechBubble( "Looks like steam is leaking from that pipe.\nI wonder where it's coming from.", Game.player.transform.Find( "BubTarget" ), 5f );
  }
  
}
