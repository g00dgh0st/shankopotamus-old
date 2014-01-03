using UnityEngine;
using System.Collections;

public class ZoomOverlay : MonoBehaviour {
  
	public void OnMouseDown() {
    GameObject currentZoomView = GameObject.FindGameObjectsWithTag( "ZoomView" )[0];
    currentZoomView.SetActive( false );
    Destroy( gameObject );
  }
}