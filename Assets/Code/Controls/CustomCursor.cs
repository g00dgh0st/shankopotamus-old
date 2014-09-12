using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour {
  
  public UIAtlas itemAtlas;
  public UIAtlas guiAtlas;
  
  private UISprite cursorSprite;
  
  public float offsetX = 0f;
  public float offsetY = 0f;
  
  void Awake() {
    cursorSprite = gameObject.GetComponent<UISprite>();
    SetOffset( cursorSprite.spriteName );
    Screen.showCursor = false;
  } 

	void Update() {
    transform.position = GameObject.Find( "Camera" ).GetComponent<Camera>().ScreenToWorldPoint( new Vector2( Input.mousePosition.x + offsetX, Input.mousePosition.y + offsetY ) );
	}
  
  public void SetCursor( string sprite ) {
    cursorSprite.atlas = guiAtlas;
    cursorSprite.spriteName = sprite;
    cursorSprite.width = 50;
    cursorSprite.height = 50;
    SetOffset( sprite );
  }
  
  public void SetItemCursor( string sprite ) {
    cursorSprite.atlas = itemAtlas;
    cursorSprite.spriteName = sprite;
    cursorSprite.width = 100;
    cursorSprite.height = 100;
  }
  
  private void SetOffset( string sprite ) {
    switch( sprite ) {
      case "FingerCursor":
        offsetX = Screen.width * 0.0025f;
        offsetY = -Screen.height * 0.017f;
        break;
      case "PointerCursor":
        offsetX = Screen.width * 0.006f;
        offsetY = -Screen.height * 0.02f;
        break;
      default:
        offsetX = 0f;
        offsetY = 0f;
        break;
    }
  }
}
