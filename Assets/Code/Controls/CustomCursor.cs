using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour {

	void Update() {
    transform.position = GameObject.Find( "Camera" ).GetComponent<Camera>().ScreenToWorldPoint( Input.mousePosition );
	}
  
  public void SetCursor( Sprite sprite ) {
    gameObject.GetComponent<UI2DSprite>().sprite2D = sprite;
    gameObject.GetComponent<UI2DSprite>().width = 20;
    gameObject.GetComponent<UI2DSprite>().height = 20;
  }
  
  public void SetItemCursor( Sprite sprite ) {
    gameObject.GetComponent<UI2DSprite>().sprite2D = sprite;
    gameObject.GetComponent<UI2DSprite>().width = 40;
    gameObject.GetComponent<UI2DSprite>().height = 40;
  }
}
