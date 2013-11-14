
class Door {
  public var destDoor : GameObject;
  public var destRoom : GameObject;
  public var inBlocking : Transform;
  public var outBlocking : Transform;
  
  static var animateTime : float = 1.5; // this is used in FadeScript to determine how long to allow for door enter/exit animations
  
  function Door( d : GameObject, i : Transform, o : Transform ) {
    destDoor = d;
    destRoom = destDoor.transform.parent.gameObject;
    inBlocking = i;
    outBlocking = o;
  }
  
  // must be called in coroutine
  function Enter() {
    Level.currentLevel.MovePlayer( inBlocking.position );
    
    while( Level.currentLevel.PlayerInMotion() ) yield;
    
    AnimateIn();
  }
  
  function DestBlocking() : Vector3 {
    return destDoor.GetComponent( MonoBehaviour ).door.outBlocking.position;
  }
  
  function DestAnimate() {
    destDoor.GetComponent( MonoBehaviour ).door.AnimateOut();
  }

  // These Animate methods should be overridden in each Door's script.
  // super() should be called after animation
  function AnimateIn()  { Level.currentLevel.usedDoor = this; }
  function AnimateOut() { Level.currentLevel.usedDoor = null; }
}