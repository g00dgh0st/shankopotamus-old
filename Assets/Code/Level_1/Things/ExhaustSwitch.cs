using UnityEngine;
using System.Collections;

public class ExhaustSwitch : Clicker {
  
  public bool goingOut = true;
  private Sprite cursor;
  
  public FreezerFan fan;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) {
      Game.script.ShowSpeechBubble( "I can't reach it.", Game.player.transform.Find( "BubTarget" ), 3f );
    } );
  }
  
  void OnItemClick() {
    if( Game.heldItem.name == "item_ladder" ) {
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        Game.script.UseItem();
        
        Game.PauseClicks();
        StartCoroutine( FlipSwitch() );
        
      });
    }
  }
  
  public IEnumerator FlipSwitch() {
    
    Game.player.PauseNav();
    
    transform.Find( "climbingLadder" ).gameObject.SetActive( true );

    yield return new WaitForSeconds( 0.5f );

    Game.player.FaceTarget( transform.position );
    
    Game.player.transform.position = new Vector3( Game.player.transform.position.x, Game.player.transform.position.y + 0.5f, Game.player.transform.position.z );
    
    yield return new WaitForSeconds( 0.5f );
    
    Game.player.Interact( "take_high", delegate() {
      goingOut = false;
      transform.Find( "switch_in" ).gameObject.SetActive( true );
      transform.Find( "switch_out" ).gameObject.SetActive( false );
      transform.Find( "wind_out" ).gameObject.SetActive( false );
    });
    
    yield return new WaitForSeconds( 1.8f );

    Game.player.transform.position = new Vector3( Game.player.transform.position.x, Game.player.transform.position.y - 0.5f, Game.player.transform.position.z );
    
    yield return new WaitForSeconds( 0.5f );
    
    Destroy( transform.Find( "climbingLadder" ).gameObject );
    
    Game.player.ResumeNav();
    
    Game.ResumeClicks();
    
    gameObject.GetComponent<BoxCollider2D>().enabled = false;
  }

  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
