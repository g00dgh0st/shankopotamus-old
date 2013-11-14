#pragma strict

class Level {
  static public var currentLevel : Level;
  
  public var lvlName  : String;
  public var currentRoom : GameObject;
  public var player : GameObject;
  
  private var navAgent : NavMeshAgent;
  
  public var usedDoor : Door;
  
  function Level( n : String, r : GameObject, p : GameObject ) {
    lvlName = n;
    player = p;
    navAgent = player.GetComponent( "NavMeshAgent" ) as NavMeshAgent;
    currentRoom = r;
    for( var room in GameObject.FindGameObjectsWithTag( "Room" ) ) room.SetActive( false );
    currentRoom.SetActive( true );
    Level.currentLevel = this;
  }

  function ChangeRoom() {
    currentRoom = usedDoor.destRoom;
    for( var room in GameObject.FindGameObjectsWithTag( "Room" ) ) room.SetActive( false );
    currentRoom.SetActive( true );
    TeleportPlayer( usedDoor.DestBlocking() );
    usedDoor.DestAnimate();
  }
  
  function TeleportPlayer( d : Vector3 ) {
    navAgent.enabled = false;
    player.transform.position = d;
    navAgent.enabled = true;
  }
  
  function MovePlayer( d : Vector3 ) {
    navAgent.destination = d;
  }

  function PlayerInMotion() : boolean {
    return navAgent.hasPath || navAgent.pathPending;
  }
}