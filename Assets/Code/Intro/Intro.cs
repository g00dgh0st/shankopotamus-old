using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

  private GameObject player;
  private GameObject guard;
  
  private bool canDo = false;
  private bool moveGuard = false;


  void Start() {
    player = GameObject.Find( "Player" );
    guard = GameObject.Find( "Guard" );
    player.GetComponent<Player>().MoveTo( GameObject.Find( "InCellTarget" ).transform.position );
    player.GetComponent<Player>().PauseNav();
    StartCoroutine( WaitForEnter() );
  }
  
  void OnPress( bool press ) {
    if( press && canDo ) {
      player.GetComponent<Player>().MoveTo( Camera.main.ScreenToWorldPoint( Input.mousePosition ) );
    }
  }
  
  void Update() {
    if( player.transform.localPosition.y > -0.3164263f ) {
      player.GetComponent<LayerSetter>().SetOrder( 0 );
    }  
    
    if( guard && guard.transform.position.x > -0.739535f && guard.transform.parent.gameObject.name == "Player" ) {
      guard.transform.parent = guard.transform.parent.parent;
      ShowSpeechBubble( "Get comfy maggot.\nYou're gonna be in there a while.", guard.transform.Find( "BubTarget" ), 4f );
    }  
    
    if( moveGuard ) {
      guard.transform.position = new Vector3( guard.transform.position.x - 0.01f, guard.transform.position.y, guard.transform.position.z );
    }
  }
  
  public void ShowSpeechBubble( string text, Transform target, float time ) {
    Destroy( GameObject.FindWithTag( "SpeechBubble" ) );
    GameObject bub = Instantiate( Resources.Load( "SpeechBubble" ) ) as GameObject;
    bub.GetComponent<SpeechBubble>().text = text;
    bub.GetComponent<SpeechBubble>().target = target;
    bub.GetComponent<SpeechBubble>().time = time;
  }

  private IEnumerator WaitForEnter() {
    
    while( player.GetComponent<Player>().InMotion() ) {
      yield return null;
    }
    
    GameObject.Find( "CellDoor" ).transform.Find( "open" ).gameObject.SetActive( false );
    GameObject.Find( "CellDoor" ).transform.Find( "closed" ).gameObject.SetActive( true );

    player.transform.localScale = new Vector3( -player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z );
    
    yield return new WaitForSeconds( 1f );
    
    moveGuard = true;
    
    yield return new WaitForSeconds( 3f );
    
    // Zooming and Fading stuff
    GameObject sprites = GameObject.Find( "Sprites" );
    GameObject root = GameObject.Find( "Root" );
    Color clr = new Color( 1f, 1f, 1f, 1f );
    float multi = 0.1f;
    
    
    while( sprites.transform.Find( "Cell" ).GetComponent<SpriteRenderer>().color.a > 0f ) {
      foreach( SpriteRenderer renderer in sprites.GetComponentsInChildren<SpriteRenderer>() ) {
        clr = new Color( 1f, 1f, 1f, clr.a - ( 0.0005f * multi ) );
        
        renderer.color = clr;
      }
      
      root.transform.localScale = root.transform.localScale * ( 1f + ( 0.0025f * multi ) );
      
      Vector3 newPos = Camera.main.transform.position + ( ( GameObject.Find( "Cell_Shanko" ).transform.Find( "center" ).position - Camera.main.transform.position ) * ( 0.08f * multi ) );
      Camera.main.transform.position = new Vector3( newPos.x, newPos.y, -10f );
      
      if( multi < 1f ) multi += 0.009f;
      
      yield return null;
    }
    
    PolyNav2D.current.GenerateMap();
    
    moveGuard = false;
    Destroy( guard );
    
    player.GetComponent<Player>().ResumeNav();
    player.GetComponent<PolyNavAgent>().maxSpeed = 1f;
    canDo = true;
  }
  
}
