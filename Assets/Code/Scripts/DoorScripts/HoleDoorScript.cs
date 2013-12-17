using UnityEngine;
using System.Collections;

public class HoleDoorScript : MonoBehaviour {

  class HoleDoor : Door {
  
    void HoleDoor( GameObject d, Transform i, Transform o ) : base( d, i, o ) {}
  
    override void AnimateIn() {
      Level.currentLevel.MovePlayer( outBlocking.position );
      base.AnimateIn();
    }
  
    override void AnimateOut() {
      Level.currentLevel.MovePlayer( inBlocking.position );
      base.AnimateOut();
    }
  }

  public GameObject destDoor;
  public Transform inBlocking;
  public Transform outBlocking;

  public HoleDoor door;

  void Awake() {
    door = new HoleDoor( destDoor, inBlocking, outBlocking );
  }

  void OnMouseOver() {
    if( Input.GetMouseButtonDown(0) ) StartCoroutine( door.Enter() );
  }
}