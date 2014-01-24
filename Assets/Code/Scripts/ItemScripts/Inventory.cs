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
  
  public void Update() {
    if( currentItem != null ) {
      Screen.showCursor = false;
      
      Vector3 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
      currentItem.gameObject.transform.position = new Vector3( mousePos.x, mousePos.y, 0 );

      if( Input.GetMouseButton( 1 ) ) currentItem = null;
    } else if( Screen.showCursor == false ) {
      Screen.showCursor = true;
      RepositionGrid();
    }
  }
  
  public void AddItem( GameObject i ) {
    
    GameObject newItem = Instantiate( i ) as GameObject;
    
    items.Add( i.GetComponent<Item>() );
        
    // put into grid
    newItem.transform.parent = grid.transform;
    newItem.transform.localScale = new Vector3( 1, 1, 1 );
    RepositionGrid();
  }

  public void RemoveItem( Item i ) {
    if( currentItem == i ) currentItem = null;
    items.Remove( i );
    Destroy( ( (Item)i ).gameObject );
    RepositionGrid();
  }
  
  public void UseCurrentItem() {
    RemoveItem( currentItem );
  }
  
  public void CombineItems( Item a, Item b ) {
    Debug.Log( a );
    Debug.Log( b );
  }
  
  public string CurrentItemName() {
    if( currentItem != null )
      return currentItem.name;
    else
      return null;
  }
  
  
  
  private void RepositionGrid() {
    grid.GetComponent<UIGrid>().Reposition();
  }
  
}


