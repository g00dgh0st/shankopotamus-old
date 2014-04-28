using UnityEngine;
using System.Collections;

public class Shower : MonoBehaviour {
  
  private Sprite cursor;
  public bool isOn = false;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Game.player.MoveTo( transform.position, delegate() {
      if( !isOn ) {
        transform.Find( "sparks" ).gameObject.SetActive( true );
        transform.Find( "steam" ).gameObject.SetActive( true );
        isOn = true;
        if( Game.cookies.Contains( "showersOn" ) ) {
          Game.cookies["showersOn"] = (int)Game.cookies["showersOn"] + 1;
        } else {
          Game.cookies.Add( "showersOn", 1 );
        }
      } else {
        transform.Find( "sparks" ).gameObject.SetActive( false );
        transform.Find( "steam" ).gameObject.SetActive( false );
        isOn = false;
        if( Game.cookies.Contains( "showersOn" ) && (int)Game.cookies["showersOn"] > 1 ) {
          Game.cookies["showersOn"] = (int)Game.cookies["showersOn"] - 1;
        } else if( (int)Game.cookies["showersOn"] <= 1 ) {
          Game.cookies.Remove( "showersOn" );
        }
      }
      transform.parent.Find( "SteamFog" ).gameObject.GetComponent<SteamFog>().CheckShowers();
    });
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
