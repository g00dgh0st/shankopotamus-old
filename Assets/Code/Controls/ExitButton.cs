using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {
  
  void Start() {
    if( Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer ) {
      Destroy( gameObject );
    }
  }
  
  public void OnClick() {
    GameObject obj = GameObject.Instantiate( Resources.Load( "ExitConfirm" ) ) as GameObject;
    obj.transform.parent = GameObject.Find( "UI Root" ).transform;
    obj.transform.localScale = new Vector3( 1f, 1f, 1f );
    obj.transform.localPosition = new Vector3( 1f, 1f, 1f );
    obj.name = "ExitConfirm";
  }
}
