using UnityEngine;
using System.Collections;

public class FuseBoxZoom : MonoBehaviour {
  private Texture2D cursor;
  
  private bool isBroken = false;
 
  public void Start() {
    cursor = Resources.Load( "Cursors/cursor_hand" ) as Texture2D;
  }
  
  public void OnClick() {
    if( !isBroken ) {
      isBroken = true;
      transform.Find( "BrokenBox" ).gameObject.SetActive( true );
      transform.Find( "NormalBox" ).gameObject.SetActive( false );
      transform.Find( "Shock" ).gameObject.particleSystem.Emit(200);
      
      StartCoroutine( GameObject.Find( "FuseBox" ).GetComponent<FuseBox>().ShockAnimate() );
    }
  }
  
  public void OnHover( bool isOver ) {
    if( isOver && !isBroken )
      Cursor.SetCursor( cursor, new Vector2( 10, 10 ), CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }

}
