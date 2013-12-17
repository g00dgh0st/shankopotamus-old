using UnityEngine;
using System.Collections;

public class Dispatcher : MonoBehaviour {

  public static ArrayList handlers = new ArrayList();
  public enum dlEvents {PLAYERMOVE, PLAYERCLICK, BUTTONPRESS, DOORENTER};
  
  public delegate void Callback( dlEvents evt, string str );


  public static bool registerCallback( dlEvents evt, Callback handler ) {
  	Hashtable temp = new Hashtable();
  	temp["event"] = evt;
  	temp["handler"] = handler;
  	handlers.Add( temp );
  	return true;
  }

  public static bool removeCallback( dlEvents evt, Callback handler ) {
  	for( int i = 0; i < handlers.Count; i++ ) {
  		Hashtable temp = handlers[i] as Hashtable;
  		dlEvents tempevent = (dlEvents)temp["event"];
  		Callback temphandler = temp["handler"] as Callback;
		
  		if( tempevent == evt && temphandler == handler ) {
  			handlers.RemoveAt( i );
  			return true;
  		}
  	}
  	return false;
  }


  public static void eventNotify( dlEvents e, string str ) {
  	for( int i = 0; i < handlers.Count; i++ ) {
  		Hashtable temp = handlers[i] as Hashtable;
  		dlEvents evt = (dlEvents)temp["event"]; 
  		Callback handler = temp["handler"] as Callback;
		
  		if( evt == e ) {
  			handler( e, str );
  		}
  	}
  }
}