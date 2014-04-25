using UnityEngine;
using System.Collections;

public class FreezerFan : MonoBehaviour {
  
  private Sprite cursor;
  
  public ExhaustSwitch exhaust;
  public bool steaming;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    if( steaming ) {
      Game.script.ShowSpeechBubble( "There's steam blowing out of the fan.", Game.player.transform.Find( "BubTarget" ), 3f );
      return;
    }
    
    if( exhaust.goingOut )
      Game.script.ShowSpeechBubble( "It looks like it's sucking in the warm air.", Game.player.transform.Find( "BubTarget" ), 3f );
    else
      Game.script.ShowSpeechBubble( "It's blowing out some awful smelling air.", Game.player.transform.Find( "BubTarget" ), 3f );
  }

  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
