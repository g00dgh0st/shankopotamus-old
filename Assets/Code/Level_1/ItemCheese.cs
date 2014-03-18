using UnityEngine;
using System.Collections;

public class ItemCheese : MonoBehaviour {

  void OnClick() {
    Game.script.AddItem( "cheese" );
    Destroy( gameObject );
  }
}
