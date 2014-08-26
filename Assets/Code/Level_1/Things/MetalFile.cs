using UnityEngine;
using System.Collections;

public class MetalFile : MonoBehaviour {
  
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Game.player.MoveTo( transform.position, delegate( bool b ) { 
    
      GuardTowerGuard scrip = Game.GetScript<GuardTowerGuard>();
    
      if( !scrip.atScreens && scrip.firstTime ) {
        Game.GetScript<GuardTowerGuard>().OnClick();
      } else if( !scrip.atScreens && !scrip.firstTime ) {
        scrip.DontTouchBubble();
      } else if( scrip.atScreens ) {
        Game.script.AddItem( "metal_file" );
        Destroy( gameObject );
      }
    } );
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
