using UnityEngine;
using System.Collections;

public class Whale : Clicker {

  void Start() {
    cursorType = Clicker.CursorType.Hand;
  }
  
  void OnEnable() {
    Game.script.GetComponent<Level1>().seenWhale = true;
  }
  
  IEnumerator MoveWhale() {
    Debug.Log( "Knock Whale" );
    gameObject.GetComponent<SpriteRenderer>().enabled = false;
    
    yield return new WaitForSeconds( 1.5f );
    
    StartCoroutine( Game.FadeCamera( delegate() {
      Game.ResumeClicks();
      Game.ResumeCam();
      
      Vector3 plPos = Game.player.transform.Find( "CamTarget" ).position;
      Camera.main.transform.position = new Vector3( plPos.x, plPos.y, -20f );
      
      GameObject.Find( "SewersRight" ).SetActive( false );
      
      Destroy( gameObject );
    } ) );
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) { Game.script.ShowSpeechBubble( "This whale is blocking the path. \nMaybe if I could flush something really big through the sewers...", Game.player.transform.Find( "BubTarget" ), 5f ); } );
  }
  
  void OnTriggerEnter2D( Collider2D collider ) {
    if( collider.gameObject.name == "PigSewer" ) {
      Destroy( collider.gameObject );
      StartCoroutine( MoveWhale() );
    }
  }
}
