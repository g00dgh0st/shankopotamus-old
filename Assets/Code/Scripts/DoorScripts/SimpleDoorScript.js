#pragma strict

class SimpleDoor extends Door {
  
  function SimpleDoor( d : GameObject, i : Transform, o : Transform ) {
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

public var door : SimpleDoor;

function Awake() {
  door = new SimpleDoor( destDoor, inBlocking, outBlocking );
}

function OnMouseOver() {
  if( Input.GetMouseButtonDown( 0 ) ) {
    StartCoroutine( door.Enter() );
  }
}
