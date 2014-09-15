using UnityEngine;
using System.Collections;

public class SkipButton : MonoBehaviour {

  void OnClick() {
    Application.LoadLevel( 2 );
  }
  
}
