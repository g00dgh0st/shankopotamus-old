using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
  public static string lvlName;
  public static GameObject currentRoom;
  
  public void Start() {
    foreach( GameObject room in GameObject.FindGameObjectsWithTag( "Room" ) ) room.SetActive( false );
    currentRoom.SetActive( true );
  }
  
  public static void ChangeRoom( Door destDoor ) {
    currentRoom = destDoor.GetDestRoom();
    foreach( GameObject room in GameObject.FindGameObjectsWithTag( "Room" ) ) room.SetActive( false );
    
    Game.FadeOut();
    
    currentRoom.SetActive( true );
    destDoor.GoOut();
  }
}
