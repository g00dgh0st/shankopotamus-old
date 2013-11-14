#pragma strict

public var playerTrans : Transform;
public var rBound : Transform;
public var lBound : Transform;
public var tBound : Transform;
public var bBound : Transform;
public var yOff : float = 10;

function Update () {
  if( !gameObject.activeInHierarchy ) return;
  var camPos = transform.position;
  var playerPos = playerTrans.position;
  
  if( !( ( rBound != null && camera.WorldToScreenPoint( rBound.position ).x <= camera.pixelWidth && playerPos.x > camPos.x ) || ( lBound != null && camera.WorldToScreenPoint( lBound.position ).x >= 0 && playerPos.x < camPos.x ) ) )
    transform.position.x = playerPos.x;
    
  if( !( ( tBound != null && camera.WorldToScreenPoint( tBound.position ).y >= 0 && playerPos.y > camPos.y ) || ( bBound != null && camera.WorldToScreenPoint( bBound.position ).y <= camera.pixelHeight && playerPos.y < camPos.y ) ) )
    transform.position.y = playerPos.y + yOff;
}