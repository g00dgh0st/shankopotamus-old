using UnityEngine;
using System.Collections;

public class Shower : MonoBehaviour {
  
  private Sprite cursor;
  public bool isOn = false;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Game.player.MoveTo( transform.position, delegate() {
      if( !isOn ) {
        transform.Find( "sparks" ).gameObject.SetActive( true );
        transform.Find( "steam" ).gameObject.SetActive( true );
        isOn = true;
      } else {
        transform.Find( "sparks" ).gameObject.SetActive( false );
        transform.Find( "steam" ).gameObject.SetActive( false );
        isOn = false;
      }
    });
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
