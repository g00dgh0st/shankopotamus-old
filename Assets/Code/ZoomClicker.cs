using UnityEngine;
using System.Collections;

public class ZoomClicker : MonoBehaviour {
  public GameObject zoomView;
  private Texture2D cursor;
  
  void Start() {
    zoomView.SetActive( false );
    cursor = Resources.Load( "Cursors/cursor_eye" ) as Texture2D;
  }
  
  void OnClick() {
    zoomView.SetActive( true );
    Game.cookies.Add( "zoomed", true );
    Game.PauseClicks();
    zoomView.transform.position = new Vector3( Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 5f );
  }
  
  void OnHover( bool isOver ) {
    if( isOver )
      Cursor.SetCursor( cursor, Vector2.zero, CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }
}
