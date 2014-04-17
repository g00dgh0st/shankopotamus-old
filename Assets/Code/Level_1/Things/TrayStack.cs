using UnityEngine;
using System.Collections;

public class TrayStack : MonoBehaviour {
  
  private Sprite cursor;
    
  private bool once = false;
    
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Game.player.MoveTo( transform.position, delegate() { 
      if( GameObject.Find( "item_tray" ) == null ) {
        Game.script.AddItem( "tray" );
        if( !once ) {
          Game.script.ShowSpeechBubble( "If I can break this up, I can use one of the shards for a shank.", Game.player.transform.Find( "BubTarget" ), 3f );
          once = true;
        }
      } else {
        Game.script.ShowSpeechBubble( "I already have a tray.", Game.player.transform.Find( "BubTarget" ), 3f );
      }
    } );
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
