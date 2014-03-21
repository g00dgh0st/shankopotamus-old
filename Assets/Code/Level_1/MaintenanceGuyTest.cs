using UnityEngine;
using System.Collections;

public class MaintenanceGuyTest : MonoBehaviour {
  
  private GameObject bub;
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
  }


  void OnClick() {
    if( bub != null ) Destroy( bub );
    bub = Game.script.ShowSpeechBubble( "Don't touch me.", transform.parent.Find( "BubTarget" ), 5f );
  }

  void OnItemClick() {
    if( bub != null ) Destroy( bub );
    
    if( Game.heldItem.name == "item_cheese" ) {
      Game.script.UseItem();
      bub = Game.script.ShowSpeechBubble( "I love cheese. \n Thanks.", transform.parent.Find( "BubTarget" ), 5f );
    } else
      bub = Game.script.ShowSpeechBubble( "Get that crap away from me.", transform.parent.Find( "BubTarget" ), 5f );
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
