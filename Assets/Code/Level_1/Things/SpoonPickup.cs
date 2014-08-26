using UnityEngine;
using System.Collections;

public class SpoonPickup : MonoBehaviour {
  
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Game.player.MoveTo( transform.position, delegate( bool b ) { 
      
      CafeteriaGuard scrip = Game.GetScript<CafeteriaGuard>();
      
      if( !scrip.distracted ) {
        scrip.StopSpoon();
      } else {
        Game.script.AddItem( "spoon" );  
        Destroy( gameObject );
      }
    } );
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
