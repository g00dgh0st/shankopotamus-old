using UnityEngine;
using System.Collections;

public class WineToilet : MonoBehaviour {
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }

  void OnItemClick() {
    if( Game.heldItem.name == "item_empty_bottle" ) {
      Game.script.UseItem();
      Game.script.AddItem( "wine_bottle" );
    }
  }
  
  void OnClick() {
    Game.script.ShowSpeechBubble( "Looks like the toilet is filled with what looks like wine.", Game.player.transform.Find( "BubTarget" ), 3f );
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
