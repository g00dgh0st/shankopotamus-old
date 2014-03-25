using UnityEngine;
using System.Collections;

public class OptionButton : MonoBehaviour {
  
  private int optionIndex;
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  public void Setup( string t, int i ) {
    transform.Find( "Label" ).gameObject.GetComponent<UILabel>().text = t;
    optionIndex = i;
  }
  
  void Update() {
    gameObject.GetComponent<BoxCollider2D>().size = new Vector2( gameObject.GetComponent<UISprite>().width, 40f );
  }
  
  void OnClick() {
    Game.dialogueManager.ChooseOption( optionIndex );
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
