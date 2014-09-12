using UnityEngine;
using System.Collections;

public class Ham : MonoBehaviour {
  
  void OnClick() {
    if( GameObject.Find( "BoxClicker" ).GetComponent<FreezerBox>().currentPos == 1 ) {
      Game.player.MoveTo( GameObject.Find( "BoxClicker" ).transform.position, delegate( bool b ) {
        StartCoroutine( GetHam() );
      });
    } else {
      Game.script.ShowSpeechBubble( "I can't reach that.",  Game.player.transform.Find( "BubTarget" ), 3f );
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
    Game.CursorHover( isOver, "HandCursor" );
  }
  
  public IEnumerator GetHam() {
    Game.PauseClicks();
    Camera.main.GetComponent<CameraControl>().pauseScale = true;
    Game.player.PauseNav();
    
    Game.player.transform.position = new Vector3( Game.player.transform.position.x, Game.player.transform.position.y + 0.15f, Game.player.transform.position.z );
    yield return new WaitForSeconds( 0.1f );
    
    Game.player.Interact( "take_high", delegate() {
      Game.script.AddItem( "ham" );
      gameObject.GetComponent<SpriteRenderer>().enabled = false;
    });
    
    while( Game.player.anim.IsPlaying( "take_high" ) ) {
      yield return null;
    }
    
    yield return new WaitForSeconds( 0.1f );
    Game.player.transform.position = new Vector3( Game.player.transform.position.x, Game.player.transform.position.y - 0.15f, Game.player.transform.position.z );
    
    Game.player.ResumeNav();
    Game.ResumeClicks();
    Camera.main.GetComponent<CameraControl>().pauseScale = false;
    
    Destroy( gameObject );
  }
}
