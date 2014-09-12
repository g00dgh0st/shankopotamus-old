using UnityEngine;
using System.Collections;

public class RatHole : MonoBehaviour {

  public Transform ratIn;
  public Transform ratPeek;
  public Transform ratOut;
  
  // 0 - in; 1- out; 2 - peeking;
  public int ratPos = 1;
  
  public Transform rat;
  
  public Transform moveTo;
  private float speed = 0.1f;
  
  void OnItemDrop( string item ) {
    if( item == "cheese_rod" && ratPos == 0 && moveTo == null ) {
      Game.script.UseItem();
      Game.player.MoveTo( transform.position, delegate( bool b ) {
        Destroy( rat.gameObject );
        ratPos = 3;
        Game.script.AddItem( "rat" );
      } );
    } else {
      Game.script.ShowSpeechBubble( "That won't do anything.", Game.player.transform.Find( "BubTarget" ), 2f );
    }
  }
  
  void OnClick() {
    if( ratPos == 0 && moveTo == null ) {
      ratPos = 2;
      StartCoroutine( PeekOut() );
    }
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
  
  void OnHover( bool isOver ) {
    if( ratPos == 0 ) Game.CursorHover( isOver, "HandCursor" );
  }
  
  void Update() {
    if( moveTo != null ) {
      Vector3 diff = moveTo.position - rat.position;
      rat.position = rat.position + ( diff * speed );
      if( Vector3.Distance( moveTo.position, rat.position ) < 0.001f ) {
        moveTo = null;
        rat.localScale = new Vector3( 1f, 1f, 1f );
      }
    }
  }
  
  private IEnumerator PeekOut() {
    moveTo = ratPeek;
    while( moveTo != null ) {
      yield return null;
    }
    Game.script.ShowSpeechBubble( "Fuck off.", rat.Find( "BubTarget" ), 1f );
    yield return new WaitForSeconds( 1f );
    moveTo = ratIn;
    while( moveTo != null ) {
      yield return null;
    }
    ratPos = 0;
  }

}
