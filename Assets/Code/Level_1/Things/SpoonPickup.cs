using UnityEngine;
using System.Collections;

public class SpoonPickup : MonoBehaviour {
  
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnItemClick() {
    if( Game.heldItem.name == "item_spoon" ) {
      Game.player.MoveTo( transform.position, delegate( bool b ) { 
        Game.script.UseItem();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
      } );
    }
  }
  
  void OnClick() {
    Game.player.MoveTo( transform.position, delegate( bool b ) { 
      if( GameObject.Find( "item_spoon" ) ) {
        Game.script.ShowSpeechBubble( "There's a big hole where the spoon was. \n Looks like a worm is sleeping in it.", Game.player.transform.Find( "BubTarget" ), 4f );
      } else {
        Game.script.AddItem( "spoon" );  
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
      }
    } );
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
