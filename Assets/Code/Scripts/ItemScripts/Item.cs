using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
  
  public string itemName;
  
  public void OnClick() {
    Item curr = Game.inventory.currentItem;
    if( curr == null ) {
      Game.inventory.currentItem = this;
      collider2D.enabled = false;
    } else
      Game.inventory.CombineItems( curr, this );
  }
}
