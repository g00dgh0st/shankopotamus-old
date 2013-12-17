using UnityEngine;
using System.Collections;

public class BasicDoor : MonoBehaviour {
  
  public class BasicDoorObj : Door {
  
    public BasicDoorObj( GameObject d, Transform i, Transform o ) : base( d, i, o ) {}
  
    public override void AnimateIn() {
      Game.player.MoveTo( outBlocking.position );
      base.AnimateIn();
    }
  
    public override void AnimateOut() {
      Game.player.MoveTo( inBlocking.position );
      base.AnimateOut();
    }
  }

  public GameObject destDoor;

  private BasicDoorObj door;

  public void Start() {
    Transform inBlock = transform.Find( "inBlocking" );
    Transform outBlock = transform.Find( "outBlocking" );
    
    door = new BasicDoorObj( destDoor, inBlock, outBlock );
  }

  public void OnMouseOver() {
    if( Input.GetMouseButtonDown(0) ) StartCoroutine( door.GoIn() );
  }
}