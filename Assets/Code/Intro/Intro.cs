using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

  private GameObject player;
  private GameObject guard;
  private GameObject chameleon;
  
  private bool moveGuard = false;


  void Start() {
    player = GameObject.Find( "Player" );
    guard = GameObject.Find( "Guard" );
    chameleon = GameObject.Find( "Chameleon" );
    player.GetComponent<Player>().MoveTo( GameObject.Find( "InCellTarget" ).transform.position );
    player.GetComponent<Player>().PauseNav();
    Game.PauseClicks();
    StartCoroutine( WaitForEnter() );
  }
  
  void Update() {
    if( player.transform.localPosition.y > -0.3164263f ) {
      player.GetComponent<LayerSetter>().SetOrder( 0 );
    }  
    
    if( !guard ) return;
    
    if( guard.transform.position.x > -0.739535f && guard.transform.parent.gameObject.name == "Player" ) {
      guard.transform.parent = guard.transform.parent.parent;
      ShowSpeechBubble( "Welcome to your new home, dingus.", guard.transform.Find( "BubTarget" ), 4f );
    }  
    
    if( moveGuard ) {
      guard.transform.position = new Vector3( guard.transform.position.x - 0.005f, guard.transform.position.y, guard.transform.position.z );
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
    
    guard.transform.localScale = new Vector3( -guard.transform.localScale.x, guard.transform.localScale.y, guard.transform.localScale.z );
    
    moveGuard = true;
    
    yield return new WaitForSeconds( 3f );
    
    // Zooming and Fading stuff
    GameObject sprites = GameObject.Find( "Sprites" );
    GameObject root = GameObject.Find( "Root" );
    Color clr = new Color( 1f, 1f, 1f, 1f );
    float multi = 0.1f;
    
    Vector3 diff = new Vector3( ( player.transform.localScale.x > 0 ? 1 : -1 ) * 1.338778f, 1.338778f, 1.338778f ) - player.transform.localScale;
    
    while( sprites.transform.Find( "Cell" ).GetComponent<SpriteRenderer>().color.a > 0f ) {
      foreach( SpriteRenderer renderer in sprites.GetComponentsInChildren<SpriteRenderer>() ) {
        clr = new Color( 1f, 1f, 1f, clr.a - ( 0.0005f * multi ) );
        
        renderer.color = clr;
      }
      
      if( Mathf.Abs( player.transform.localScale.x ) > 1.338778f ) player.transform.localScale += 0.005f * diff * multi;
      
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
    Game.ResumeClicks();
    player.GetComponent<PolyNavAgent>().maxSpeed = 1f;
    
    yield return new WaitForSeconds( 5f );
      
    SpriteRenderer cSprite = chameleon.transform.Find( "chameleon_test" ).GetComponent<SpriteRenderer>();
    
    while( cSprite.color.a < 1f ) {
      cSprite.color = new Color( 1f, 1f, 1f, cSprite.color.a + 0.005f );
      yield return null;
    }
    
    chameleon.collider2D.enabled = true;
      
  }
}
