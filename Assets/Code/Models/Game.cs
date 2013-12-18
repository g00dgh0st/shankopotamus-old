using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
  public static Player player;
  public static Level level;
  public static MonoBehaviour script;
  
  public string levelName;
  public GameObject startRoom;
  
  public void Awake() { Application.targetFrameRate = 60; }
  
  public void Start() {
    player = (Player)GameObject.FindGameObjectsWithTag( "Player" )[0].GetComponent( "Player" );
    level = new Level( levelName, startRoom );
    script = this;
  }
}