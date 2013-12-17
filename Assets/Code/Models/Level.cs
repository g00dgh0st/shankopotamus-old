using UnityEngine;
using System.Collections;

class Level {
  static public Level currentLevel;
  
  public string lvlName;
  public GameObject currentRoom;
  public GameObject player;
  
  private NavMeshAgent navAgent;
  
  public Door usedDoor;
  
  void Level( string n, GameObject r, GameObject p ) {
    lvlName = n;
    player = p;
    navAgent = player.GetComponent<"NavMeshAgent">() as NavMeshAgent;
    currentRoom = r;
    for( var room in GameObject.FindGameObjectsWithTag( "Room" ) ) room.SetActive( false );
    currentRoom.SetActive( true );
    Level.currentLevel = this;
  }

  void ChangeRoom() {
    currentRoom = usedDoor.destRoom;
    for( var room in GameObject.FindGameObjectsWithTag( "Room" ) ) room.SetActive( false );
    currentRoom.SetActive( true );
    TeleportPlayer( usedDoor.DestBlocking() );
    usedDoor.DestAnimate();
  }
  
  void TeleportPlayer( Vector3 d ){
    navAgent.enabled = false;
    player.transform.position = d;
    navAgent.enabled = true;
  }
  
  void MovePlayer( Vector3 d ){
    navAgent.destination = d;
  }

  bool PlayerInMotion() {
     return navAgent.hasPath || navAgent.pathPending;
  }
}
