using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
  private Camera cam;
  private Vector3 startPos;
  
  public float xOffset = 0.1f;
  public float yOffset = 0.05f;

  public void Start() {
    cam = Camera.main;
    startPos = transform.position;
  }
  
  public void Update() {
    Vector3 camPos = cam.transform.position;
    Vector3 diffPos = startPos - camPos;
    
    transform.position = new Vector3( ( startPos - ( diffPos * xOffset ) ).x, ( startPos - ( diffPos * yOffset ) ).y, startPos.z );
  }
}