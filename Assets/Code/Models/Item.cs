using UnityEngine;
using System.Collections;

public class Item {
  
  public string name;
  public Texture2D image;
  
  public Item( string n, Texture2D t ) {
    name = n;
    image = t;
  }
}
