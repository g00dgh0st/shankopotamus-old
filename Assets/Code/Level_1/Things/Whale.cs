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
  
  void MoveWhale() {
    waypoint1.SetActive( true );
    waypoint2.SetActive( true );
    Debug.Log( "Knock Whale" );
    Destroy( gameObject );
  }
  
  void OnClick() {
    Game.player.MoveTo( movepoint.position, delegate() { Game.script.ShowSpeechBubble( "This whale is blocking the path. \nMaybe if I could flush something really big into the sewers...", Game.player.transform.Find( "BubTarget" ), 5f ); } );
  }
  
  void OnTriggerEnter( Collider collider ) {
    if( collider.gameObject.name == "Pig" ) {
      Destroy( collider.gameObject );
      MoveWhale();
    }
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
