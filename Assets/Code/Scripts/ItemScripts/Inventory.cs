using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
  public ArrayList items;
  
  public Item currentItem;
  
  private GameObject grid;
  
  
  public void Start() {
    items = new ArrayList();
    grid = GameObject.Find( "InventoryGrid" );
  }
  
  public void AddItem( GameObject i ) {
    GameObject newItem = Instantiate( i ) as GameObject;
    
    items.Add( i.GetComponent<InventoryItem>().item );
        
    // put into grid
    newItem.transform.parent = grid.transform;
    grid.GetComponent<UIGrid>().Reposition();
  }

  // public void RemoveItem( Item i ) {
  //   if( currentItem == i ) currentItem = null;
  //   Destroy( i.invObject );
  //   i.invObject = null;
  //   items.Remove( i );
  // }
  // 
}
