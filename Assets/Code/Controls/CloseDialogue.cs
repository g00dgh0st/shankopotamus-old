using UnityEngine;
using System.Collections;

public class CloseDialogue : MonoBehaviour {
  
  public bool isClose = false;
  
  public void OnClick() {
    if( !isClose ) {
      Destroy( GameObject.Find( "ExitConfirm" ) );
    } else {
      StartCoroutine( Quittin() );
    }
  }
  
  IEnumerator Quittin() {
    GameObject.Find( "ExitConfirm" ).transform.Find( "BG" ).transform.Find( "Title" ).GetComponent<UILabel>().text = "Go to hell.";
    
    yield return new WaitForSeconds( 0.05f );
    
    Application.LoadLevel( 0 );
  }
}
