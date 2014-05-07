using UnityEngine;
using System.Collections;

public class WimpyGuy : MonoBehaviour {
    
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  public Transform waypoint;
  
  private ToughGuy toughGuy;
  
  public bool onFire = false;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    
    toughGuy = Game.GetScript<ToughGuy>();
    
    dialogue = toughGuy.dialogue;
  }


  void OnClick() {
    if( toughGuy.onFire ) {
      Game.script.ShowSpeechBubble( "Nothing like eating good food in front of a warm fire.", transform.parent.Find( "BubTarget" ), 3f );
      return;
    } 
      
    if( !onFire ) {
      int index;
    
      if( toughGuy.wantsMeat ) {
        index = 30;
      } else if( toughGuy.talkedOnce ) {
        index = 9;
      } else {
        index = 4;
      }
    
      Game.player.MoveTo( waypoint.position, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, index ); } );
    } else {
      Game.script.ShowSpeechBubble( "Oh jeez, I'm on fire. Boy it hurts.", transform.parent.Find( "BubTarget" ), 3f );
    }
  }

  void OnItemClick() {
    if( toughGuy.wantsMeat && Game.heldItem.name == "item_three_meat_surprise" ) {
      Game.player.MoveTo( transform.position, delegate( bool b ) {
        Game.script.UseItem();
        GameObject.Find( "ToughGuy" ).transform.Find( "fire" ).gameObject.SetActive( true );
        toughGuy.onFire = true;
        Game.script.ShowSpeechBubble( "I set him on fire.", transform.parent.Find( "BubTarget" ), 3f );
        Game.script.ShowSpeechBubble( "AAAAHHHH!", GameObject.Find( "ToughGuy" ).transform.Find( "BubTarget" ), 3f );
        StartCoroutine( Game.GetScript<CafeteriaGuard>().Distraction() );
      });
    } 
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
