using UnityEngine;
using System.Collections;

public abstract class Door {
  public static float animateTime = 1.5f; // this is used in FadeScript to determine how long to allow for door enter/exit animations

  public GameObject destDoor;
  
  protected GameObject destRoom;
  protected Transform inBlocking;
  protected Transform outBlocking;
  
  public Door( GameObject d, Transform i, Transform o ) {
    destDoor = d;
    inBlocking = i;
    outBlocking = o;
  }
  
  // must be called in coroutine
  public IEnumerator GoIn( GameObject destDoor ) {
    Game.player.MoveTo( inBlocking.position );

    yield return new WaitForSeconds(0.1f);
    while( Game.player.InMotion() ) yield return 0;
    
    AnimateIn();
    
    yield return new WaitForSeconds(0.1f);
    while( Game.player.InMotion() ) yield return 0;
    
    Game.level.ChangeRoom( destDoor );
  }
  
  public void GoOut() {
    Game.player.TeleportTo( outBlocking.position );
    
    AnimateOut();
  }
  
  // These Animate methods should be overridden in each Door's script.
  public abstract void AnimateIn();
  public abstract void AnimateOut();
}