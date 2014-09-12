using UnityEngine;
using System.Collections;

public class ItemClicker : MonoBehaviour {
  public string name;
  public string label;
  public string description;
  public ItemCombo[] combos;
  
  void OnClick() {
    Game.script.ShowSpeechBubble( description, Game.player.transform.Find( "BubTarget" ), 2f );
  }
  
  void OnItemDrop( string item ) {
    foreach( ItemCombo combo in combos ) {
      if( combo.combine == item ) {
        Game.script.AddItem( combo.result );
        Game.script.UseItem();
        Game.script.RemoveItem( name );
        break;
      }
    }
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, "HandCursor" );
  }
  
  void OnDrag( Vector2 delta ) {
    transform.position = Camera.main.ScreenToWorldPoint( Input.mousePosition );
    collider2D.enabled = false;
    Game.script.HoldItem( gameObject );
    gameObject.GetComponent<UISprite>().depth = 4;
  }
  
  void OnDragEnd() {
    collider2D.enabled = true;
		UICamera.hoveredObject.SendMessage( "OnItemDrop", name, SendMessageOptions.DontRequireReceiver );
    gameObject.GetComponent<UISprite>().depth = 3;
    Game.script.DropItem();
  }
}
