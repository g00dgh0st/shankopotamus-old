using UnityEngine;
using System.Collections;

public class NavClicker : MonoBehaviour {
  
  private bool isPressed = false;
  
  void Update() {
    if( isPressed )
  		Game.player.MoveTo( Camera.main.ScreenToWorldPoint( Input.mousePosition ) );
      
  }
 
  void OnPress( bool press ) {
    isPressed = press;
  }
}