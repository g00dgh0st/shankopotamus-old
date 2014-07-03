using UnityEngine;
using System.Collections;

public class ZoomClicker : Clicker {
  public GameObject zoomView;
  private Sprite cursor;
  
  void Awake() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_eye" );
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) {
      zoomView.SetActive( true );
      Game.cookies.Add( "zoomed", true );
      Game.ZoomIn();
      zoomView.transform.position = new Vector3( Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 5f );
    });
    
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
