using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
  
  private Transform playerTrans;
  private float mouseScrollThreshold = 30f;

	// Use this for initialization
	void Start () {
	  playerTrans = Game.player.gameObject.transform.Find( "CamTarget" );
    transform.position = new Vector3( playerTrans.position.x, playerTrans.position.y, -10 );
    
    
	}
	
	// Update is called once per frame
	void Update () {
    
    ArrayList sprites = new ArrayList();
    
    foreach( Transform child in Game.currentRoom.transform ) {
      if( child.gameObject.CompareTag( "MainSprite" ) ) sprites.Add( child.gameObject );
    }
    
    Bounds roomBounds = ((GameObject)sprites[0]).GetComponent<SpriteRenderer>().bounds;
    
    for( int i = 1; i < sprites.Count; i++ ) {
      roomBounds.Encapsulate( ((GameObject)sprites[i]).GetComponent<SpriteRenderer>().bounds );
    }
    
    Vector3 pos = transform.position;
    Vector3 newPos;

    Debug.Log( roomBounds.Contains( new Vector3( pos.x, pos.y, roomBounds.center.z ) ) );

    if( Game.player.InMotion() ) {
      Vector3 diff = new Vector3( playerTrans.position.x, playerTrans.position.y, -10 ) - pos;
      newPos = pos + ( diff * 0.1f );
      
      if( roomBounds.Contains( new Vector3( newPos.x, newPos.y, roomBounds.center.z ) ) || !roomBounds.Contains( new Vector3( pos.x, pos.y, roomBounds.center.z ) ) )
        transform.position = newPos;
    } else if( Input.mousePosition.x < Screen.width && Input.mousePosition.x > 0 && Input.mousePosition.y > 0 && Input.mousePosition.y < Screen.height ){
      
      newPos = pos;
      
      if( Input.mousePosition.x > Screen.width - mouseScrollThreshold ) 
        newPos = new Vector3( pos.x + ( 2f * Time.deltaTime ), pos.y, pos.z );

      if( Input.mousePosition.x < 0 + mouseScrollThreshold ) 
        newPos = new Vector3( pos.x - ( 2f * Time.deltaTime ), pos.y, pos.z );

      if( Input.mousePosition.y > Screen.height - mouseScrollThreshold ) 
        newPos = new Vector3( pos.x, pos.y + ( 2f * Time.deltaTime ), pos.z );
      
      if( Input.mousePosition.y < 0 + mouseScrollThreshold ) 
        newPos = new Vector3( pos.x, pos.y - ( 2f * Time.deltaTime ), pos.z );
      
      if( newPos == pos ) return;
      if( roomBounds.Contains( new Vector3( newPos.x, newPos.y, roomBounds.center.z ) ) || !roomBounds.Contains( new Vector3( pos.x, pos.y, roomBounds.center.z ) ) )
        transform.position = newPos;
    }
	}
}
