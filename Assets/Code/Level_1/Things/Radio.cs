using UnityEngine;
using System.Collections;

public class Radio : Clicker {
  
  void Start() {
    cursorType = Clicker.CursorType.Hand;
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) { 
      Game.player.FaceTarget( transform.position );
      
      if( Game.cookies.Contains( "showersOn" ) ) {
        switch( (int)Game.cookies["showersOn"] ) {
          case 0:
            Game.script.ShowSpeechBubble( "Hey! I can clearly see you tryin' to steal my radio!", transform.Find( "BubTarget" ), 3f );
            break;
          case 1:
            Game.script.ShowSpeechBubble( "Hey! I see you tryin' to steal my radio!", transform.Find( "BubTarget" ), 3f );
            break;
          case 2:
            Game.script.ShowSpeechBubble( "Hey! I can kinda see you tryin' to steal my radio!", transform.Find( "BubTarget" ), 3f );
            break;
          case 3:
            Game.script.ShowSpeechBubble( "Hey! I can barely see you tryin' to steal my radio!", transform.Find( "BubTarget" ), 3f );
            break;
          case 4:
            Game.player.Interact( "take_high", delegate() {
              Game.script.AddItem( "radio" );
              Destroy( gameObject );
            });
            break;
          default:
            Game.script.ShowSpeechBubble( "Hey! I can clearly see you tryin' to steal my radio!", transform.Find( "BubTarget" ), 3f );
            break;
        }
      } else {
        Game.script.ShowSpeechBubble( "Hey! I can clearly see you tryin' to steal my radio!", transform.Find( "BubTarget" ), 3f );
      }
    } );
  }
}
