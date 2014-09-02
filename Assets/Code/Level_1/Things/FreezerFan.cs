using UnityEngine;
using System.Collections;

public class FreezerFan : MonoBehaviour {
  
  private Sprite cursor;
  
  public ExhaustSwitch exhaust;
  public bool steaming;

  void OnEnable() {
    if( Game.cookies != null && Game.cookies.Contains( "showersOn" ) && (int)Game.cookies["showersOn"] == 4 && !exhaust.goingOut ) {
      steaming = true;
      transform.Find( "steam" ).gameObject.SetActive( true );
      
      transform.parent.Find( "Icicle" ).gameObject.SetActive( true );
      
    } else {
      steaming = false;
      transform.Find( "steam" ).gameObject.SetActive( false );
    }
    
    if( exhaust.goingOut ) {
      transform.Find( "wind_in" ).gameObject.SetActive( true );
      transform.Find( "wind_out" ).gameObject.SetActive( false );
    } else {
      transform.Find( "wind_out" ).gameObject.SetActive( true );
      transform.Find( "wind_in" ).gameObject.SetActive( false );
    }
  }
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    if( steaming ) {
      Game.script.ShowSpeechBubble( "There's steam blowing out of the fan.", Game.player.transform.Find( "BubTarget" ), 3f );
      return;
    }
    
    if( exhaust.goingOut )
      Game.script.ShowSpeechBubble( "It looks like it's sucking in air.", Game.player.transform.Find( "BubTarget" ), 3f );
    else
      Game.script.ShowSpeechBubble( "It's blowing out some awful smelling air.", Game.player.transform.Find( "BubTarget" ), 3f );
  }

  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
