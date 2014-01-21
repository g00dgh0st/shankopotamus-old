using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
  public ArrayList items;
  
  public void Start() {
    items = new ArrayList();
  }
  
  public void OnGUI() {
    foreach( Item i in items ) {
      GUI.Button( new Rect( Screen.width - 40, 0, 40, 40 ), i.image );
    }
  }
  
  public void AddItem( Item i ) {
    items.Add( i );
  }

  public void RemoveItem( Item i ) {
    items.Remove( i );
  }
  
}
