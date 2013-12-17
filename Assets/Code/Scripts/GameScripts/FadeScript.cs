using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour {

  // This script handles fading the screen in and out when changing rooms

  private Texture2D blackPxl;
  private float aLerp = 0.0f;
  private float aLerpSpeed;

  void Start() {
    blackPxl = Resources.Load( "Art/blackPxl" ) as Texture2D;
    aLerpSpeed = 0.01f / Door.animateTime;
  }

  void OnGUI() {
    if( Level.currentLevel == null ) return;
    if( Level.currentLevel.usedDoor != null ) { // this flag is thrown on in the Door.Animate() function
      if( aLerp < 1.0f ) aLerp += aLerpSpeed;
      GUI.color.a = aLerp;
      GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), blackPxl );
      if( !isFading ) StartCoroutine( "RoomFade" );
    } else if( aLerp > 0.0f && Level.currentLevel.usedDoor == null ) {
      if( aLerp >= 0.0f ) aLerp -= aLerpSpeed;
      else aLerp = 0.0f;
      GUI.color.a = aLerp;
      GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), blackPxl );
    } 
  }

  private bool isFading = false;
  void RoomFade() { 
    isFading = true;
    yield return new WaitForSeconds( Door.animateTime ); 
    Level.currentLevel.ChangeRoom();
    isFading = false;
    dispatcher.eventNotify(dlEvents.DOORENTER, "");
  }
}