using UnityEngine;
using System.Collections;

public class ItemClicker : MonoBehaviour {
  public string name;
  public string label;
  public string description;
  
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }

  void OnClick() {
    Game.script.HoldItem( gameObject );
  }
  
  void OnHover( bool isOver ) {
    if( Game.heldItem != null ) return;
    if( isOver ) {
      Game.cursor.GetComponent<CustomCursor>().SetCursor( cursor );
      Game.cursor.SetActive( true );
      Screen.showCursor = false;
    } else {
      Game.cursor.SetActive( false );
      Screen.showCursor = true;
    }
  }
}
