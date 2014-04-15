using UnityEngine;
using System.Collections;

public class Radio : MonoBehaviour {
  
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    GuardTowerGuard scrip = Game.GetScript<GuardTowerGuard>();
    
    if( !scrip.atScreens && scrip.firstTime ) {
      Game.GetScript<GuardTowerGuard>().OnClick();
    } else if( !scrip.atScreens && !scrip.firstTime ) {
      scrip.DontTouchBubble();
    } else if( scrip.atScreens ) {
      Debug.Log( "Break, get battery" );
      Game.script.AddItem( "battery" );
      Destroy( gameObject );
    }
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
