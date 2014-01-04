using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
  public Transform rBound;
  public Transform lBound;
  public Transform tBound;
  public Transform bBound;
  public float yOff = 5f;
  
  // TODO have the script resize these things for the room
  public Vector3 playerScale;
  public float cameraSize;
  
  private Transform playerTrans;
  private Camera cam;
  private float camHalfWidth;
  private float camHalfHeight;

  public void Start() {
    playerTrans = GameObject.FindGameObjectsWithTag( "Player" )[0].transform;
    cam = Camera.main;
    camHalfWidth = cam.pixelWidth / 2f;
    camHalfHeight = cam.pixelHeight / 2f;
  }

  public void Update() {
    Vector3 camPos = cam.transform.position;
    Vector3 playerPos = playerTrans.position;

    // Screen coords of new point
    Vector3 newPosScreen = cam.WorldToScreenPoint( new Vector3( playerPos.x, playerPos.y + yOff, camPos.z ) ); 
    
    // check left bound
    if( lBound != null ) {
      float camLOffset = cam.WorldToScreenPoint( lBound.position ).x - ( newPosScreen.x - camHalfWidth );
      if( camLOffset > 0 ) newPosScreen = new Vector3( newPosScreen.x + camLOffset, newPosScreen.y, newPosScreen.z );
    }
    
    // check right bound
    if( rBound != null ) {
      float camROffset = cam.WorldToScreenPoint( rBound.position ).x - ( newPosScreen.x + camHalfWidth );
      if( camROffset < 0 ) newPosScreen = new Vector3( newPosScreen.x + camROffset, newPosScreen.y, newPosScreen.z );
    }
    
    // check top bound
    if( tBound != null ) {
      float camTOffset = cam.WorldToScreenPoint( tBound.position ).y - ( newPosScreen.y + camHalfHeight );
      if( camTOffset < 0 ) newPosScreen = new Vector3( newPosScreen.x, newPosScreen.y + camTOffset, newPosScreen.z );
    }
    
    // check bottom bound
    if( bBound != null ) {
      float camBOffset = cam.WorldToScreenPoint( bBound.position ).y - ( newPosScreen.y - camHalfHeight );
      if( camBOffset > 0 ) newPosScreen = new Vector3( newPosScreen.x, newPosScreen.y + camBOffset, newPosScreen.z );
    }
    
    // assign new position
    cam.transform.position = cam.ScreenToWorldPoint( newPosScreen );
  }
}


