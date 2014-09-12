using UnityEngine;
using System.Collections;

public class FuseBox : Clicker {
  public GameObject zoomView;
  
  private bool open = false;
  
  public bool broken = false;
  
  void Start() {
    zoomView.SetActive( false );
    cursorType = Clicker.CursorType.Hand;
  }

  void OnItemDrop( string item ) {
    if( item == "fuse_box_key" && !open ) {
      Game.script.UseItem();
      Game.player.MoveTo( movePoint, delegate( bool b ) {
        transform.parent.Find( "closed" ).gameObject.SetActive( false );
        transform.parent.Find( "open" ).gameObject.SetActive( true );
        open = true;
      } );
    } else {
      base.OnItemDrop( item );
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
  
}
