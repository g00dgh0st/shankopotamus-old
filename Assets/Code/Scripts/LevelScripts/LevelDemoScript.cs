using UnityEngine;
using System.Collections;

public class LevelDemoScript : MonoBehaviour {

  public string lvlName;
  public GameObject startRoom;
  public GameObject player;

  private Level level; 

  public void Start() {
    level = new Level( lvlName, startRoom, player );
  }
}