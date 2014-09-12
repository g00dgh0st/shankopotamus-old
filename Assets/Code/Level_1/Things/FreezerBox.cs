using UnityEngine;
using System.Collections;

public class FreezerBox : MonoBehaviour {
  
  public Transform[] positions;
  public Transform box;
  
  public int currentPos;
  
  void Start() {
    transform.position = new Vector3( box.position.x, box.position.y, transform.position.z );
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
  
  void OnClick() {
    Vector3 moveTo = transform.parent.Find( "BoxPush" + ( currentPos + 1 ) ).position;
    
    Game.player.MoveTo( moveTo, delegate( bool b ) {
      
      Game.PauseClicks();
      Game.player.PauseNav();
      
      int nextPos;
      
      if( currentPos < positions.Length - 1 ) {
        nextPos = currentPos + 1;
      } else {
        nextPos = 0;
      }
      
      Game.player.FaceTarget( positions[nextPos].position );
      
      Game.player.Interact( "push_box_start", delegate() {
        if( currentPos < positions.Length - 1 ) {
          currentPos++;
        } else {
          currentPos = 0;
        }
        Update();
      }, 0.4f );
    
    } );
  }
  
  void Update() {
    if( box.position != positions[currentPos].position && Vector3.Distance( box.position, positions[currentPos].position ) > 0.02f ) {
      Vector3 dir = ( positions[currentPos].position - transform.position ) * 0.01f;
      box.position += dir;
      Game.player.transform.position += dir;
      Game.player.anim.Play( "push_box" );
    } else if( transform.position.x != box.position.x ) {
      transform.position = new Vector3( box.position.x, box.position.y, transform.position.z );
      Game.player.Interact( "push_box_stop", delegate() {
        Game.ResumeClicks();
        Game.player.ResumeNav();
      } );
    }
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, "HandCursor" );
  }
}
