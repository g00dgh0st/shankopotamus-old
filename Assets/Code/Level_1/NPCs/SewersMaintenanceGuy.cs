using UnityEngine;
using System.Collections;

public class SewersMaintenanceGuy : MonoBehaviour {
  
  private Dialogue dialogue;
  
  public FuseBox fusebox;
  
  public Transform dest;
  
  private bool atFuseBox = false;
  private bool moving = false;
  private bool noLadder = false;
  public bool wantsStew = false;
  public bool ratRod = false;

  public Cook cook;
  
  public Transform ladder;
  public Transform fishingPos;
  public Transform fixingPos;
  
  public Transform movePoint;
  
  void Start() {
    SetupDialogue();
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, "ChatCursor" );
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
    if( wantsStew && item == "pancake_stew" ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint.position, delegate( bool b ) {
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
    } else {
      Game.script.ShowSpeechBubble( "That won't do anything.", Game.player.transform.Find( "BubTarget" ), 2f );
    }
  }

  void OnClick() {
    if( !atFuseBox )
      Game.player.MoveTo( movePoint.position, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 0 ); } );
    else {
      Game.player.MoveTo( movePoint.position, delegate( bool b ) { Game.dialogueManager.StartDialogue( dialogue, 15 ); } );
    }
  }

  void Update() {
    if( moving ) {
      transform.parent.position = new Vector3( transform.parent.position.x + 0.02f, transform.parent.position.y, 0f );
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
          new Option( "I broke that- I mean, that fuse box over there broke.", 14, delegate() { return fusebox.broken && !atFuseBox; } ),
          new Option( "What are you doing?", 1 ),
          new Option( "Can I use your ladder?", 9, delegate() { return !noLadder; } ),
          new Option( "Can I borrow your fishing rod?", 3, delegate() { return ratRod; } ),
          new Option( "Are there any rats down here?", 19, delegate() { return cook.wantsIngredients; } ),
          new Option( "Did you realize there is a dead whale in the sewer?", 21, delegate() { return Game.script.GetComponent<Level1>().seenWhale; }),
          new Option( "Sorry.", -1 )
        }
      ),
      // 1
      new Step( camTarget, "Fishing.",
        new Option[] {
          new Option( "Shouldn't you be working?", 4 ),
          new Option( "What can you catch down here?", 2 ),
          new Option( "Can I borrow your fishing rod?", 3, delegate() { return ratRod; } ),
        }
      ),
      // 2
      new Step( camTarget, "Fish. Also hepatitis.",
        new Option[] {
          new Option( "Shouldn't you be working?", 4 ),
          new Option( "Can I borrow your fishing rod?", 3, delegate() { return ratRod; } ),
          new Option( "I have another question.", 0 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 3
      new Step( camTarget, "I'm using it.",
        new Option[] {
          new Option( "Shouldn't you be working?", 4 ),
          new Option( "I have another question.", 0 ),
          new Option( "Ok. I'll leave then.", -1 )
        }
      ),
      // 4
      new Step( camTarget, "I'm a maintenance guy. Ain't nothing broken, so there ain't nothing to maintain.",
        new Option[] {
          new Option( "Isn't maintenance more about preventing things from breaking?", 8 ),
          new Option( "What if something broke right now?", 5 ),
          new Option( "I have another question.", 0 ),
          new Option( "Ok. I'll leave then.", -1 )
        }
      ),
      // 5
      new Step( camTarget, "I guess I'd have to go fix it wouldn't I?",
        new Option[] {
          new Option( "I have another question.", 0 ),
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
          new Option( "Bye.", -1 )
        }
      ),
      // 8
      new Step( camTarget, "Right now I'm preventing myself from breaking your face. How's that for maintenance?",
        new Option[] {
          new Option( "What if something broke right now?", 5 ),
          new Option( "I have another question.", 0 ),
          new Option( "I think I'll leave you alone.", -1 )
        }
      ),
      // 9
      new Step( camTarget, "I'm using it.",
        new Option[] {
          new Option( "Why don't you sit on something else?", 12 ),
          new Option( "I just need to use it for a second.", 10 )
        }
      ),
      // 10
      new Step( camTarget, "I tell you what, you get me a can of Pancake Stew, you can do whatever you want with this ladder.",
        new Option[] {
          new Option( "What's Pancake Stew?", 11 ),
          new Option( "Where can I find one?", 17 ),
          new Option( "I have another question.", 0, delegate() { return !atFuseBox; } ),
          new Option( "I'll try to find one.", -1 )
        },
        delegate() { wantsStew = true; }
      ),
      // 11
      new Step( camTarget, "That's a stupid question.",
        new Option[] {
          new Option( "Where can I find one?", 17 ),
          new Option( "I have another question.", 0, delegate() { return !atFuseBox; } ),
          new Option( "I'll try to find one.", -1 )
        }
      ),
      // 12
      new Step( camTarget, "How about I sit on your face?", 13 ),
      // 13
      new Step( camTarget, "That didn't come out right.",
        new Option[] {
          new Option( "I just need to use it for a second.", 10 ),
          new Option( "I have another question.", 0, delegate() { return !atFuseBox; } ),
          new Option( "I'll leave you alone.", -1 )
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
          new Option( "Did you realize there is a dead whale in the sewer?", 21, delegate() { return Game.script.GetComponent<Level1>().seenWhale; }),
          new Option( "Nothing.", -1 )
        }
      ), 
      // 16
      new Step( camTarget, "Don't tell me how to do my job. I don't tell you how to be a dumbass.",
        new Option[] {
          new Option( "I just need to use it for a second.", 10 ),
          new Option( "I'll leave you alone.", -1 )
        }
      ),
      // 17
      new Step( camTarget, "One of the inmates is a hoarder. Got anything you could ask for. He sold me a bucket of cat feces once.", 
        new Option[] {
          new Option( "What did you need that for?", 18 ),
          new Option( "I have another question.", 0, delegate() { return !atFuseBox; } ),
          new Option( "I'll go talk to him.", -1 )
        }
      ),
      // 18
      new Step( camTarget, "That's a stupid question.", 
        new Option[] {
          new Option( "I have another question.", 0, delegate() { return !atFuseBox; } ),
          new Option( "I'll go find some Pancake Stew", -1 )
        }
      ),
      // 19
      new Step( camTarget, "There's a rat who lives in the other side of the sewer. He's a real bastard.", 
        new Option[] {
          new Option( "Do you know how I can capture it?", 20 ),
          new Option( "There's a whale blocking that side.", 21 ),
          new Option( "I have another question.", 0 ),
          new Option( "I gotta go.", -1 )
        }
      ),
      // 20
      new Step( camTarget, "What are you, a dumbass? Put some cheese on a fishing rod.", 
        new Option[] {
          new Option( "Can I borrow your fishing rod?", 3 ),
          new Option( "I have another question.", 0, delegate() { return !atFuseBox; } ),
          new Option( "I gotta go.", -1 )
        },
        delegate() { ratRod = true; }  
      ),
      // 21
      new Step( camTarget, "I tried to move it, but it's too big. What I need to do is flush a whale-sized object into the sewers to push it out of the way.", 22 ),
      // 22
      new Step( camTarget, "But I don't care.", 
        new Option[] {
          new Option( "I have another question.", 0, delegate() { return !atFuseBox; } ),
          new Option( "I gotta go.", -1 )
        }
      )
    } );
  }  
}
