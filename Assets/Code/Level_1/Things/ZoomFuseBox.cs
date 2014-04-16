using UnityEngine;
using System.Collections;

public class ZoomFuseBox : MonoBehaviour {
  
  private bool broken = false;
  
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    if( !broken ) {
      transform.Find( "normal" ).gameObject.SetActive( false );
      transform.Find( "broken" ).gameObject.SetActive( true );
      Game.GetScript<FuseBox>().broken = true;
      StartCoroutine( CloseOut() );
    }
  }
  
  IEnumerator CloseOut() {
    yield return new WaitForSeconds( 1f );
    Game.GetScript<ClickOverlay>().OnClick();
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
  
}
