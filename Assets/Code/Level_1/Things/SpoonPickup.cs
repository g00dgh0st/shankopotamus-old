using UnityEngine;
using System.Collections;

public class SpoonPickup : Clicker {
  
  void Start() {
    cursorType = Clicker.CursorType.Hand;
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) { 
      
      CafeteriaGuard scrip = Game.GetScript<CafeteriaGuard>();
      
      if( !scrip.distracted ) {
        scrip.StopSpoon();
      } else {
        Game.player.Interact( "take", delegate() {
          Game.script.AddItem( "spoon" );  
          Destroy( gameObject );
        });
      }
    } );
  }
  
}
