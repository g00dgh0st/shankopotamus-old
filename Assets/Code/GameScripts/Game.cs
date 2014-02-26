using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
  public static Player player;
  public static Level level;
  public static DialogueManager dialogueManager;
  public static Inventory inventory;
  public static MonoBehaviour script;
  public static Hashtable cookies;
  
  
  public string levelName;
  public GameObject startRoom;
  
  // For fading
  public static float aLerp;
  public static float aLerpSpeed;
  public static Texture2D blackTex;
  public static int isFading = 0;

  public void Start() {
    player = GameObject.FindGameObjectsWithTag( "Player" )[0].GetComponent<Player>();
    level = new Level( levelName, startRoom );
    dialogueManager = gameObject.GetComponent<DialogueManager>();
    inventory = gameObject.GetComponent<Inventory>();
    script = this;
    cookies = new Hashtable();
    
    aLerp = 0.0f;
    aLerpSpeed = 0.05f;
    blackTex = Resources.Load( "blackPxl" ) as Texture2D;
    
    Application.targetFrameRate = 60;
  }
  
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
  }

// STATIC METHODS

  public delegate void Callback();
  
  public static IEnumerator FadeCamera( Callback cBack ) {
    isFading = 1;
    aLerp = 0.0f;
    
    while( isFading != 2 ) {
      yield return 0;
    }
    
    cBack();
  }
  
  public static void PauseClicks() {
    Camera.main.transform.Find( "ClickOverlay" ).gameObject.SetActive( true );
  }
  
  public static void ResumeClicks() {
    Camera.main.transform.Find( "ClickOverlay" ).gameObject.SetActive( false );
  }
  
  public static void PauseCam() {
    Game.level.currentRoom.transform.Find( "CamTrans" ).gameObject.SetActive( false );
    foreach( Parallax p in FindObjectsOfType( typeof( Parallax ) ) ) p.enabled = false;
  }
  
  public static void ResumeCam() {
    Camera.main.orthographicSize = 1f;
    Game.level.currentRoom.transform.Find( "CamTrans" ).gameObject.SetActive( true );
    foreach( Parallax p in FindObjectsOfType( typeof( Parallax ) ) ) p.enabled = true;
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

}