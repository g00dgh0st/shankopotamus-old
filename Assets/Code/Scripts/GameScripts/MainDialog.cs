using UnityEngine;
using System.Collections;

public class MainDialog : MonoBehaviour {

  public enum dialogStates {UNDEFINED, INTRO, MOVEREQUEST, MOVEACK, DOORREQUEST, DOORACK, DOORQUIP};

  string button1Text;
  string button2Text;
  string button3Text;

  Rect button1Rect;
  Rect button2Rect;
  Rect button3Rect;

  string dialogText;
  Rect dialogRect;

  dialogStates state;
  dialogStates lastState;
  string lastPress;

  public void Start() {
  	// Set current state info
  	lastState = dialogStates.UNDEFINED;
  	state = dialogStates.UNDEFINED;
	
  	// Set the starting state of the dialog
  	transitionToState(dialogStates.INTRO);
	
  	// Register the callbacks the dialog cares about
  	Dispatcher.registerCallback(Dispatcher.dlEvents.PLAYERMOVE, eventHandler);
  	Dispatcher.registerCallback(Dispatcher.dlEvents.BUTTONPRESS, eventHandler);
  	Dispatcher.registerCallback(Dispatcher.dlEvents.DOORENTER, eventHandler);
  }

  public void eventHandler( Dispatcher.dlEvents e, string str ) {
  	if(e == Dispatcher.dlEvents.BUTTONPRESS)
  	{
  		lastPress = str;
      
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
  	else if(e == Dispatcher.dlEvents.PLAYERMOVE)
  	{
  		if(state == dialogStates.INTRO || state == dialogStates.MOVEREQUEST)
  		{
  			transitionToState(dialogStates.MOVEACK);
  		}
  	}
  	else if(e == Dispatcher.dlEvents.DOORENTER)
  	{
  		if(state == dialogStates.MOVEACK || state == dialogStates.DOORREQUEST || state == dialogStates.DOORQUIP)
  		{
  			transitionToState(dialogStates.DOORACK);
  		}
  	}
  }


  public void transitionToState( dialogStates toState ) {

  	if(toState == dialogStates.INTRO)
  	{
  		lastState = state;
		
  		button1Text = "Lowly?!?!?";
  		button1Rect = new Rect(30,60,280,25);
	
  		button2Text = "Ummm, is that me?";
  		button2Rect = new Rect(30,90,280,25);
		
  		button3Text = "Wait, I shank things?";
  		button3Rect = new Rect(30, 120,280,25);
		
  		dialogText = "Once there was a lowly hippo,\n destined to do some shanking.";
  		dialogRect = new Rect(10,10,320,165);
		
  		state = toState;
  	}
  	else if(toState == dialogStates.MOVEREQUEST)
  	{
  		lastState = state;
		
  		button1Text = "";
  		button1Rect = new Rect(0,0,0,0);
		
  		button2Text = "";
  		button2Rect = new Rect(0,0,0,0);
		
  		button3Text = "";
  		button3Rect = new Rect(0,0,0,0);
		
  		dialogText = "Yup, that's you!\nI bet you can't even move...";
  		dialogRect = new Rect(10,10,320,45);
		
  		state = toState;
  	}
  	else if(toState == dialogStates.MOVEACK)
  	{
  		lastState = state;
		
  		button1Text = "You know it!";
  		button1Rect = new Rect(30,60,280,25);
		
  		button2Text = "Well, I guess so...";
  		button2Rect = new Rect(30,90,280,25);
		
  		button3Text = "Now I'm ready to shank things!";
  		button3Rect = new Rect(30, 120,280,25);
		
  		dialogText = "HOLY SHIT!\n You can MOVE!!!";
  		dialogRect = new Rect(10,10,320,165);
		
  		state = toState;
  	}
  	else if(toState == dialogStates.DOORREQUEST)
  	{
  		lastState = state;
		
  		button1Text = "I bet I can!";
  		button1Rect = new Rect(30,60,280,25);
		
  		button2Text = "Let's find out.";
  		button2Rect = new Rect(30,90,280,25);
		
  		button3Text = "Door, Shmoore, Easy, Peasy.";
  		button3Rect = new Rect(30, 120,280,25);
		
  		dialogText = "EhHrmm... I got excited for a moment\nI bet you can't go through a door.";
  		dialogRect = new Rect(10,10,320,165);
		
  		state = toState;
  	}
  	else if(toState == dialogStates.DOORQUIP)
  	{
  		lastState = state;
		
  		button1Text = "";
  		button1Rect = new Rect(0,0,0,0);
		
  		button2Text = "";
  		button2Rect = new Rect(0,0,0,0);
		
  		button3Text = "";
  		button3Rect = new Rect(0,0,0,0);
		
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
  		dialogRect = new Rect(10,10,320,45);
		
  		state = toState;
  	}
  	else if(toState == dialogStates.DOORACK)
  	{
  		lastState = state;
		
  		button1Text = "I want to hear it again please.";
  		button1Rect = new Rect(30,60,280,25);
		
  		button2Text = "";
  		button2Rect = new Rect(0,0,0,0);
		
  		button3Text = "";
  		button3Rect = new Rect(0,0,0,0);
		
  		dialogText = "My star pupil! You managed the door and all!\nThat's all I've got...";
  		dialogRect = new Rect(10,10,320,95);
		
  		state = toState;
  	}
		
  }

  public void OnGUI() {

  	// Make a background box
  	GUI.Box (dialogRect, dialogText);
	
  	if (GUI.Button( button1Rect, button1Text )) {
  		Dispatcher.eventNotify(Dispatcher.dlEvents.BUTTONPRESS, "button1");
  	}
	
  	if (GUI.Button( button2Rect, button2Text )) {
  		Dispatcher.eventNotify(Dispatcher.dlEvents.BUTTONPRESS, "button2");
  	}
	
  	if (GUI.Button( button3Rect, button3Text )) {
  		Dispatcher.eventNotify(Dispatcher.dlEvents.BUTTONPRESS, "button3");
  	}
  }
}