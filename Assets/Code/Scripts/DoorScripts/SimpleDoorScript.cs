using UnityEngine;
using System.Collections;

public class SimpleDoorScript : MonoBehaviour {
  
  public class SimpleDoor : Door {
  
    public SimpleDoor( GameObject d, Transform i, Transform o ) : base( d, i, o ) {}
  
    public override void AnimateIn() {
      Level.currentLevel.MovePlayer( outBlocking.position );
      base.AnimateIn();
    }
  
    public override void AnimateOut() {
      Level.currentLevel.MovePlayer( inBlocking.position );
      base.AnimateOut();
    }
  }

  public GameObject destDoor;
  public Transform inBlocking;
  public Transform outBlocking;

  public SimpleDoor door;

  public void Awake() {
    door = new SimpleDoor( destDoor, inBlocking, outBlocking );
  }

  public void OnMouseOver() {
    if( Input.GetMouseButtonDown(0) ) StartCoroutine( door.Enter() );
  }
}