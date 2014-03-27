using UnityEngine;
using System.Collections;

public class Chicken : MonoBehaviour {
  
  private Sprite cursor;
  
  public Transform spot;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Game.player.MoveTo( spot.position, delegate() {
      Game.script.AddItem( "chicken" );
      Destroy( gameObject );
    });
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
