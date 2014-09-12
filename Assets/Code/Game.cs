using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
  // Global jank
  public static Player player;
  public static Game script;
  public static Hashtable cookies;
  public static GameObject cursor;
  public static DialogueManager dialogueManager;

  // items stuff
  public ArrayList items;
  public Inventory inventory;
  public static GameObject heldItem;
  
  // level and room stuff
  public string levelName;
  public GameObject startRoom;
  public static GameObject currentRoom;

  // For fading
  public static float aLerp;
  public static float aLerpSpeed;
  public static Texture2D blackTex;
  public static int isFading = 0;
  
  // paused or not
  public static bool clicksPaused = false;

  public void Awake() {
    player = GameObject.FindGameObjectsWithTag( "Player" )[0].GetComponent<Player>();
    cursor = GameObject.Find( "CustomCursor" );
    cursor.SetActive( false );
    script = this;
    cookies = new Hashtable();
    
    dialogueManager = gameObject.GetComponent<DialogueManager>();

    items = new ArrayList();
    heldItem = null;
    inventory = gameObject.GetComponent<Inventory>();
    
    aLerp = 0.0f;
    aLerpSpeed = 0.05f;
    blackTex = Resources.Load( "blackPxl" ) as Texture2D;
    
    Application.targetFrameRate = 60;
    
    currentRoom = startRoom;
  }
  
  void Start() {
    foreach( GameObject room in GameObject.FindGameObjectsWithTag( "Room" ) ) {
      if( room != startRoom ) room.SetActive( false );
    }
    float cScale = startRoom.GetComponent<Room>().characterScale;
    player.transform.localScale = new Vector3( cScale, cScale, cScale );
  }
  
// BEGIN FADE STUFF
  public void OnGUI() {
    if( aLerp <= 1.0f && Game.isFading == 1 ) {
      GUI.color = new Color( GUI.color.r, GUI.color.g, GUI.color.b, aLerp );
      GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), blackTex );
      aLerp += aLerpSpeed;
    } else if( aLerp > 1.0f && Game.isFading == 1 ) {
      StartCoroutine( DelayFadeIn( 0.6f ) );
      aLerp = 1.0f;
    }
    
    if( aLerp >= 0.0f && Game.isFading == 2 ) {
      GUI.color = new Color( GUI.color.r, GUI.color.g, GUI.color.b, aLerp );
      GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), blackTex );
      aLerp -= aLerpSpeed;
    } else if( aLerp < 0.0f && Game.isFading == 2 ) {
      Game.isFading = 0;
    }
  }
    
  public IEnumerator DelayFadeIn( float delay ) {
    yield return new WaitForSeconds( delay );
    Game.isFading = 2;
    foreach( GameObject bub in GameObject.FindGameObjectsWithTag( "SpeechBubble" ) ) {
      Destroy( bub );
    }
  }
// END FADE STUFF

// BEGIN ITEM STUFF
  
  void Update() {
    if( heldItem != null && Input.GetMouseButton( 1 ) ) DropItem();
  }
  
  public void HoldItem( GameObject item ) {
    Game.heldItem = item;
  }
  
  public void DropItem() {
    Game.heldItem = null;
    GameObject.Find( "Inventory" ).GetComponent<UIGrid>().repositionNow = true;
  }
  
  public void AddItem( string itemName ) {
    items.Add( itemName );
    GameObject newItem = Instantiate( Resources.Load( "Item" ) ) as GameObject;
    Item template = null;
    
    foreach( Item tmp in inventory.items ) {
      if( tmp.name == itemName ) {
        template = tmp;
        break;
      }
    }
    
    if( template == null ) return;
    
    // set sprite
    // newItem.transform.Find( "Sprite" ).GetComponent<SpriteRenderer>().sprite = template.sprite;
    newItem.GetComponent<UISprite>().spriteName = itemName;
    
    // set up prefab with item data
    newItem.name = "item_" + itemName;
    newItem.GetComponent<ItemClicker>().name = template.name;
    newItem.GetComponent<ItemClicker>().label = template.label;
    newItem.GetComponent<ItemClicker>().description = template.description;
    newItem.GetComponent<ItemClicker>().combos = template.combos;
    
    // Add to scene
    GameObject inv = GameObject.Find( "Inventory" );
    newItem.transform.parent = inv.transform;
    newItem.transform.localScale = new Vector3( 1f, 1f, 1f );
    inv.GetComponent<UIGrid>().repositionNow = true;
    newItem.transform.localPosition = new Vector3( newItem.transform.localPosition.x, newItem.transform.localPosition.y, -1f );
  }
  
  public void RemoveItem( string itemName ) {
    items.Remove( itemName );
    Destroy( GameObject.Find( "item_" + itemName ) );
  }
  
  public void UseItem() {
    GameObject.Find( "Inventory" ).GetComponent<UIGrid>().repositionNow = true;
    RemoveItem( Game.heldItem.GetComponent<ItemClicker>().name );
    Game.heldItem = null;
  }
