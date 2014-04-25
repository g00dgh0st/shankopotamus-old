using UnityEngine;
using System.Collections;

public class SewersMaintenanceGuy : MonoBehaviour {
  
  private Sprite cursor;
  
  private Dialogue dialogue;
  
  public FuseBox fusebox;
  
  public Transform dest;
  
  private bool atFuseBox = false;
  private bool moving = false;
  private bool noLadder = false;
  public bool wantsStew = false;
  
  public Transform ladder;
  public Transform fishingPos;
  public Transform fixingPos;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_chat" );
    
    SetupDialogue();
  }

  void OnItemClick( string itemName ) {
    if( wantsStew && itemName == "pancake_stew" ) {
      Game.player.MoveTo( transform.position, delegate() {
        Game.script.UseItem();
        Game.script.ShowSpeechBubble( "Thanks. You can have the ladder.", transform.parent.Find( "BubTarget" ), 3f );
        wantsStew = false;
        noLadder = true;
        if( !atFuseBox ) {
          ladder.parent = transform.parent.parent;
          ladder.gameObject.GetComponent<BoxCollider2D>().enabled = true;
          transform.parent.position = fishingPos.position;
        } else {
          ladder.parent = transform.parent.parent;
          ladder.gameObject.GetComponent<BoxCollider2D>().enabled = true;
          transform.parent.position = fixingPos.position;
        }
      });
    }
  }

  void OnClick() {
    if( !atFuseBox )
      Game.player.MoveTo( transform.position, delegate() { Game.dialogueManager.StartDialogue( dialogue, 0 ); } );
    else {
      Game.player.MoveTo( transform.position, delegate() { Game.dialogueManager.StartDialogue( dialogue, 15 ); } );
    }
  }

  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
  
  void Update() {
    if( moving ) {
      transform.parent.position = new Vector3( transform.parent.position.x + 0.02f, transform.parent.position.y, 0.8f );
      if( transform.parent.position.x > dest.position.x ) moving = false;
    }
  }
  
  IEnumerator MoveToFix() {
    yield return new WaitForSeconds( 0.3f );
    
    Transform rod = transform.parent.Find( "FishingRod" );
    rod.parent = transform.parent.parent;
    Transform dropPoint = GameObject.Find( "RodDropPoint" ).transform;
    
    rod.position = dropPoint.position;
    rod.rotation = dropPoint.rotation;
    rod.Find( "Clicker" ).gameObject.SetActive( true );
    
    transform.parent.parent = transform.parent.parent.parent;
      
    moving = true;
    while( moving ) {
      yield return null;
    }
    
    transform.parent.parent = transform.parent.parent.Find( "SewersMid" );
    atFuseBox = true;
  }
  
  // All dialogue is "written" here
  public void SetupDialogue() {
    Transform camTarget = transform.parent.Find( "CamTarget" );
    
    dialogue = new Dialogue();
    
    dialogue.SetSteps(
    new Step[] {
      //0
      new Step( camTarget, "Don't touch me.", 
        new Option[] {
          new Option( "Hey, I broke-- I mean, there's a broken fuse box over there.", 14, delegate() { return fusebox.broken && !atFuseBox; } ),
          new Option( "What are you doing?", 1 ),
          new Option( "Can I use your ladder?", 9, delegate() { return !noLadder; } ),
          new Option( "Can I borrow your fishing rod?", 3 ),
          new Option( "Sorry.", -1 )
        }
      ),
      // 1
      new Step( camTarget, "Fishing.",
        new Option[] {
          new Option( "Shouldn't you be working?", 4 ),
          new Option( "What can you catch down here?", 2 ),
          new Option( "Can I borrow your fishing rod?", 3 ),
          new Option( "Can I use your ladder?", 9, delegate() { return !noLadder; } )
        }
      ),
      // 2
      new Step( camTarget, "Fish. Also hepatitis.",
        new Option[] {
          new Option( "Are you sure that fish is safe to eat?", 6 ),
          new Option( "Can I borrow your fishing rod?", 3 ),
          new Option( "Can I use your ladder?", 9, delegate() { return !noLadder; } )
        }
      ),
      // 3
      new Step( camTarget, "I'm using it.",
        new Option[] {
          new Option( "What can you catch down here?", 2 ),
          new Option( "Shouldn't you be working?", 4 ),
          new Option( "What about your ladder?", 9, delegate() { return !noLadder; } ),
          new Option( "Ok. I'll leave then.", -1 )
        }
      ),
      // 4
      new Step( camTarget, "I'm a maintenance guy. There ain't nothing broken, so there ain't nothing to maintain.",
        new Option[] {
          new Option( "Isn't maintenance more about preventing things from breaking?", 8 ),
          new Option( "What if something broke right now?", 5 )
        }
      ),
      // 5
      new Step( camTarget, "Well then, I guess I would have to go fix it wouldn't I?",
        new Option[] {
          new Option( "Ok, I'm going to go.", -1 )
        }
      ),
      // 6
      new Step( camTarget, "I don't eat them. I just like to watch them plead with their God for another chance at life.",
        new Option[] {
          new Option( "That seems kind of cruel.", 7 )
        }
      ),
      // 7
      new Step( camTarget, "These fish are die-hard \"Toddlers & Tiaras\" fans. They deserve it.",
        new Option[] {
          new Option( "Can I borrow your fishing rod?", 3 ),
          new Option( "Can I use your ladder?", 9, delegate() { return !noLadder; } ),
          new Option( "Bye.", -1 )
        }
      ),
      // 8
      new Step( camTarget, "Right now I'm preventing myself from breaking your face. How's that for maintenance?",
        new Option[] {
          new Option( "What if something broke right now?", 5 ),
          new Option( "I think I'll leave you alone.", -1 )
        }
      ),
      // 9
      new Step( camTarget, "I'm using it.",
        new Option[] {
          new Option( "Why don't you sit on something else?", 12 ),
          new Option( "What if I trade you for it?", 10 )
        }
      ),
      // 10
      new Step( camTarget, "I tell you what, you get me a can of Pancake Stew, you can do whatever you want with this ladder.",
        new Option[] {
          new Option( "What's Pancake Stew?", 11 ),
          new Option( "Can I have your fishing rod?", 3, delegate() { return !atFuseBox; } ),
          new Option( "Ok, I'll try to find one.", -1 )
        },
        delegate() { wantsStew = true; }
      ),
      // 11
      new Step( camTarget, "That's a stupid question. The prison's been running out of cans lately, so if you can find one, I'll give you the ladder.",
        new Option[] {
          new Option( "Can I have your fishing rod?", 3, delegate() { return !atFuseBox; }  ), 
          new Option( "Ok I'll try to find a can.", -1 )
        }
      ),
      // 12
      new Step( camTarget, "How about I sit on your face?", 13 ),
      // 13
      new Step( camTarget, "That didn't come out right.",
        new Option[] {
          new Option( "What if I trade you for it?", 10 )
        }
      ),
      // 14
      new Step( camTarget, "Dern it. Why can't people read sticky notes?", 
        delegate() {
          // drop rod
          Game.dialogueManager.StopDialogue();
          
          StartCoroutine( MoveToFix() );
      }, true ),
      // 15
      new Step( camTarget, "What do you want?",
        new Option[] {
          new Option( "Do you actually need to use that ladder?", 16, delegate() { return !noLadder; } ),
          new Option( "Nothing.", -1 )
        }
      ), 
      // 16
      new Step( camTarget, "Don't tell me how to do my job. I don't tell you how to be a dumbass.",
        new Option[] {
          new Option( "How about I trade you something for that ladder?", 10 )
        }
      )
    } );
  }  
}
