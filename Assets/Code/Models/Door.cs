using UnityEngine;
using System.Collections;

public class Door {
  public static float animateTime = 1.5f; // this is used in FadeScript to determine how long to allow for door enter/exit animations

  public GameObject destDoor;
  
  protected GameObject destRoom;
  protected Transform inBlocking;
  protected Transform outBlocking;
  
  public Door( GameObject d, Transform i, Transform o ) {
    destDoor = d;
    destRoom = destDoor.transform.parent.gameObject;
    inBlocking = i;
    outBlocking = o;
  }
  
  // must be called in coroutine
  public IEnumerator GoIn() {
    Game.player.MoveTo( inBlocking.position );
    
    while( Game.player.InMotion() ) yield return 0;
    
    AnimateIn();
  }
  
  public void GoOut() {
    Game.player.TeleportTo( outBlocking.position );
    
    AnimateOut();
  }
  
  // These Animate methods should be overridden in each Door's script.
  // base should be called after animation
  public virtual void AnimateIn()  { 
    Object destScript = destDoor.GetComponent( "MonoBehaviour" );
    Level.ChangeRoom( (Door)destScript.GetType().GetMethod( "door" ).Invoke( destScript, null ) );
  }
  public virtual void AnimateOut() { 
    // Level.currentDoor = null; 
  }
  
  
  
  public GameObject GetDestRoom() {
    return destRoom;
  }
}