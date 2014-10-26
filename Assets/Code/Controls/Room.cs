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
  
  
  // misc nav stuff
  public bool lockToBottom = false;
  public float navRadius = 0.0f;
  public float lookAhead = 0.5f;
  
  // Ambient light color
  public Color ambientLight = new Color( 228f, 228f, 228f, 255f );
  
  // TODO color bounds
  public colorBoundary[] colorBounds;
  
  void Start() {
    SetUp();
  }
  
  void OnEnable() {
    SetUp();
  }
  
  void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
 	
    foreach( yBoundary bound in yBounds ) {
      if( bound.boundType == yBoundary.BoundType.Dynamic )
        Gizmos.DrawLine( bound.dynamicTrans1.position, bound.dynamicTrans2.position );
      else
        Gizmos.DrawLine( bound.pos1, bound.pos2 );
    }
  
 }
  
  private void SetUp() {
    if( Game.currentRoom == gameObject ) {
      PolyNav2D.current.masterCollider = transform.Find( "Navigation" ).GetComponent<PolygonCollider2D>();
      PolyNav2D.current.inflateRadius = navRadius;
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
public class colorBoundary {
  public string name;
  
  public enum BoundType{ GreaterThan, LessThan };
  public BoundType boundType = BoundType.GreaterThan;
  
  public enum BoundAxis{ X, Y };
  public BoundAxis boundAxis = BoundAxis.X;
  
  public float boundValue;
  
  public Color ambientColor;
}


[System.Serializable]
public class yBoundary {
  public string name;

  public enum BoundType { Static, Dynamic };
  public BoundType boundType = BoundType.Static;
  public Transform dynamicTrans1;
  public Transform dynamicTrans2;

  public Vector2 pos1;
  public Vector2 pos2;

  public int orderAbove;
  public int orderBelow;
  
  public Vector2 DistanceToPoint( Vector2 point ) {
    Vector2 p1;
    Vector2 p2;
    
    if( boundType == BoundType.Dynamic ) {
      p1 = dynamicTrans1.position;
      p2 = dynamicTrans2.position;
    } else {
      p1 = pos1;
      p2 = pos2;
    }
    
    Vector2 a = point - p1;
    Vector2 n = ( p2 - p1 ).normalized;

    float d = Vector2.Distance( p1, p2 );
    float t = Vector2.Dot( n, a );

    if( t <= 0f ) return p1 - point;

    if( t >= d ) return p2 - point;

    Vector2 b = n * t;

    Vector2 closestPoint = p1 + b;

    return closestPoint - point;
  }
}
