#pragma strict
// simple stationary camera that rotates to look at player

public var playerTrans : Transform;

function Update () {
  if( !gameObject.activeInHierarchy ) return;
  transform.LookAt( playerTrans );
}