using UnityEngine;
using System.Collections;

public class Whale : MonoBehaviour {

  public GameObject waypoint1;
  public GameObject waypoint2;
  
  public Transform movepoint;
  
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  IEnumerator MoveWhale() {
    waypoint1.SetActive( true );
    waypoint2.SetActive( true );
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
    Game.player.MoveTo( movepoint.position, delegate() { Game.script.ShowSpeechBubble( "This whale is blocking the path. \nMaybe if I could flush something really big through the sewers...", Game.player.transform.Find( "BubTarget" ), 5f ); } );
  }
  
  void OnCollisionEnter2D( Collision2D collider ) {
    if( collider.gameObject.name == "PigSewer" ) {
      Destroy( collider.gameObject );
      StartCoroutine( MoveWhale() );
    }
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
