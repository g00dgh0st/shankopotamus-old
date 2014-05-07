using UnityEngine;
using System.Collections;

public class CafeteriaGuard : MonoBehaviour {
    
  private GameObject bub;
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  private bool moving = false;
  private bool distracted = false;
  
  public Transform moveTo;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    
    SetupDialogue();
  }

  void OnClick() {
    if( distracted ) return;
    if( GameObject.Find( "item_spoon" ) == null )
      Game.player.MoveTo( transform.position, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 0); } );
    else
      Game.player.MoveTo( transform.position, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 10); } );
  }

  void OnHover( bool isOver  ) {
    if( !distracted ) Game.CursorHover( isOver, cursor );
  }
  
  void Update() {
    if( moving ) {
      transform.parent.position = new Vector3( transform.parent.position.x - 0.01f, transform.parent.position.y, -3 );
      if( transform.parent.position.x < moveTo.position.x ) moving = false;
    }
    
    if( !distracted && GameObject.Find( "item_spoon" ) && Game.player.transform.position.x > 2.74f && Game.player.InMotion() ) {
      Game.player.StopMove();
       Game.dialogueManager.StartDialogue( dialogue, 10 );
    }
  }
  
  public IEnumerator Distraction() {
    yield return new WaitForSeconds( 1f );
    Game.script.ShowSpeechBubble( "Hey! Are you bastards lighting each other on fire again?", transform.parent.Find( "BubTarget" ), 5f );
    moving = true;
    distracted = true;
    yield return new WaitForSeconds( 5f );
    Game.script.ShowSpeechBubble( "Looks kinda cool. I'll just stand here and watch.", transform.parent.Find( "BubTarget" ), 5f );
  }
  
  // All dialogue is "written" here
  public void SetupDialogue() {
    Transform camTarget = transform.parent.Find( "CamTarget" );
    
    dialogue = new Dialogue();
    
    dialogue.SetSteps(
    new Step[] {
      // 0
      new Step( camTarget, "Don't touch me.",
        new Option[] {
          new Option( "I didn't touch you.", 1 ),
          new Option( "Would you mind leaving for a few minutes?", 3 ),
          new Option( "Sorry.", -1 )
        }
      ),
      // 1
      new Step( camTarget, "I know, I was telling you for future reference.", 
        new Option[] {
          new Option( "I wasn't planning on touching you.", 2 ),
          new Option( "I'm going to go now.", -1 )
        }
      ),
      // 2
      new Step( camTarget, "That's what they all say.", 
        new Option[] {
          new Option( "Do a lot of people touch you?", 9 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 3
      new Step( camTarget, "Do you think I'm stupid?", 
        new Option[] {
          new Option( "I just heard there's some free cake in the cell block.", 4 ),
          new Option( "I figured it was worth a shot.", 8 ),
          new Option( "Nevermind.", -1 )
        }
      ),
      // 4
      new Step( camTarget, "Not much of a cake guy. If you were to tell me there was free hummus, well now that would be tempting.", 
        new Option[] {
          new Option( "Actually, they do have hummus too.", 5 ),
          new Option( "Well that's too bad.", -1 )
        }
      ),
      // 5
      new Step( camTarget, "They got pita bread too?", 
        new Option[] {
          new Option( "Yeah, sure.", 6 ),
          new Option( "I don't know.", 7 )
        }
      ),
      // 6
      new Step( camTarget, "Not much of a pita bread guy." ),
      // 7
      new Step( camTarget, "Wait a second, Cake and Hummus day isn't until next Friday. Get out of here!" ),
      // 8
      new Step( camTarget, "I've got my eye on you, punk." ),
      // 9
      new Step( camTarget, "I had a weird childhood." ),
      // 10
      new Step( camTarget, "Where do you think you're going with that spoon? Put it back where you got it!", 
        delegate() {
          Game.dialogueManager.StopDialogue();
      }, true ),
    } );
  }  
}
