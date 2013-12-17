using UnityEngine;
using System.Collections;

public class Dispatcher : MonoBehaviour {

  static ArrayList handlers = new ArrayList();
  enum dlEvents {PLAYERMOVE, PLAYERCLICK, BUTTONPRESS, DOORENTER};


  static void registerCallback( dlEvents event, Function handler ) {
  	Hashtable temp = new Hashtable();
  	temp["event"] = event;
  	temp["handler"] = handler;
  	handlers.Add( temp );
  	return true;
  }

  static void removeCallback( dlEvents event, Function handler ) {
  	for( int i = 0; i < handlers.Count; i++ ) {
  		Hashtable temp = handlers[i] as Hashtable;
  		dlEvents tempevent = temp["event"];
  		Function temphandler = temp["handler"] as Function;
		
  		if( tempevent == event && temphandler == handler ) {
  			handlers.RemoveAt( i );
  			return true;
  		}
  	}
  	return false;
  }


  static void eventNotify( dlEvents e, Object object ) {
  	for( int i = 0; i < handlers.Count; i++ ) {
  		Hashtable temp = handlers[i] as Hashtable;
  		dlEvents event = temp["event"];
  		Function handler = temp["handler"] as Function;
		
  		if( event == e ) {
  			handler( e, object );
  		}
  	}
  }
}