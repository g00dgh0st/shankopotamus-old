using UnityEngine;
using System.Collections;

public class SecurityCamera : MonoBehaviour {
  
  private Sprite cursor;
  
  public SecurityScreens screens;
  
  private bool broked = false;
  
  public Transform moveTo;
  
  public string camName;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    Transform pos = moveTo == null ? transform : moveTo;
    Game.player.MoveTo( pos.position, delegate() { 
      Game.script.ShowSpeechBubble( "Looks like a security camera.", Game.player.transform.Find( "BubTarget" ), 5f );
    } );
  }
  
  void OnItemClick() {
    if( Game.heldItem.name == "item_wire_cutter" && !broked ) {
      Transform pos = moveTo == null ? transform : moveTo;
      Game.player.MoveTo( pos.position, delegate() { 
      
        Debug.Log( "Cut wire" );
        transform.parent.Find( "Wire" ).gameObject.SetActive( false );
        transform.parent.Find( "BrokeWire" ).gameObject.SetActive( true );
      
        screens.DestroyCam( camName );

        broked = true;
      
        if( screens.camerasOff < 3 )
          Game.script.DropItem();
        else
          Game.script.UseItem();
      } );
    }
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
