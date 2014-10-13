using UnityEngine;
using System.Collections;

public class PowerPanel : Clicker {
  
  public GameObject batt;
  public GameObject fifty;
  
  public GameObject miniBatt;
  public GameObject miniFifty;
  
  private bool clickedOnce = false;
  
  void OnEnable() {
    if( !clickedOnce ) {
      Game.script.GetComponent<Level1>().needBattery = true;
      clickedOnce = true;
    }
  }
  
  private void OnDragOver( GameObject obj ) {
    if( obj.CompareTag( "Item" ) ) {
      obj.GetComponent<UISprite>().alpha = 0.5f;
    }
  }
  
  private void OnDragOut( GameObject obj ) {
    if( obj.CompareTag( "Item" ) ) {
      obj.GetComponent<UISprite>().alpha = 1f;
    }
  }
  
  void OnItemDrop( string item ) {
    if( Game.heldItem.name == "item_battery" ) {
      Game.script.UseItem();
      batt.SetActive( true );
      fifty.SetActive( false );
      miniBatt.SetActive( true );
      miniFifty.SetActive( false );
      Game.GetScript<FlushHandle>().isPowered = true;
    } else {
      Game.script.ShowSpeechBubble( "That won't do anything.", Game.player.transform.Find( "BubTarget" ), 2f );
    }
  }
}
