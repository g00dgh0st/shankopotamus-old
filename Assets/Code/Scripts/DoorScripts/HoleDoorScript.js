#pragma strict

class HoleDoor extends Door {
  
  function HoleDoor( d : GameObject, i : Transform, o : Transform ) {
    super( d, i, o );
  }
  
  function AnimateIn() {
    Level.currentLevel.MovePlayer( outBlocking.position );
    super();
  }
  
  function AnimateOut() {
    Level.currentLevel.MovePlayer( inBlocking.position );
    super();
  }
}

public var destDoor : GameObject;
public var inBlocking : Transform;
public var outBlocking : Transform;

public var door : HoleDoor;

function Awake() {
  door = new HoleDoor( destDoor, inBlocking, outBlocking );
}

function OnMouseOver() {
  if( Input.GetMouseButtonDown( 0 ) ) StartCoroutine( door.Enter() );
}
