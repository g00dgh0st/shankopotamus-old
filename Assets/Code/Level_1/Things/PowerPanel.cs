using UnityEngine;
using System.Collections;

public class PowerPanel : MonoBehaviour {
  
  public GameObject batt1;
  public GameObject batt2;
  public GameObject panel;
  
  private bool clickedOnce = false;
  
  void OnEnable() {
    if( !clickedOnce ) {
      Game.script.GetComponent<Level1>().needBattery = true;
      clickedOnce = true;
    }
  }
  
  void OnItemClick() {
    if( Game.heldItem.name == "item_battery" ) {
      Game.script.UseItem();
      batt1.SetActive( true );
      batt2.SetActive( true );
      panel.SetActive( true );
      Game.GetScript<FlushHandle>().isPowered = true;
    } 
  }
}
