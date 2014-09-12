using UnityEngine;
using System.Collections;

public class WineToilet : Clicker {
  
  void Start() {
    cursorType = Clicker.CursorType.Eye;
  }

  void OnItemDrop( string item ) {
    if( item == "empty_bottle" ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        Game.script.AddItem( "wine_bottle" );
      } );
    } else {
      base.OnItemDrop( item );
    }
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) {
      Game.script.ShowSpeechBubble( "Looks like the toilet is filled with what looks like wine.", Game.player.transform.Find( "BubTarget" ), 3f );
    } );
  }
  
}
