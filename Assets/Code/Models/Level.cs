using UnityEngine;
using System.Collections;

public class Level {
  public string lvlName;
  public GameObject currentRoom;
  
  public Level( string n, GameObject r ) {
    currentRoom = r;
    lvlName = n;
    foreach( GameObject room in GameObject.FindGameObjectsWithTag( "Room" ) ) room.SetActive( false );
    currentRoom.SetActive( true );
  }
  
  public IEnumerator ChangeRoom( GameObject destDoor ) {
    currentRoom.SetActive( false );
    currentRoom = destDoor.transform.parent.gameObject;
    
    // Game.Fade(); TODO MAKE THIS FADE
    
    yield return new WaitForSeconds(0.3f);
    
    currentRoom.SetActive( true );
    
    MonoBehaviour destScript = (MonoBehaviour)destDoor.GetComponent( "MonoBehaviour" );
    
    switch( destScript.GetType().ToString() ) {
      case "BasicDoor":
        BasicDoor bd = destScript as BasicDoor;
        bd.door.GoOut();
        break;
      default:
        break;
    }
  }
}
