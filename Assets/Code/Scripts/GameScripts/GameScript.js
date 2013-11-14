#pragma strict
// this script is for handling global game scripting

function Awake() {
  Application.targetFrameRate = 60;
}

function Start() {
  // load global scripts here
  gameObject.AddComponent( "FadeScript" );
}