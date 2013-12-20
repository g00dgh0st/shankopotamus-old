using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
  public static Player player;
  public static Level level;
  public static MonoBehaviour script;
  
  public string levelName;
  public GameObject startRoom;
  
  // For fading
  public static float aLerp;
  public static float aLerpSpeed;
  public static ScreenOverlay camOverlay;
  
  public void Awake() { Application.targetFrameRate = 60; }
  
  public void Start() {
    player = (Player)GameObject.FindGameObjectsWithTag( "Player" )[0].GetComponent( "Player" );
    level = new Level( levelName, startRoom );
    script = this;
    
    aLerp = 0.0f;
    aLerpSpeed = 10.0f;
    camOverlay = Camera.main.GetComponent( "ScreenOverlay" ) as ScreenOverlay;
  }
  
  public delegate void Callback();
  
  public static IEnumerator FadeCamera( Callback cBack ) {
    aLerp = 0.0f;
    while( aLerp <= 500.0f ) {
      camOverlay.intensity = aLerp;
      aLerp += aLerpSpeed;
      yield return 0;
    }
    cBack();
    while( aLerp >= 0.0f ) {
      camOverlay.intensity = aLerp;
      aLerp -= aLerpSpeed;
      yield return 0;
    }
  }
}