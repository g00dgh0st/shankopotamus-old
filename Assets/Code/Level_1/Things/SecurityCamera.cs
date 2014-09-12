using UnityEngine;
using System.Collections;

public class SecurityCamera : Clicker {
  
  public SecurityScreens screens;
  
  private bool broked = false;
  
  public Transform moveTo;
  
  public string camName;
  
  void Start() {
    cursorType = Clicker.CursorType.Eye;
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) { 
      Game.script.ShowSpeechBubble( "Looks like a security camera.", Game.player.transform.Find( "BubTarget" ), 5f );
    } );
  }
  
  void OnItemDrop( string item ) {
    if( item == "wire_cutter" && !broked ) {
      
      if( screens.camerasOff < 3 ) Game.script.DropItem();
      else Game.script.UseItem();
      
      Game.player.MoveTo( movePoint, delegate( bool b ) { 
        transform.parent.Find( "Wire" ).gameObject.SetActive( false );
        transform.parent.Find( "BrokeWire" ).gameObject.SetActive( true );
      
        screens.DestroyCam( camName );

        broked = true;
      } );
    } else {
      base.OnItemDrop( item );
    }
  }
}
