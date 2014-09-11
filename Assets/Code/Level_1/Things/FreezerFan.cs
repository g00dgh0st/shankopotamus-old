using UnityEngine;
using System.Collections;

public class FreezerFan : Clicker {
  
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
  }
  
  void OnClick() {
    if( steaming ) {
      Game.player.FaceTarget( transform.position );
      Game.script.ShowSpeechBubble( "There's steam blowing out of the fan.", Game.player.transform.Find( "BubTarget" ), 3f );
      return;
    }
    
    if( exhaust.goingOut ) {
      Game.player.FaceTarget( transform.position );
      Game.script.ShowSpeechBubble( "It looks like it's sucking in air.", Game.player.transform.Find( "BubTarget" ), 3f );
    } else {
      Game.player.FaceTarget( transform.position );
      Game.script.ShowSpeechBubble( "It's blowing out some awful smelling air.", Game.player.transform.Find( "BubTarget" ), 3f );
    }
  }

  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, "EyeCursor" );
  }
}
