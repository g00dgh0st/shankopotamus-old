using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {
  
  // char scaling stuff
  public enum ScaleType { Static, Depth };
  public ScaleType characterScaleType = ScaleType.Static;
  
  public float characterScale = 1f;
  
  public float backY = 0.0f;
  public float frontY = 0.0f;
  
  public float backScale = 1f;
  public float frontScale = 1f;
  
  // cam control stuff
  public enum CameraType { Static, HorizontalScroll, VerticalScroll, FreeScroll };
  public CameraType cameraType = CameraType.FreeScroll;
  
  public float YLock;
  public float XLock;
  
  // for layer sorting
  public yBoundary[] yBounds;
  
  void Start() {
    SetUp();
  }
  
  void OnEnable() {
    SetUp();
  }
  
  void OnDrawGizmosSelected () {
 	// Display the explosion radius when selected
 	Gizmos.color = Color.red;
 	
  foreach( yBoundary bound in yBounds ) {
    Gizmos.DrawLine( bound.pos1, bound.pos2 );
  }
  
 }
  
  private void SetUp() {
    if( Game.currentRoom == gameObject ) {
      PolyNav2D.current.masterCollider = transform.Find( "Navigation" ).GetComponent<PolygonCollider2D>();
      PolyNav2D.current.GenerateMap();
    }
  }
  
  public int GetNewOrder( Vector2 pos ) {
    if( yBounds.Length <= 0 ) return 0;
    
    yBoundary closest = yBounds[0];
    
    for( int i = 1; i < yBounds.Length; i++ ) {
      if( Mathf.Abs( yBounds[i].DistanceToPoint( pos ).magnitude ) < Mathf.Abs( closest.DistanceToPoint( pos ).magnitude ) ) 
        closest = yBounds[i];
    }

    if( closest.DistanceToPoint( pos ).y < 0 ) 
      return closest.orderAbove;
    else 
      return closest.orderBelow;
  }
}


[System.Serializable]
public class yBoundary {
  public string name;
  public Vector2 pos1;
  public Vector2 pos2;
  public int orderAbove;
  public int orderBelow;
  
  public Vector2 DistanceToPoint( Vector2 point ) {
    Vector2 a = point - pos1;
    Vector2 n = ( pos2 - pos1 ).normalized;

    float d = Vector2.Distance( pos1, pos2 );
    float t = Vector2.Dot( n, a );

    if( t <= 0f ) return pos1 - point;

    if( t >= d ) return pos2 - point;

    Vector2 b = n * t;

    Vector2 closestPoint = pos1 + b;

    return closestPoint - point;
  }
}
