#pragma strict
// This script handles fading the screen in and out when changing rooms

private var blackPxl : Texture2D;
private var aLerp : float = 0.0;
private var aLerpSpeed : float;

var dialogMainText = '';
var button1Text = '';
var button2Text = '';

function Start() {
  blackPxl = Resources.Load( "Art/blackPxl" ) as Texture2D;
  aLerpSpeed = 0.01 / Door.animateTime;

  button1Text = "Lowly!?!?";
  button2Text = "Is that me?";
  dialogMainText = "Once there was a lowly square.";
}

function OnGUI() {

  if( Level.currentLevel == null ) return;
  if( Level.currentLevel.usedDoor != null ) { // this flag is thrown on in the Door.Animate() function
    if( aLerp < 1.0 ) aLerp += aLerpSpeed;
    GUI.color.a = aLerp;
    GUI.DrawTexture( Rect( 0, 0, Screen.width, Screen.height ), blackPxl );
    if( !isFading ) StartCoroutine( "RoomFade" );
  } else if( aLerp > 0.0 && Level.currentLevel.usedDoor == null ) {
    if( aLerp >= 0.0 ) aLerp -= aLerpSpeed;
    else aLerp = 0.0;
    GUI.color.a = aLerp;
    GUI.DrawTexture( Rect( 0, 0, Screen.width, Screen.height ), blackPxl );
  } 
}




private var isFading : boolean = false;
function RoomFade() { 
  isFading = true;
  yield WaitForSeconds( Door.animateTime ); 
  Level.currentLevel.ChangeRoom();
  isFading = false;
  dispatcher.eventNotify(dlEvents.DOORENTER, "");
}