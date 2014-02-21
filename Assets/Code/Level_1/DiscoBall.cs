using UnityEngine;
using System.Collections;

public class DiscoBall : MonoBehaviour {
  private bool isBroken = false;
  
  public void OnEnable() {
    if( Game.cookies != null && Game.cookies.Contains( "BreakDiscoBall" ) ) {
      Game.cookies.Remove( "BreakDiscoBall" );
      Break();
    }
  }

  public void Break() {
    isBroken = true;
    transform.Find( "NormalBall" ).gameObject.SetActive( false );
    transform.Find( "BrokenBall" ).gameObject.SetActive( true );
    transform.Find( "Shock" ).gameObject.SetActive( true );
  }
}
