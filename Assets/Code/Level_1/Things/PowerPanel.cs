using UnityEngine;
using System.Collections;

public class PowerPanel : MonoBehaviour {
  
  public GameObject batt1;
  public GameObject batt2;
  public GameObject panel;
  
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
