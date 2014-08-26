using UnityEngine;
using System.Collections;

public class SimpleItemPickup : Clicker {
  
  private Sprite cursor;
  
  public string itemName;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) { 
      Game.script.AddItem( itemName );
      if( gameObject.name == "Clicker" )
        Destroy( gameObject.transform.parent.gameObject );
      else
        Destroy( gameObject );
    } );
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
