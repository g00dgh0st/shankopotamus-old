using UnityEngine;
using System.Collections;

public class WineToilet : Clicker {
  
  void Start() {
    cursorType = Clicker.CursorType.Eye;
  }

  void OnItemClick() {
    if( Game.heldItem.name == "item_empty_bottle" ) {
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        Game.script.UseItem();
        Game.script.AddItem( "wine_bottle" );
      } );
    }
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) {
      Game.script.ShowSpeechBubble( "Looks like the toilet is filled with what looks like wine.", Game.player.transform.Find( "BubTarget" ), 3f );
    } );
  }
  
}
