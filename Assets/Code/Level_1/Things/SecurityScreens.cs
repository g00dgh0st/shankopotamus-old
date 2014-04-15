using UnityEngine;
using System.Collections;

public class SecurityScreens : MonoBehaviour {
  
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  public int camerasOff = 0;
  
  public Transform guardPos;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
    
    Transform camTarget = GameObject.Find( "GuardTowerGuard" ).transform.Find( "CamTarget" );
    
    dialogue = new Dialogue();
    
    dialogue.SetSteps(
    new Step[] {
      new Step( camTarget, "Don't touch that!" ),
      new Step( camTarget, "Don't touch-- oh crap, one of the cameras are down. Eh, whatever there's still two up." ),
      new Step( camTarget, "Don't touch-- oh crap two of the cameras are down. Eh, whatever there's still the one." ),
      new Step( camTarget, "Don't touch-- oh crap all of the cameras are down! I'm gonna be fired! Out of a cannon!", delegate() {
        Game.dialogueManager.StopDialogue();
        // animate guard moving to the screens.
        GameObject gurd = GameObject.Find( "GuardTowerGuard" );
        gurd.transform.position = guardPos.position;
        gurd.transform.localScale = new Vector3( gurd.transform.localScale.x * -1, gurd.transform.localScale.y, gurd.transform.localScale.z );
        Game.GetScript<GuardTowerGuard>().atScreens = true;
      }, true )
    } );
  }
  
  void OnClick() {
    GuardTowerGuard scrip = Game.GetScript<GuardTowerGuard>();
  
    if( scrip.firstTime ) {
      Game.GetScript<GuardTowerGuard>().OnClick();
    } else {
      Game.player.MoveTo( transform.position, delegate() { 
        if( camerasOff == 0 ) {
          Game.dialogueManager.StartDialogue( dialogue, 0 );
        } else if( camerasOff == 1 ) {
          Game.dialogueManager.StartDialogue( dialogue, 1 );
        } else if( camerasOff == 2 ) {
          Game.dialogueManager.StartDialogue( dialogue, 2 );
        } else if( camerasOff == 3 ) {
          Game.dialogueManager.StartDialogue( dialogue, 3 );
        }
      } );
    }
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
