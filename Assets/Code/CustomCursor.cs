using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour {

	void Update () {
    transform.position = Camera.main.ScreenToWorldPoint( Input.mousePosition );
	}
  
  public void SetCursor( Sprite sprite ) {
    gameObject.GetComponent<UI2DSprite>().sprite2D = sprite;
  }
}
