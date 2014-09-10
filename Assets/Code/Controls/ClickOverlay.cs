using UnityEngine;
using System.Collections;

public class ClickOverlay : MonoBehaviour {
  public void OnClick() {
    if( Game.cookies.Contains( "zoomed" ) ) {
      Game.cookies.Remove( "zoomed" );
      GameObject.FindGameObjectWithTag( "ZoomView" ).transform.position = new Vector3( 0f, 5f, 0f );
      GameObject.FindGameObjectWithTag( "ZoomView" ).SetActive( false );
      Game.ZoomOut();
    }    
  }
}
