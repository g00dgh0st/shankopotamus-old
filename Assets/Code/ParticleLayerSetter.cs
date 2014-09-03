using UnityEngine;
using System.Collections;

public class ParticleLayerSetter : MonoBehaviour {
  public enum SortLayer { Background, Main, Foreground };
  
  public SortLayer sortingLayer = SortLayer.Main;
  

  public int layer = 10;

  void Start () {
    // Set the sorting layer of the particle system.
    switch( sortingLayer ) {
      case SortLayer.Background:
        particleSystem.renderer.sortingLayerID = 1;
        break;
      case SortLayer.Main:
        particleSystem.renderer.sortingLayerID = 3;
        break;
      case SortLayer.Foreground:
        particleSystem.renderer.sortingLayerID = 2;
        break;
      default:
        particleSystem.renderer.sortingLayerID = 3;
        break;
    }
    particleSystem.renderer.sortingOrder = layer;
  }
}
