#pragma strict

enum dialogStates {UNDEFINED, INTRO, MOVEREQUEST, MOVEACK, DOORREQUEST, DOORACK, DOORQUIP};

var button1Text : String;
var button2Text : String;
var button3Text : String;

var button1Rect : Rect;
var button2Rect : Rect;
var button3Rect : Rect;

var dialogText : String;
var dialogRect : Rect;

var state : dialogStates;
var lastState : dialogStates;
var lastPress : String;


function Start () {
	// Set current state info
	lastState = dialogStates.UNDEFINED;
	state = dialogStates.UNDEFINED;
	
	// Set the starting state of the dialog
	transitionToState(dialogStates.INTRO);
	
	// Register the callbacks the dialog cares about
	dispatcher.registerCallback(dlEvents.PLAYERMOVE, eventHandler);
	dispatcher.registerCallback(dlEvents.BUTTONPRESS, eventHandler);
	dispatcher.registerCallback(dlEvents.DOORENTER, eventHandler);
}

function Update () {

}

function eventHandler(e : dlEvents, object : Object)
{
	if(e == dlEvents.BUTTONPRESS)
	{
		lastPress = object as String;
		if(state == dialogStates.INTRO)
		{
			transitionToState(dialogStates.MOVEREQUEST);
		}
		else if(state == dialogStates.MOVEACK)
		{
			transitionToState(dialogStates.DOORREQUEST);
		}
		else if(state == dialogStates.DOORREQUEST)
		{
			transitionToState(dialogStates.DOORQUIP);
		}
		else if(state == dialogStates.DOORACK)
		{
			transitionToState(dialogStates.INTRO);
		}
	}
	else if(e == dlEvents.PLAYERMOVE)
	{
		if(state == dialogStates.INTRO || state == dialogStates.MOVEREQUEST)
		{
			transitionToState(dialogStates.MOVEACK);
		}
	}
	else if(e == dlEvents.DOORENTER)
	{
		if(state == dialogStates.MOVEACK || state == dialogStates.DOORREQUEST || state == dialogStates.DOORQUIP)
		{
			transitionToState(dialogStates.DOORACK);
		}
	}
}


function transitionToState(toState : dialogStates)
{

	if(toState == dialogStates.INTRO)
	{
		lastState = state;
		
		button1Text = "Lowly?!?!?";
		button1Rect = Rect(30,60,280,25);
	
		button2Text = "Ummm, is that me?";
		button2Rect = Rect (30,90,280,25);
		
		button3Text = "Wait, I shank things?";
		button3Rect = Rect (30, 120,280,25);
		
		dialogText = "Once there was a lowly hippo,\n destined to do some shanking.";
		dialogRect = Rect (10,10,320,165);
		
		state = toState;
	}
	else if(toState == dialogStates.MOVEREQUEST)
	{
		lastState = state;
		
		button1Text = "";
		button1Rect = Rect(0,0,0,0);
		
		button2Text = "";
		button2Rect = Rect(0,0,0,0);
		
		button3Text = "";
		button3Rect = Rect(0,0,0,0);
		
		dialogText = "Yup, that's you!\nI bet you can't even move...";
		dialogRect = Rect (10,10,320,45);
		
		state = toState;
	}
	else if(toState == dialogStates.MOVEACK)
	{
		lastState = state;
		
		button1Text = "You know it!";
		button1Rect = Rect(30,60,280,25);
		
		button2Text = "Well, I guess so...";
		button2Rect = Rect (30,90,280,25);
		
		button3Text = "Now I'm ready to shank things!";
		button3Rect = Rect (30, 120,280,25);
		
		dialogText = "HOLY SHIT!\n You can MOVE!!!";
		dialogRect = Rect (10,10,320,165);
		
		state = toState;
	}
	else if(toState == dialogStates.DOORREQUEST)
	{
		lastState = state;
		
		button1Text = "I bet I can!";
		button1Rect = Rect(30,60,280,25);
		
		button2Text = "Let's find out.";
		button2Rect = Rect (30,90,280,25);
		
		button3Text = "Door, Shmoore, Easy, Peasy.";
		button3Rect = Rect (30, 120,280,25);
		
		dialogText = "EhHrmm... I got excited for a moment\nI bet you can't go through a door.";
		dialogRect = Rect (10,10,320,165);
		
		state = toState;
	}
	else if(toState == dialogStates.DOORQUIP)
	{
		lastState = state;
		
		button1Text = "";
		button1Rect = Rect(0,0,0,0);
		
		button2Text = "";
		button2Rect = Rect(0,0,0,0);
		
		button3Text = "";
		button3Rect = Rect(0,0,0,0);
		
		if(lastPress == "button1")
		{
			dialogText = "Well aren't you sure of yourself...\nGo do it then Mr. Sure.";
		}
		else if(lastPress == "button2")
		{
			dialogText = "The door is the big flame thingy over there\nTrust me!";
		}
		else if(lastPress == "button3")
		{
			dialogText = "You know what they say:\n Ego was the death of the shanking hippo!";
		}
		dialogRect = Rect (10,10,320,45);
		
		state = toState;
	}
	else if(toState == dialogStates.DOORACK)
	{
		lastState = state;
		
		button1Text = "I want to hear it again please.";
		button1Rect = Rect(30,60,280,25);
		
		button2Text = "";
		button2Rect = Rect(0,0,0,0);
		
		button3Text = "";
		button3Rect = Rect(0,0,0,0);
		
		dialogText = "My star pupil! You managed the door and all!\nThat's all I've got...";
		dialogRect = Rect (10,10,320,95);
		
		state = toState;
	}
		
}

function OnGUI () {

	var button1 = GUI.Button(button1Rect, button1Text);
	var button2 = GUI.Button (button2Rect, button2Text);
	var button3 = GUI.Button (button3Rect, button3Text);
	// Make a background box
	GUI.Box (dialogRect, dialogText);
	
	if (button1) {
		dispatcher.eventNotify(dlEvents.BUTTONPRESS, "button1");
	}
	
	if (button2) {
		dispatcher.eventNotify(dlEvents.BUTTONPRESS, "button2");
	}
	
	if (button3) {
		dispatcher.eventNotify(dlEvents.BUTTONPRESS, "button3");
	}
}


