using UnityEngine;
using System.Collections;

public class InventoryItem : MonoBehaviour {
  
  public Item item;
  public string itemName;
  public Sprite itemSprite;
  
  public void Start() {
    item = new Item( itemName, itemSprite );
  }
  
  public void OnClick() {
    Debug.Log( item.name );
  }
}
