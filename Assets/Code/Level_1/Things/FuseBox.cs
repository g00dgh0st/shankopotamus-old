using UnityEngine;
using System.Collections;

public class FuseBox : MonoBehaviour {
  public GameObject zoomView;
  private Sprite cursor;
  
  public Transform moveTo;
  
  private bool open = false;
  
  void Start() {
    zoomView.SetActive( false );
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }

  void OnItemClick() {
    if( Game.heldItem.name == "item_fuse_box_key" && !open ) {
      Transform pos = moveTo == null ? transform : moveTo;
      Game.player.MoveTo( pos.position, delegate() {
        Game.script.UseItem();
        transform.parent.Find( "closed" ).gameObject.SetActive( false );
        transform.parent.Find( "open" ).gameObject.SetActive( true );
        cursor = Resources.Load<Sprite>( "Cursors/cursor_eye" );
        open = true;
      } );
    }
  }
  
  void OnClick() {
    if( open ) {
      Transform pos = moveTo == null ? transform : moveTo;
      Game.player.MoveTo( pos.position, delegate() {
        zoomView.SetActive( true );
        Game.cookies.Add( "zoomed", true );
        Game.ZoomIn();
        zoomView.transform.position = new Vector3( Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 5f );
      });
    } else {
      Game.script.ShowSpeechBubble( "It's locked. Looks like I need a key.", Game.player.transform.Find( "BubTarget" ), 5f );
    }
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
