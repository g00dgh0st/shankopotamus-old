#pragma strict
#pragma downcast

static var handlers = new Array();
enum dlEvents {PLAYERMOVE, PLAYERCLICK, BUTTONPRESS, DOORENTER};


static function registerCallback (event : dlEvents, handler : Function)
{
	var temp:Hashtable = new Hashtable();
	temp["event"] = event;
	temp["handler"] = handler;
	handlers[handlers.length] = temp;
	return true;
}

static function removeCallback(event : dlEvents, handler : Function)
{
	for(var i = 0; i < handlers.length; ++i)
	{
		var temp:Hashtable = handlers[i] as Hashtable;
		var tempevent:dlEvents = temp["event"];
		var temphandler:Function = temp["handler"] as Function;
		
		if(tempevent == event && temphandler == handler)
		{
			handlers.splice(i, 1);
			return true;
		}
	}
	return false;
}


static function eventNotify(e : dlEvents, object : Object)
{
	for(var i = 0; i < handlers.length; ++i)
	{
		var temp:Hashtable = handlers[i] as Hashtable;
		var event : dlEvents = temp["event"];
		var handler : Function = temp["handler"] as Function;
		
		if(event == e)
		{
			handler(e, object);
		}
	}
}



