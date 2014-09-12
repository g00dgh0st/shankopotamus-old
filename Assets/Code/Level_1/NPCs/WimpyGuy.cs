using UnityEngine;
using System.Collections;

public class WimpyGuy : Clicker {
    
  private Dialogue dialogue;
  
  private ToughGuy toughGuy;
  
  public bool onFire = false;
  
  void Start() {
    cursorType = Clicker.CursorType.Chat;
    
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
    
      Game.player.MoveTo( movePoint, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, index ); } );
    } else {
      Game.script.ShowSpeechBubble( "Oh jeez, I'm on fire again.", transform.parent.Find( "BubTarget" ), 3f );
    }
  }

  void OnItemDrop( string item ) {
    if( toughGuy.wantsMeat && item == "three_meat_surprise" ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        GameObject.Find( "ToughGuy" ).transform.Find( "fire" ).gameObject.SetActive( true );
        toughGuy.onFire = true;
        Game.script.ShowSpeechBubble( "I set him on fire.", transform.parent.Find( "BubTarget" ), 3f );
        Game.script.ShowSpeechBubble( "AAAAHHHH!", GameObject.Find( "ToughGuy" ).transform.Find( "BubTarget" ), 3f );
        StartCoroutine( Game.GetScript<CafeteriaGuard>().Distraction() );
      });
    } else {
      base.OnItemDrop( item );
    } 
  }
}
