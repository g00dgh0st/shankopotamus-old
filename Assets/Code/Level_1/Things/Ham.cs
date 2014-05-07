using UnityEngine;
using System.Collections;

public class Ham : MonoBehaviour {
  
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    if( GameObject.Find( "BoxClicker" ).GetComponent<FreezerBox>().currentPos == 1 ) {
      Game.player.MoveTo( transform.position, StartGetHam );
    } else {
      Game.script.ShowSpeechBubble( "I can't reach that.",  Game.player.transform.Find( "BubTarget" ), 3f );
    }
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
  
  public void StartGetHam( bool b ) {
    Game.PauseClicks();
    StartCoroutine( GetHam() );
  }
  
  public IEnumerator GetHam() {
    
    Game.player.transform.position = new Vector3( Game.player.transform.position.x, Game.player.transform.position.y + 0.25f, Game.player.transform.position.z );
    
    yield return new WaitForSeconds( 0.5f );

    Game.script.AddItem( "ham" );
    gameObject.GetComponent<SpriteRenderer>().enabled = false;
    
    yield return new WaitForSeconds( 0.5f );

    Game.player.transform.position = new Vector3( Game.player.transform.position.x, Game.player.transform.position.y - 0.25f, Game.player.transform.position.z );
    
    Game.ResumeClicks();
    
    Destroy( gameObject );
  }
}
