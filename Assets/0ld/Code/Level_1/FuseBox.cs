using UnityEngine;
using System.Collections;

public class FuseBox : ZoomClicker {

  private bool isOpen = false;
  public bool isBroken = false;
  
  private GameObject shock;
  private bool isShocking = false;

  public void Start() {
    base.Start();
    cursor = Resources.Load( "Cursors/cursor_hand" ) as Texture2D;
    shock = transform.Find( "Shock" ).gameObject;
  }
  
  public void Update() {
    if( shock ) {
      if( shock.GetComponent<Animator>().GetCurrentAnimatorStateInfo( 0 ).IsName( "Base Layer.ShockingWire" ) ) {
        isShocking = true;
      } else if( isShocking ) {
        Destroy( shock );
      }
    }
  }
  
  public void OnClick() {
    if( !isOpen ) {
      isOpen = true;
      cursor = Resources.Load( "Cursors/cursor_eye" ) as Texture2D;
      transform.Find( "OpenBox" ).gameObject.SetActive( true ); 
      transform.Find( "ClosedBox" ).gameObject.SetActive( false ); 
    } else if( !isBroken ){
      base.OnClick();
    }
  }
  
  public void OnHover( bool isOver ) {
    if( isOver && !isBroken )
      Cursor.SetCursor( cursor, new Vector2( 10, 10 ), CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }
  
  public IEnumerator ShockAnimate() {
    yield return new WaitForSeconds(1);
    GameObject.Find( "ZoomCam" ).GetComponent<ZoomCam>().ClearZoom();
    Game.PauseClicks();
    
    isBroken = true;
    shock.SetActive( true );
    shock.GetComponent<Animator>().SetBool( "Broken", true ); 
    
    //Break discoball in left sewers
    Game.cookies.Add( "BreakDiscoBall", true );
    
    Game.ResumeClicks();
  }
}
