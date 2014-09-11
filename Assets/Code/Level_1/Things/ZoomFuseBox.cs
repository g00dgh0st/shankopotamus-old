using UnityEngine;
using System.Collections;

public class ZoomFuseBox : MonoBehaviour {
  
  private bool broken = false;
  
  void OnClick() {
    if( !broken ) {
      transform.Find( "Electric1" ).gameObject.GetComponent<ParticleSystem>().Play();
      transform.Find( "Electric2" ).gameObject.GetComponent<ParticleSystem>().Play();
      Game.GetScript<FuseBox>().broken = true;
      GameObject.Find( "FuseBox" ).transform.Find( "Electric2" ).gameObject.GetComponent<ParticleSystem>().Play();
      StartCoroutine( CloseOut() );
    }
  }
  
  IEnumerator CloseOut() {
    yield return new WaitForSeconds( 1.5f );
    Game.GetScript<ClickOverlay>().OnClick();
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, "HandCursor" );
  }
  
}
