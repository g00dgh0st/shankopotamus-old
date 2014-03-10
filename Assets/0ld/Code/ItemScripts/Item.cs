using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
  
  public string itemName;
  public ItemCombo[] itemCombos;
  
  public void OnClick() {
    Item curr = Game.inventory.currentItem;
    if( curr == null ) {
      Game.inventory.currentItem = this;
      collider2D.enabled = false;
    } else {
      GameObject combo = GetCombo( curr.gameObject );

      // combine items
      if( combo != null ) {
        Game.inventory.UseCurrentItem();
        Game.inventory.AddItem( combo );
        Game.inventory.RemoveItem( this );
        /// need some messaging
      } else {
        // no combo
        // error message
      }
      
      
    }
  }
  
  // checks to see if there is a valid combo, returns null if not
  public GameObject GetCombo( GameObject combiner ) {
    foreach( ItemCombo ic in itemCombos ) {
      if( combiner == ic.combinesWith )
        return ic.turnsInto;
    }
    return null;
  } 
  
}

