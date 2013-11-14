#pragma strict

public var lvlName : String;
public var startRoom : GameObject;
public var player : GameObject;

private var level : Level; 

function Start() {
  level = new Level( lvlName, startRoom, player );
}

function Update() {

}
