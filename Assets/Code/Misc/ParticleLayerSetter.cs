using UnityEngine;
using System.Collections;

public class ParticleLayerSetter : MonoBehaviour {

  void Start ()
  {
    // Set the sorting layer of the particle system.
    particleSystem.renderer.sortingLayerName = "Foreground";
    particleSystem.renderer.sortingOrder = 10;
  }
}
