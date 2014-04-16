using UnityEngine;
using System.Collections;

public class ZoomClicker : MonoBehaviour {
  public GameObject zoomView;
  private Sprite cursor;
  
  public Transform moveTo;
  
  void Start() {
    zoomView.SetActive( false );
    cursor = Resources.Load<Sprite>( "Cursors/cursor_eye" );
  }
  
  void OnClick() {
    Transform pos = moveTo == null ? transform : moveTo;
    
    Game.player.MoveTo( pos.position, delegate() {
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
