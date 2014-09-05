using UnityEngine;
using System.Collections;

public class MenuToggle : MonoBehaviour {
  
  private Sprite cursor;
  private bool isOpen = false;
  
  private Transform menu;
  private Transform outt;
  private Transform inn;
  
  void Start() {
    cursor = Resources.Load<Sprite>( "Cursors/cursor_hand" );
    menu = GameObject.Find( "MenuBar" ).transform;
    outt = GameObject.Find( "menuOut" ).transform;
    inn = GameObject.Find( "menuIn" ).transform;
  }
 
  void OnClick() {
    isOpen = !isOpen;
  }
  
  void Update() {
    if( isOpen && menu.position.y != inn.position.y ) {
      menu.position = new Vector3( menu.position.x, menu.position.y + ( ( inn.position.y - menu.position.y ) * 0.4f ), menu.position.z );
    } else if( !isOpen && menu.position.y != outt.position.y ) {
      menu.position = new Vector3( menu.position.x, menu.position.y + ( ( outt.position.y - menu.position.y ) * 0.4f ), menu.position.z );
    }
    
    if( isOpen && Mathf.Abs( menu.position.y - inn.position.y ) < 0.001f ) menu.position = inn.position;
    if( !isOpen && Mathf.Abs( menu.position.y - outt.position.y ) < 0.001f ) menu.position = outt.position;
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, cursor );
  }
  
  public void CloseMenu() {
    isOpen = false;
  }
  
  public void OpenMenu() {
    isOpen = true;
  }
}
