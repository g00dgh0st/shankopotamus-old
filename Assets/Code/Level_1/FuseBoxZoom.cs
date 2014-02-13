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
      
      StartCoroutine( ShockAnimate() );
    }
  }

  
  public void OnHover( bool isOver ) {
    if( isOver && !isBroken )
      Cursor.SetCursor( cursor, new Vector2( 10, 10 ), CursorMode.Auto );
    else
  		Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
  }
  
  private IEnumerator ShockAnimate() {
    yield return new WaitForSeconds(1);
    GameObject.Find( "ZoomCam" ).GetComponent<ZoomCam>().ClearZoom();
    Game.PauseClicks();
    
    GameObject.Find( "FuseBox" ).GetComponent<FuseBox>().isBroken = true;
    
    GameObject shock = GameObject.Find( "FuseBox" ).transform.Find( "Shock" ).gameObject;
    shock.SetActive( true );
    shock.GetComponent<Animator>().SetBool( "Broken", true ); 
    
    
    // TODO Remove shock after animation is done, set up walk in trigger for Sewers Left
    
    while( shock.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName( "Base Layer.Idle" ) ) {
      yield return null;
    }
    
    while( shock.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName( "Base Layer.ShockingWire" ) ) {
      yield return null;
    }
    
    Destroy( shock );
       
    Game.ResumeClicks();
  }
}
