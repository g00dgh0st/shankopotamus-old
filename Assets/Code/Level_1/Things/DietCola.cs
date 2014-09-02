using UnityEngine;
using System.Collections;

public class DietCola : Clicker {
  
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
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
          Game.script.AddItem( "diet_cola" );
          Destroy( gameObject );
        });
      }
    } );
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
