using UnityEngine;
using System.Collections;

public class ZoomClicker : MonoBehaviour {

  protected Texture2D cursor;
  protected Blur camBlur;
  protected GameObject zoomCam;
  
  public GameObject zoomView;

  public void Start() {
    cursor = Resources.Load( "Cursors/cursor_eye" ) as Texture2D;
    zoomView.SetActive( false );
    camBlur = Camera.main.GetComponent<Blur>();
    zoomCam = Camera.main.transform.Find( "ZoomCam" ).gameObject;
  }
  
	public void OnClick() {
    Vector3 camPos = Camera.main.transform.position;
    zoomView.SetActive( true );
    zoomView.transform.position = new Vector3( camPos.x, camPos.y, -9 );
    camBlur.enabled = true;
    zoomCam.SetActive( true );
    Game.PauseClicks();
  }
  
  public void OnHover( bool isOver ) {
    if( isOver )
      Cursor.SetCursor( cursor, new Vector2( 20f, 10f ), CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }
}