using UnityEngine;
using System.Collections;

/// Do whatever in here for specific Level 1 functionality

public class Level1 : MonoBehaviour {
  
  public bool lookingForCode = false;
  public bool knowBatCode = false;
  public bool seenWhale = false;
  public bool needBattery = false;

  void Start() {
    // StartCoroutine( Intro() ); 
  }
  
  IEnumerator Intro() {
    Game.PauseClicks();
    
    yield return new WaitForSeconds( 2f );
    
    bool moveDone = false;
    
    Game.player.MoveTo( GameObject.Find( "IntroMove" ).transform.position, delegate( bool b ) { moveDone = true; });
    
    while( moveDone == false ) yield return null;
    
    Game.PauseCam();
    Camera.main.orthographicSize = 0.4f;
    
    Transform chameleon = Game.player.transform.Find( "Chameleon" );
    
    Game.TargetCam( chameleon.Find( "CamTarget" ) );
    
    SpriteRenderer cSprite = chameleon.Find( "chameleon_test" ).GetComponent<SpriteRenderer>();
    
    yield return new WaitForSeconds( 0.8f );
  
    while( cSprite.color.a < 1f ) {
      cSprite.color = new Color( 1f, 1f, 1f, cSprite.color.a + 0.02f );
      yield return null;
    }
    
    Game.dialogueManager.ShowDialogueBubble( "Hey listen, take a look at that." );
    
    yield return new WaitForSeconds( 3f );
    
    Game.TargetCam( GameObject.Find( "SpoonPickup" ).transform );
    
    Game.dialogueManager.ShowDialogueBubble( "If you can distract the guard, sneak that spoon out, sharpen it, baby you got a shank going." );
    
    yield return new WaitForSeconds( 5f );
    
    Game.TargetCam( chameleon.Find( "CamTarget" ) );
    
    Game.dialogueManager.ShowDialogueBubble( "But keep in mind, there's more than one way to make a weapon in this prison. There's at least 327." );
   
    yield return new WaitForSeconds( 5f );
   
    Game.dialogueManager.HideDialogueBubble(); 
    
    Camera.main.orthographicSize = 1f;
    Game.ResumeCam();
    Game.ResumeClicks();
    Game.player.ResumeNav();
    
    while( cSprite.color.a > 0f ) {
      cSprite.color = new Color( 1f, 1f, 1f, cSprite.color.a - 0.02f );
      yield return null;
    }
    
    Destroy( chameleon.gameObject );
  }
}
