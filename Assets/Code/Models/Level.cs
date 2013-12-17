using UnityEngine;
using System.Collections;

public class Level {
  static public Level currentLevel;
  
  public string lvlName;
  public GameObject currentRoom;
  public GameObject player;
  
  private NavMeshAgent navAgent;
  
  public Door usedDoor;
  
  public Level( string n, GameObject r, GameObject p ) {
    lvlName = n;
    player = p;
    navAgent = player.GetComponent( "NavMeshAgent" ) as NavMeshAgent;
    currentRoom = r;
    foreach( GameObject room in GameObject.FindGameObjectsWithTag( "Room" ) ) room.SetActive( false );
    currentRoom.SetActive( true );
    Level.currentLevel = this;
  }

  public void ChangeRoom() {
    currentRoom = usedDoor.destRoom;
    foreach( GameObject room in GameObject.FindGameObjectsWithTag( "Room" ) ) room.SetActive( false );
    currentRoom.SetActive( true );
    TeleportPlayer( usedDoor.DestBlocking() );
    usedDoor.DestAnimate();
  }
  
  public void TeleportPlayer( Vector3 d ) {
    navAgent.enabled = false;
    player.transform.position = d;
    navAgent.enabled = true;
  }
  
  public void MovePlayer( Vector3 d ) {
    navAgent.destination = d;
  }

  public bool PlayerInMotion() {
     return navAgent.hasPath || navAgent.pathPending;
  }
}
