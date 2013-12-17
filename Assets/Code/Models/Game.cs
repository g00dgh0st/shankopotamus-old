using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
  public static Player player;
  
  public void Awake() { Application.targetFrameRate = 60; }
  
  public void Start() {
    player = (Player)GameObject.FindGameObjectsWithTag( "Player" )[0].GetComponent( "Player" );
    gameObject.AddComponent( "FadeEffect" );
  }
  
  public static void FadeOut() {
    // gameObject.GetComponent( "FadeEffect" ).FadeOut();
  }
}