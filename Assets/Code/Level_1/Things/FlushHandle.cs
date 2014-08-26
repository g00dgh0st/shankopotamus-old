using UnityEngine;
using System.Collections;

public class FlushHandle : Clicker {
  
  private Sprite cursor;
  
  public bool isPowered = false;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    if( !isPowered ) {
      Debug.Log( "normal flush" );
    } else {
      Debug.Log( "suck in pig" );
      Destroy( GameObject.Find( "Pig" ) );
      Game.cookies.Add( "pigInSewer", true );
    }
  }

  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
