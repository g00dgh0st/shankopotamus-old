using UnityEngine;
using System.Collections;

public class WimpyGuy : MonoBehaviour {
    
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  public Transform waypoint;
  
  private ToughGuy toughGuy;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    
    toughGuy = Game.GetScript<ToughGuy>();
    
    dialogue = toughGuy.dialogue;
  }


  void OnClick() {
    int index;
    
    if( toughGuy.wantsMeat ) {
      index = 30;
    } else if( toughGuy.talkedOnce ) {
      index = 9;
    } else {
      index = 4;
    }
    
    Game.player.MoveTo( waypoint.position, delegate() { Game.dialogueManager.StartDialogue( dialogue, index ); } );
  }

  void OnItemClick() {
    if( toughGuy.wantsMeat && Game.heldItem.name == "item_three_meat_surprise" ) {
      Game.player.MoveTo( transform.position, delegate() {
        Game.script.UseItem();
      });
    } 
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
