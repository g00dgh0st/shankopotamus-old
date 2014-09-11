using UnityEngine;
using System.Collections;

public class ItemClicker : MonoBehaviour {
  public string name;
  public string label;
  public string description;
  public ItemCombo[] combos;
  
  void OnClick() {
    Game.script.HoldItem( gameObject );
  }
  
  void OnItemClick() {
    foreach( ItemCombo combo in combos ) {
      if( combo.combine == Game.heldItem.GetComponent<ItemClicker>().name ) {
        Game.script.AddItem( combo.result );
        Game.script.UseItem();
        Game.script.RemoveItem( name );
        break;
      }
    }
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, "HandCursor" );
  }
}
