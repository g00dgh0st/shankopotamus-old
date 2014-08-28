using UnityEngine;
using System.Collections;

public class ParticleLayerSetter : MonoBehaviour {

  public int layer = 10;

  void Start () {
    // Set the sorting layer of the particle system.
    particleSystem.renderer.sortingLayerName = "Main";
    particleSystem.renderer.sortingOrder = layer;
  }
}
