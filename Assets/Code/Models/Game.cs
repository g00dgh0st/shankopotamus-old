using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
  public static Player player;
  public static Level level;
  public static DialogueManager dialogueManager;
  public static Inventory inventory;
  public static MonoBehaviour script;
  
  
  public string levelName;
  public GameObject startRoom;
  
  // For fading
  public static float aLerp;
  public static float aLerpSpeed;
  public static Texture2D blackTex;
  public static int isFading = 0;

  public void Awake() { Application.targetFrameRate = 60; }
  
  public void Start() {
    player = (Player)GameObject.FindGameObjectsWithTag( "Player" )[0].GetComponent( "Player" );
    level = new Level( levelName, startRoom );
    dialogueManager = gameObject.GetComponent( "DialogueManager" ) as DialogueManager;
    inventory = gameObject.GetComponent( "Inventory" ) as Inventory;
    script = this;
    
    aLerp = 0.0f;
    aLerpSpeed = 0.05f;
    blackTex = Resources.Load( "blackPxl" ) as Texture2D;
  }
  
  public void OnGUI() {
    if( aLerp <= 1.0f && Game.isFading == 1 ) {
      GUI.color = new Color( GUI.color.r, GUI.color.g, GUI.color.b, aLerp );
      GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), blackTex );
      aLerp += aLerpSpeed;
    } else if( aLerp > 1.0f && Game.isFading == 1 ) {
      StartCoroutine( DelayFadeIn( 0.6f ) );
      aLerp = 1.0f;
    }
    
    if( aLerp >= 0.0f && Game.isFading == 2 ) {
      GUI.color = new Color( GUI.color.r, GUI.color.g, GUI.color.b, aLerp );
      GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), blackTex );
      aLerp -= aLerpSpeed;
    } else if( aLerp < 0.0f && Game.isFading == 2 ) {
      Game.isFading = 0;
    }
  }
    
  public IEnumerator DelayFadeIn( float delay ) {
    yield return new WaitForSeconds( delay );
    Game.isFading = 2;
  }

// STATIC METHODS

  public delegate void Callback();
  
  public static IEnumerator FadeCamera( Callback cBack ) {
    isFading = 1;
    aLerp = 0.0f;
    
    while( isFading != 2 ) {
      yield return 0;
    }
    
    cBack();
  }
  
  public static void PauseClicks() {
    Camera.main.transform.Find( "ClickOverlay" ).gameObject.SetActive( true );
  }
  
  public static void ResumeClicks() {
    Camera.main.transform.Find( "ClickOverlay" ).gameObject.SetActive( false );
  }
  
  public static void PauseCam() {
    Game.level.currentRoom.transform.Find( "CamTrans" ).gameObject.SetActive( false );
    foreach( Parallax p in FindObjectsOfType( typeof( Parallax ) ) ) p.enabled = false;
  }
  
  public static void ResumeCam() {
    Camera.main.orthographicSize = 5f;
    Game.level.currentRoom.transform.Find( "CamTrans" ).gameObject.SetActive( true );
    foreach( Parallax p in FindObjectsOfType( typeof( Parallax ) ) ) p.enabled = true;
  }

}