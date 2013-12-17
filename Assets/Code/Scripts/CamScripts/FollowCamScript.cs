﻿using UnityEngine;
using System.Collections;

public class FollowCamScript : MonoBehaviour {
  public Transform playerTrans;
  public Transform rBound;
  public Transform lBound;
  public Transform tBound;
  public Transform bBound;
  public float yOff = 10;

  void Update() {
    if( !gameObject.activeInHierarchy ) return;
    Vector3 camPos = transform.position;
    Vector3 playerPos = playerTrans.position;
  
    if( !( ( rBound != null && camera.WorldToScreenPoint( rBound.position ).x <= camera.pixelWidth && playerPos.x > camPos.x ) || ( lBound != null && camera.WorldToScreenPoint( lBound.position ).x >= 0 && playerPos.x < camPos.x ) ) )
      transform.position.x = playerPos.x;
    
    if( !( ( tBound != null && camera.WorldToScreenPoint( tBound.position ).y >= 0 && playerPos.y > camPos.y ) || ( bBound != null && camera.WorldToScreenPoint( bBound.position ).y <= camera.pixelHeight && playerPos.y < camPos.y ) ) )
      transform.position.y = playerPos.y + yOff;
  }
}