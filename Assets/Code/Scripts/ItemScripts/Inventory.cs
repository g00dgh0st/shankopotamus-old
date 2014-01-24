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
    
    items.Add( i.GetComponent<Item>() );
        
    // put into grid
    newItem.transform.parent = grid.transform;
    newItem.transform.localScale = new Vector3( 1, 1, 1 );
    grid.GetComponent<UIGrid>().Reposition();
  }

  public void RemoveItem( Item i ) {
    if( currentItem == i ) currentItem = null;
    items.Remove( i );
    Destroy( ( (Item)i ).gameObject );
  }
  
  public void CombineItems( Item a, Item b ) {
    Debug.Log( a );
    Debug.Log( b );
  }
}


