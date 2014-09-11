using UnityEngine;
using System.Collections;

public class Shower : Clicker {
  
  public bool isOn = false;
  
  void Start() {
    cursorType = Clicker.CursorType.Hand;
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) {
      Game.player.Interact( "press", delegate( ) {
        Game.player.FaceTarget( transform.position );
        if( !isOn ) {
          transform.Find( "water" ).gameObject.SetActive( true );
          transform.Find( "Showers_Handle" ).localScale = new Vector3( -1.346163f, 1.346163f, 1.346163f );
          isOn = true;
          if( Game.cookies.Contains( "showersOn" ) ) {
            Game.cookies["showersOn"] = (int)Game.cookies["showersOn"] + 1;
          } else {
            Game.cookies.Add( "showersOn", 1 );
          }
        } else {
          transform.Find( "water" ).gameObject.SetActive( false );
          transform.Find( "Showers_Handle" ).localScale = new Vector3( 1.346163f, 1.346163f, 1.346163f );
          isOn = false;
          if( Game.cookies.Contains( "showersOn" ) && (int)Game.cookies["showersOn"] > 1 ) {
            Game.cookies["showersOn"] = (int)Game.cookies["showersOn"] - 1;
          } else if( (int)Game.cookies["showersOn"] <= 1 ) {
            Game.cookies.Remove( "showersOn" );
          }
        }
        transform.parent.Find( "SteamFog" ).gameObject.GetComponent<SteamFog>().CheckShowers();
      });
    });
  }
}
