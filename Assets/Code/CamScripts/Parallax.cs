using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
  private Camera cam;
  private Vector3 startPos;
  
  public float offset = 0.1f;

  public void Start() {
    cam = Camera.main;
    startPos = transform.position;
  }

  public void Update() {
    Vector3 camPos = cam.transform.position;
    Vector3 diffPos = startPos - camPos;
    
    transform.position = new Vector3( ( startPos - ( diffPos * offset ) ).x, ( startPos - ( diffPos * offset ) ).y, startPos.z );
    
  }
}