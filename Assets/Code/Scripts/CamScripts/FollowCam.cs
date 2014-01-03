using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
  public Transform rBound;
  public Transform lBound;
  public Transform tBound;
  public Transform bBound;
  public float yOff = 10;
  
  private Transform playerTrans;
  private Camera cam;

  public void Start() {
    playerTrans = GameObject.FindGameObjectsWithTag( "Player" )[0].transform;
    cam = Camera.main;
  }

  public void Update() {
    Vector3 camPos = cam.transform.position;
    Vector3 playerPos = playerTrans.position;
  
    // if( !( ( rBound != null && cam.WorldToScreenPoint( rBound.position ).x <= cam.pixelWidth && playerPos.x > camPos.x ) || ( lBound != null && cam.WorldToScreenPoint( lBound.position ).x >= 0 && playerPos.x < camPos.x ) ) )
      cam.transform.position = new Vector3( playerPos.x, cam.transform.position.y + yOff, cam.transform.position.z );
        
    // if( !( ( tBound != null && cam.WorldToScreenPoint( tBound.position ).y <= cam.pixelHeight && playerPos.y > camPos.y ) || ( bBound != null && cam.WorldToScreenPoint( bBound.position ).y >= 0 && playerPos.y < camPos.y ) ) )
      cam.transform.position = new Vector3( cam.transform.position.x, playerPos.y + yOff, cam.transform.position.z );
  }
}