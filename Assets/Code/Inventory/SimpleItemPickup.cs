using UnityEngine;
using System;
using System.Collections;

public class SimpleItemPickup : Clicker {
  
  private Sprite cursor;
  
  public string itemName;
  
  public enum ItemHeight { High, Normal, Low };
  public ItemHeight itemHeight = ItemHeight.Normal;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) { 
      Game.player.FaceTarget( transform.position );
      Action callback = delegate() {
        Game.script.AddItem( itemName );
        if( gameObject.name == "Clicker" )
          Destroy( gameObject.transform.parent.gameObject );
        else
          Destroy( gameObject );
      };
      
      if( itemHeight == ItemHeight.High ) Game.player.Interact( "take_high", callback );
      if( itemHeight == ItemHeight.Low ) Game.player.Interact( "take_low", callback );
      else Game.player.Interact( "take", callback );
    } );
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
