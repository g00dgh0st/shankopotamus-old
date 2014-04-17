using UnityEngine;
using System.Collections;

public class Rat : MonoBehaviour {
  
  public RatHole hole;
  
  public Transform runAwayPos;
  
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    if( GameObject.Find( "Whale" ) ) {
      Game.TargetCam( Game.player.transform.Find( "CamTarget" ) );
      Game.script.ShowSpeechBubble( "This whale is in the way. I can't reach that.", Game.player.transform.Find( "BubTarget" ), 3f );
    } else
      Game.player.MoveTo( runAwayPos.position );
  }
  
  void OnHover( bool isOver ) {
    if( hole.ratPos == 1 && hole.moveTo == null ) Game.CursorHover( isOver, cursor );
  }
  
  void Update() {
    
    if( hole.ratPos == 1 && hole.moveTo == null ) {
      if( Vector3.Distance( Game.player.transform.position, runAwayPos.position ) < 0.5f ) {
        hole.ratPos = 0;
        hole.moveTo = hole.ratIn;
        transform.localScale = new Vector3( -1f, 1f, 1f );
      }
    } else if ( hole.ratPos == 0 && hole.moveTo == null ) {
      if( Vector3.Distance( Game.player.transform.position, runAwayPos.position ) > 0.5f ) {
        hole.ratPos = 1;
        hole.moveTo = hole.ratOut;
      }
    }
    
  }

}
