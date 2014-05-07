using UnityEngine;
using System.Collections;

public class ExhaustSwitch : MonoBehaviour {
  
  public bool goingOut = true;
  private Sprite cursor;
  
  public FreezerFan fan;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Game.script.ShowSpeechBubble( "I can't reach it.", Game.player.transform.Find( "BubTarget" ), 3f );
  }
  
  void OnItemClick() {
    if( Game.heldItem.name == "item_ladder" ) {
      Game.player.MoveTo( transform.position, delegate( bool b ) {
        Game.script.UseItem();
        goingOut = false;
        transform.Find( "switch_in" ).gameObject.SetActive( true );
        transform.Find( "switch_out" ).gameObject.SetActive( false );
        transform.Find( "wind_in" ).gameObject.SetActive( true );
        transform.Find( "wind_out" ).gameObject.SetActive( false );
      });
    }
  }

  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
