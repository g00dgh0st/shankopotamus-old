using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

  public float characterScale = 1f;
  public yBoundary[] yBounds;
  
  public int GetNewOrder( float pos ) {
    if( yBounds.Length <= 0 ) return 0;
    
    yBoundary closest = yBounds[0];
    
    for( int i = 1; i < yBounds.Length; i++ ) {
      if( Mathf.Abs( pos - yBounds[i].pos ) < Mathf.Abs( pos - closest.pos ) ) closest = yBounds[i];
    }

    if( pos > closest.pos ) 
      return closest.orderAbove;
    else 
      return closest.orderBelow;
  }
}

[System.Serializable]
public class yBoundary {
  public float pos;
  public int orderAbove;
  public int orderBelow;
}
