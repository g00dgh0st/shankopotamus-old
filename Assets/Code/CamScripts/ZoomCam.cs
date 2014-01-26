using UnityEngine;
using System.Collections;

public class ZoomCam : MonoBehaviour {
  
	public void Update() {
    if( Input.GetMouseButton( 0 ) ) {
      GameObject currentZoomView = GameObject.FindGameObjectsWithTag( "ZoomView" )[0];
      currentZoomView.SetActive( false );
      Blur camBlur = Camera.main.GetComponent<Blur>();
      camBlur.enabled = false;
      gameObject.SetActive( false );
      Game.ResumeClicks();
    }
  }
}