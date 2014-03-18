using UnityEngine;
using System.Collections;

public class InventoryPanel : MonoBehaviour {

  void Hover( bool isOver ) {
    if( isOver ) {
      transform.Find( "Inventory" ).localPosition = new Vector3( 25f, 25f, 0f );
    } else {
      transform.Find( "Inventory" ).localPosition = new Vector3( 75f, 25f, 0f );
    }
  }
}
