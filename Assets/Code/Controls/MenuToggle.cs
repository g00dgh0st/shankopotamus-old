using UnityEngine;
using System.Collections;

public class MenuToggle : MonoBehaviour {
  
  public static bool isMoving = false;
  
  private Transform menu;
  private Transform outt;
  private Transform inn;
  
  void Start() {
    menu = GameObject.Find( "MenuBar" ).transform;
    outt = GameObject.Find( "menuOut" ).transform;
    inn = GameObject.Find( "menuIn" ).transform;
  }
 
  void OnClick() {
    StartCoroutine( ToggleMenu() );
  }
  
  void Update() {
    if( Input.GetMouseButton( 0 ) && IsOpen() && !menu.gameObject.GetComponent<Collider2D>().OverlapPoint( Camera.main.ScreenToWorldPoint( Input.mousePosition ) ) ) CloseMenu();
  }
  
  IEnumerator ToggleMenu() {
    if( MenuToggle.isMoving ) yield break;
        
    MenuToggle.isMoving = true;
    
    Transform target = ( menu.position.y == outt.position.y ? inn : outt );
    
    while( Mathf.Abs( menu.position.y - target.position.y ) > 0.001f ) {
      menu.position = new Vector3( menu.position.x, menu.position.y + ( ( target.position.y - menu.position.y ) * 0.4f ), menu.position.z );
      yield return null;
    }
    
    menu.position = new Vector3( menu.position.x, target.position.y, menu.position.z );

    MenuToggle.isMoving = false;
  }
  
  public void CloseMenu() {
    StartCoroutine( ToggleMenu() );
  }
  
  public void OpenMenu() {
    StartCoroutine( ToggleMenu() );
  }
  
  public bool IsOpen() {
    return menu.position.y == inn.position.y;
  }
  
}
