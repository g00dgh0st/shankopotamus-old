using UnityEngine;
using System.Collections;

public class FuseBox : Clicker {
  public GameObject zoomView;
  private Sprite cursor;
  
  private bool open = false;
  
  public bool broken = false;
  
  void Start() {
    zoomView.SetActive( false );
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
  }

  void OnItemClick() {
    if( Game.heldItem.name == "item_fuse_box_key" && !open ) {
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        Game.script.UseItem();
        transform.parent.Find( "closed" ).gameObject.SetActive( false );
        transform.parent.Find( "open" ).gameObject.SetActive( true );
        open = true;
      } );
    }
  }
  
  void OnClick() {
    if( open ) {
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        zoomView.SetActive( true );
        Game.cookies.Add( "zoomed", true );
        Game.ZoomIn();
        zoomView.transform.position = new Vector3( Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 5f );
      });
    } else {
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        Game.script.ShowSpeechBubble( "It's locked. Looks like I need a key.", Game.player.transform.Find( "BubTarget" ), 5f );
      } );
    }
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
}
