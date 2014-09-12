using UnityEngine;
using System.Collections;

public class Rat : MonoBehaviour {
  
  public RatHole hole;
  
  public Transform runAwayPos;
  
  void OnClick() {
    Game.player.MoveTo( runAwayPos.position );
  }
  
  void OnHover( bool isOver ) {
    if( hole.ratPos == 1 && hole.moveTo == null ) Game.CursorHover( isOver, "HandCursor" );
  }
  
  private void OnDragOver( GameObject obj ) {
    if( obj.CompareTag( "Item" ) ) {
      obj.GetComponent<UISprite>().alpha = 0.5f;
    }
  }
  
  private void OnDragOut( GameObject obj ) {
    if( obj.CompareTag( "Item" ) ) {
      obj.GetComponent<UISprite>().alpha = 1f;
    }
  }
  
  void OnItemDrop( string item ) {
    Game.script.ShowSpeechBubble( "That won't do anything.", Game.player.transform.Find( "BubTarget" ), 2f );
  }
  
  void Update() {
    if( hole.ratPos == 1 && hole.moveTo == null ) {
      if( Vector3.Distance( Game.player.transform.position, runAwayPos.position ) < 0.5f ) {
        hole.ratPos = 0;
        hole.moveTo = hole.ratIn;
        transform.localScale = new Vector3( -1f, 1f, 1f );
      }
    } else if ( hole.ratPos == 0 && hole.moveTo == null ) {
      if( Vector3.Distance( Game.player.transform.position, runAwayPos.position ) > 0.5f && Game.player.transform.position.x < runAwayPos.position.x ) {
        hole.ratPos = 1;
        hole.moveTo = hole.ratOut;
      }
    }
    
  }

}
