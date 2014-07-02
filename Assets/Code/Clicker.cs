using UnityEngine;
using System.Collections;

public class Clicker : MonoBehaviour {

  public Vector3 movePoint = Vector3.zero;
  
  void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawSphere( movePoint, 0.02f );
  }
}
