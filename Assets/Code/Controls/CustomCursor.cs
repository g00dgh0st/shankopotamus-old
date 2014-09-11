using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour {
  
  public UIAtlas itemAtlas;
  public UIAtlas guiAtlas;
  
  private UISprite cursorSprite;
  
  void Awake() {
    cursorSprite = gameObject.GetComponent<UISprite>();
  } 

	void Update() {
    transform.position = GameObject.Find( "Camera" ).GetComponent<Camera>().ScreenToWorldPoint( Input.mousePosition );
	}
  
  public void SetCursor( string sprite ) {
    cursorSprite.atlas = guiAtlas;
    cursorSprite.spriteName = sprite;
    cursorSprite.width = 50;
    cursorSprite.height = 50;
  }
  
  public void SetItemCursor( string sprite ) {
    cursorSprite.atlas = itemAtlas;
    cursorSprite.spriteName = sprite;
    cursorSprite.width = 100;
    cursorSprite.height = 100;
  }
}
