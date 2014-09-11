using UnityEngine;
using System.Collections;

public class FlushHandle : Clicker {
  
  public bool isPowered = false;
  
  public ParticleSystem small;
  public ParticleSystem big;
  
  void Start() {
    cursorType = Clicker.CursorType.Hand;
  }
  
  void OnClick() {
    Game.player.MoveTo( movePoint, delegate( bool b ) { 
      Game.player.FaceTarget( transform.position );
      
      if( !isPowered ) {
        Game.player.Interact( "press", delegate() {
          Game.script.ShowSpeechBubble( "Careful, don't get sucked in.", GameObject.Find( "Pig" ).transform.Find( "BubTarget" ), 3f );
          small.Play();
        } );
      } else {
        Game.player.Interact( "press", delegate() {
          big.Play();
          StartCoroutine( SuckInPig() );
        } );
      }
    } );
  }
  
  IEnumerator SuckInPig() {
    Transform pig = GameObject.Find( "Pig" ).transform;
    Game.script.ShowSpeechBubble( "Ahhh!", pig.Find( "BubTarget" ), 1.5f );
    
    while( pig.localPosition.x < 0.45821f ) {
      pig.position = new Vector3( pig.position.x + 0.05f, pig.position.y, pig.position.z );
      yield return null;
    }
    Destroy( GameObject.Find( "Pig" ) );
    Game.cookies.Add( "pigInSewer", true );
  }
}
