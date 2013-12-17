using UnityEngine;
using System.Collections;

public class Door {
  public static float animateTime = 1.5f; // this is used in FadeScript to determine how long to allow for door enter/exit animations

  public GameObject destDoor;
  
  
  public GameObject destRoom;
  public Transform inBlocking;
  public Transform outBlocking;
  
  private Door destDoorObj;
  
  
  public Door( GameObject d, Transform i, Transform o ) {
    destDoor = d;
    destRoom = destDoor.transform.parent.gameObject;
    inBlocking = i;
    outBlocking = o;

    Object dd = destDoor.GetComponent<MonoBehaviour>();
    destDoorObj = dd.GetType().GetMethod( "door" ).Invoke( dd, null ) as Door;
  }
  
  // must be called in coroutine
  public IEnumerator Enter() {
    Level.currentLevel.MovePlayer( inBlocking.position );
    
    while( Level.currentLevel.PlayerInMotion() ) yield return 0;
    
    AnimateIn();
  }
  
  public Vector3 DestBlocking() {
    return (Vector3)destDoorObj.outBlocking.position;
  }
  
  public void DestAnimate() {
    destDoorObj.AnimateOut();
  }

  // These Animate methods should be overridden in each Door's script.
  // base should be called after animation
  public virtual void AnimateIn()  { Level.currentLevel.usedDoor = this; }
  public virtual void AnimateOut() { Level.currentLevel.usedDoor = null; }
}