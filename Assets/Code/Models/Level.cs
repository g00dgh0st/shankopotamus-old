using UnityEngine;
using System.Collections;

public class Level {
  public string lvlName;
  public GameObject currentRoom;
  private MonoBehaviour destScript;
  
  public Level( string n, GameObject r ) {
    currentRoom = r;
    lvlName = n;
    foreach( GameObject room in GameObject.FindGameObjectsWithTag( "Room" ) ) room.SetActive( false );
    currentRoom.SetActive( true );
    Camera.main.transform.position = currentRoom.transform.Find( "CamTrans" ).position;
  }
  
  public void ChangeRoom( GameObject destDoor ) {
    destDoor.transform.parent.gameObject.SetActive( true );
    destScript = (MonoBehaviour)destDoor.GetComponent( "MonoBehaviour" );
    destScript.StartCoroutine( Game.FadeCamera( MoveCamToNewRoom ) );
  }
  
  public void MoveCamToNewRoom() {
    currentRoom.SetActive( false );
    currentRoom = destScript.transform.parent.gameObject;
    Camera.main.transform.position = currentRoom.transform.Find( "CamTrans" ).position;
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
