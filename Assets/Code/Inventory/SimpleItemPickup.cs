using UnityEngine;
using System.Collections;

public class SimpleItemPickup : MonoBehaviour {
  
  private Sprite cursor;
  
  public string itemName;
  
  public Transform point;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Transform moveTo = ( point == null ? transform : point );
    
    Game.player.MoveTo( moveTo.position, delegate() { 
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