// END ITEM STUFF
  
// BEGIN DIALOGUE STUFF
  public GameObject ShowSpeechBubble( string text, Transform target, float time ) {
    Destroy( GameObject.FindWithTag( "SpeechBubble" ) );
    GameObject bub = Instantiate( Resources.Load( "SpeechBubble" ) ) as GameObject;
    bub.GetComponent<SpeechBubble>().text = text;
    bub.GetComponent<SpeechBubble>().target = target;
    bub.GetComponent<SpeechBubble>().time = time;
    return bub;
  }
  
// END DIALOGUE STUFF
  
  
  
  
  
// STATIC METHODS
  
  public static void HideMenu() {
    GameObject.Find( "MenuBar" ).transform.Find( "MenuToggle" ).gameObject.SetActive( false );
  }
  
  public static void ShowMenu() {
    GameObject.Find( "MenuBar" ).transform.Find( "MenuToggle" ).gameObject.SetActive( true );
  }

  public delegate void Callback();
  
  public static IEnumerator FadeCamera( Callback cBack ) {
    isFading = 1;
    aLerp = 0.0f;
    
    while( isFading != 2 ) {
      yield return null;
    }
    
    cBack();
  }
  
  public static void TargetCam( Transform trns ) {
    Camera.main.transform.position = new Vector3( trns.position.x, trns.position.y - 0.1f, Camera.main.transform.position.z );
  }
  
  public static void PauseClicks() {
    Game.clicksPaused = true;
    Camera.main.transform.Find( "ClickOverlay" ).gameObject.SetActive( true );
  }
  
  public static void ResumeClicks() {
    Game.clicksPaused = false;
    Camera.main.transform.Find( "ClickOverlay" ).gameObject.SetActive( false );
  }
  
  public static void PauseCam() {
    Camera.main.gameObject.GetComponent<CameraControl>().isPaused = true;
  }
  
  public static void ResumeCam() {
    Camera.main.gameObject.GetComponent<CameraControl>().isPaused = false;
  }
  
  public static void ZoomIn() {
    Game.PauseClicks();
    Game.PauseCam();
    Camera.main.gameObject.GetComponent<Blur>().enabled = true;
  }
  
  public static void ZoomOut() {
    Game.ResumeClicks();
    Game.ResumeCam();
    Camera.main.gameObject.GetComponent<Blur>().enabled = false;
  }
  
  public static void ReverseNormals( Mesh mesh ) {
		Vector3[] normals = mesh.normals;
		for (int i=0;i<normals.Length;i++)
			normals[i] = -normals[i];
		mesh.normals = normals;

		for (int m=0;m<mesh.subMeshCount;m++)
		{
			int[] triangles = mesh.GetTriangles(m);
			for (int i=0;i<triangles.Length;i+=3)
			{
				int temp = triangles[i + 0];
				triangles[i + 0] = triangles[i + 1];
				triangles[i + 1] = temp;
			}
			mesh.SetTriangles(triangles, m);
		}
  }
  
  public static void CursorHover( bool isOver, string cursor ) {
    if( Game.heldItem != null ) return;
    if( isOver ) {
      Game.cursor.GetComponent<CustomCursor>().SetCursor( cursor );
      Game.cursor.transform.position = GameObject.Find( "Camera" ).GetComponent<Camera>().ScreenToWorldPoint( Input.mousePosition );
      Game.cursor.SetActive( true );
      Screen.showCursor = false;
    } else {
      Game.cursor.SetActive( false );
      Screen.showCursor = true;
    }
  }
  
  public static T GetScript<T>() where T : Component {
    GameObject go = GameObject.Find( typeof(T).ToString() );
    
    if( go.transform.Find( "Clicker" ) )
      return ( T ) go.transform.Find( "Clicker" ).gameObject.GetComponent<T>();
    else
      return ( T ) go.GetComponent<T>();
  }
  
}