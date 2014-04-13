using UnityEngine;
using System.Collections;

public class KeypadButton : MonoBehaviour {

  public int number;

  void OnClick() {
    transform.parent.gameObject.GetComponent<Keypad>().PressButton( number );
  }
}