using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
  
  public bool isStart = false;
  
  public void OnClick() {
    if( isStart ) {
      Application.LoadLevel( 1 );
    } else {
      Debug.Log( "FDSF" );
      Application.Quit();
    }
  }
}
