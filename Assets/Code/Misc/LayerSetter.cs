using UnityEngine;
using System.Collections;

public class LayerSetter : MonoBehaviour {

  void Start ()
  {
    // Set the sorting layer of the particle system.
    renderer.sortingLayerName = "Foreground";
    renderer.sortingOrder = 10;
    GetComponent<LineRenderer>().sortingLayerName = "Foreground";
    GetComponent<LineRenderer>().sortingOrder = 10;
  }
}
