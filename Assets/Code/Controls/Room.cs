using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

  public float characterScale = 1f;
  public yBoundary[] yBounds;
  
  public enum CameraType { Static, HorizontalScroll, VerticalScroll, FreeScroll };
  
  public CameraType cameraType = CameraType.FreeScroll;
  
  public float XYLock;
  
  void Start() {
    SetUp();
  }
  
  void OnEnable() {
    SetUp();
  }
  
  private void SetUp() {
    if( Game.currentRoom == gameObject ) {
      PolyNav2D.current.masterCollider = transform.Find( "Navigation" ).GetComponent<PolygonCollider2D>();
      PolyNav2D.current.GenerateMap();
    }
  }
  
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
