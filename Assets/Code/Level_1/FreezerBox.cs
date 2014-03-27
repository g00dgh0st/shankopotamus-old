using UnityEngine;
using System.Collections;

public class FreezerBox : MonoBehaviour {
  
  public Transform[] positions;
  public Transform box;
  
  public int currentPos;
  
  private Sprite cursor;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }
  
  void OnClick() {
    if( currentPos < positions.Length - 1 ) {
      currentPos++;
    } else {
      currentPos = 0;
    }
  }
  
  void Update() {
    if( box.position != positions[currentPos].position ) {
      box.position = positions[currentPos].position;
      transform.position = new Vector3( box.position.x, box.position.y, transform.position.z );
    }
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
