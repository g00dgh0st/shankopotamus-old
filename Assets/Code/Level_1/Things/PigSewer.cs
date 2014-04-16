using UnityEngine;
using System.Collections;

public class PigSewer : MonoBehaviour {

  private bool moving = false;
  private bool fadeStarted = false;
  private bool bubShown = false;
  
  void OnEnable() {
    if( Game.cookies != null && Game.cookies.Contains( "pigInSewer" ) ) {
      StartCoroutine( Engage() );
    }
  }
  
  private IEnumerator Engage() {
    while( Game.clicksPaused || Game.player.InMotion() ) {
      yield return null;
    } 
    
    Game.PauseClicks();
    Game.PauseCam();
    moving = true;
    Game.cookies.Remove( "pigInSewer" );
    transform.Find( "Sprites" ).gameObject.SetActive( true );
  }
  
  void Update() {
    if( moving ) {
      transform.position = new Vector3( transform.position.x + 0.04f, transform.position.y, transform.position.z );
      
      Vector3 pos = Camera.main.transform.position;
      Vector3 newPos;
      Vector3 diff = new Vector3( transform.position.x + 1f, transform.position.y + 0.5f, -20f ) - pos;
      newPos = pos + ( diff * 0.05f );
      
      Camera.main.transform.position = newPos;

      if( transform.localPosition.x > 3.2f && !fadeStarted ) {
        fadeStarted = true;
        StartCoroutine( Game.FadeCamera( delegate() {
          GameObject.Find( "Rooms" ).transform.Find( "SewersRight" ).gameObject.SetActive( true );
          transform.localPosition = new Vector3( 5.5f, transform.localPosition.y, transform.localPosition.z );
          Camera.main.transform.position = new Vector3( transform.position.x + 1f, transform.position.y + 0.5f, -20f );
        } ) );
      }
      
      if( !bubShown && transform.position.x > Game.player.transform.position.x ) {
        Game.script.ShowSpeechBubble( "Not the best way to die.", transform.Find( "BubTarget" ), 5f );
        bubShown = true;
      }
    }
    
  }
}
