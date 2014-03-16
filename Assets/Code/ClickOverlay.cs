using UnityEngine;
using System.Collections;

public class ClickOverlay : MonoBehaviour {
  void OnClick() {
    if( Game.cookies.Contains( "zoomed" ) ) {
      Game.cookies.Remove( "zoomed" );
      GameObject.FindGameObjectWithTag( "ZoomView" ).SetActive( false );
      Game.ZoomOut();
    }
  }
}
