using UnityEngine;
using System.Collections;

public class MetalFile : Clicker {
  
  void Start() {
    cursorType = Clicker.CursorType.Hand;
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) { 
    
      GuardTowerGuard scrip = Game.GetScript<GuardTowerGuard>();
    
      if( !scrip.atScreens && scrip.firstTime ) {
        Game.GetScript<GuardTowerGuard>().OnClick();
      } else if( !scrip.atScreens && !scrip.firstTime ) {
        scrip.DontTouchBubble();
      } else if( scrip.atScreens ) {
        Game.player.Interact( "take", delegate() {
          Game.player.FaceTarget( transform.position );
          Game.script.AddItem( "metal_file" );
          Destroy( gameObject );
        } );
      }
    } );
  }
}
