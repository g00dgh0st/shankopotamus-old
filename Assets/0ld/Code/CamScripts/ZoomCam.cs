using UnityEngine;
using System.Collections;

public class ZoomCam : MonoBehaviour {
  
	public void Update() {
    if( Input.GetMouseButton( 0 ) ) {
      RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
      
      if( hit.collider.gameObject.name == "ClickOverlay" ) {
        ClearZoom();
      }
    }
  }
  
  public void ClearZoom() {
    GameObject currentZoomView = GameObject.FindGameObjectsWithTag( "ZoomView" )[0];
    currentZoomView.SetActive( false );
    Blur camBlur = Camera.main.GetComponent<Blur>();
    camBlur.enabled = false;
    gameObject.SetActive( false );
    Game.ResumeClicks();
  }
}