using UnityEngine;
using System.Collections;

public class Door {

  public GameObject destDoor;
  public GameObject destRoom;
  public Transform inBlocking;
  public Transform outBlocking;
  
  static float animateTime = 1.5f; // this is used in FadeScript to determine how long to allow for door enter/exit animations
  
  void Door( GameObject d, Transform i, Transform o ) {
    destDoor = d;
    destRoom = destDoor.transform.parent.gameObject;
    inBlocking = i;
    outBlocking = o;
  }
  
  // must be called in coroutine
  void Enter() {
    Level.currentLevel.MovePlayer( inBlocking.position );
    
    while( Level.currentLevel.PlayerInMotion() ) yield return 0;
    
    AnimateIn();
  }
  
  Vector3 DestBlocking() {
    return destDoor.GetComponent<MonoBehaviour>().door.outBlocking.position;
  }
  
  void DestAnimate() {
    destDoor.GetComponent<MonoBehaviour>().door.AnimateOut();
  }

  // These Animate methods should be overridden in each Door's script.
  // super() should be called after animation
  virtual void AnimateIn() { Level.currentLevel.usedDoor = this; }
  virtual void AnimateOut() { Level.currentLevel.usedDoor = null; }
}