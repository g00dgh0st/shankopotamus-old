using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
  public ArrayList items;
  
  public Item currentItem;
  
  public void Start() {
    items = new ArrayList();
  }
  
  public void OnGUI() {
    if( Input.GetMouseButtonUp( 1 ) && currentItem != null ) currentItem = null;
    
    foreach( Item i in items ) {
      if( i == currentItem ) continue;
      if( GUI.Button( new Rect( Screen.width - 40, 0, 40, 40 ), i.image ) ) {
        currentItem = i;
      }
    }
    
    if( currentItem != null ) {
      Screen.showCursor = false;
      GUI.Label( new Rect( Input.mousePosition.x - 20, Screen.height - Input.mousePosition.y - 20, 40, 40 ), currentItem.image );
    } else if( Screen.showCursor == false ) 
      Screen.showCursor = true;
  }
  
  public void AddItem( Item i ) {
    items.Add( i );
  }

  public void RemoveItem( Item i ) {
    if( currentItem == i ) currentItem = null;
    items.Remove( i );
  }
  
}
