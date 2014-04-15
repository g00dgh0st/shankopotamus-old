using UnityEngine;
using System.Collections;

public class DietCola : MonoBehaviour {
  
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Game.player.MoveTo( transform.position, delegate() { 
    
      GuardTowerGuard scrip = Game.GetScript<GuardTowerGuard>();
    
      if( !scrip.atScreens && scrip.firstTime ) {
        Game.GetScript<GuardTowerGuard>().OnClick();
      } else if( !scrip.atScreens && !scrip.firstTime ) {
        scrip.DontTouchBubble();
      } else if( scrip.atScreens ) {
        Game.script.AddItem( "diet_cola" );
        Destroy( gameObject );
      }
    } );
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
