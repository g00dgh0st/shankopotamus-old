using UnityEngine;
using System.Collections;

public class ZoomClicker : Clicker {
  public GameObject zoomView;
  
  void Start() {
    cursorType = Clicker.CursorType.Eye;
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) {
      zoomView.SetActive( true );
      Game.cookies.Add( "zoomed", true );
      Game.ZoomIn();
      zoomView.transform.position = new Vector3( Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 5f );
    });
    
  }
}

