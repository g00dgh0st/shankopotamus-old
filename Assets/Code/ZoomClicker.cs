﻿using UnityEngine;
using System.Collections;

public class ZoomClicker : MonoBehaviour {
  public GameObject zoomView;
  private Sprite cursor;
  
  void Start() {
    zoomView.SetActive( false );
    cursor = Resources.Load<Sprite>( "Cursors/cursor_eye" );
  }
  
  void OnClick() {
    zoomView.SetActive( true );
    Game.cookies.Add( "zoomed", true );
    Game.ZoomIn();
    zoomView.transform.position = new Vector3( Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 5f );
  }
  
  void OnHover( bool isOver ) {
    if( Game.heldItem != null ) return;
    if( isOver ) {
      Game.cursor.GetComponent<CustomCursor>().SetCursor( cursor );
      Game.cursor.SetActive( true );
      Screen.showCursor = false;
    } else {
      Game.cursor.SetActive( false );
      Screen.showCursor = true;
    }
  }
}