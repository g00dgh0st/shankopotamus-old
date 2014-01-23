using UnityEngine;
using System.Collections;

public class ZoomClicker : MonoBehaviour {

  private Texture2D cursor;
  private Blur camBlur;
  private GameObject zoomCam;
  
  public GameObject zoomView;

  public void Start() {
    cursor = Resources.Load( "Cursors/cursor_eye" ) as Texture2D;
    zoomView.SetActive( false );
    camBlur = Camera.main.GetComponent( "Blur" ) as Blur;
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
      Cursor.SetCursor( cursor, Vector2.zero, CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }
}