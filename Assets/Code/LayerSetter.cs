using UnityEngine;
using System.Collections;

public class LayerSetter : MonoBehaviour {
  public enum SortLayer { Background, Main, Foreground };
  
  public int sortingOrder = 10;
  public SortLayer sortingLayer = SortLayer.Main;

  void Start() { 
    Reset();
  }
  
  private void Reset() {
    foreach( MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>() ) {
      renderer.sortingLayerName = sortingLayer.ToString();
      renderer.sortingOrder = sortingOrder;
      switch( sortingLayer ) {
        case SortLayer.Background:
          renderer.sortingLayerID = 1;
          break;
        case SortLayer.Main:
          renderer.sortingLayerID = 3;
          break;
        case SortLayer.Foreground:
          renderer.sortingLayerID = 2;
          break;
          default:
          break;
      }
    }
    
    foreach( SkinnedMeshRenderer renderer in GetComponentsInChildren<SkinnedMeshRenderer>() ) {
      renderer.sortingLayerName = sortingLayer.ToString();
      renderer.sortingOrder = sortingOrder;
      switch( sortingLayer ) {
        case SortLayer.Background:
          renderer.sortingLayerID = 1;
          break;
        case SortLayer.Main:
          renderer.sortingLayerID = 3;
          break;
        case SortLayer.Foreground:
          renderer.sortingLayerID = 2;
          break;
          default:
          break;
      }
    }
  }
  
  public void SetOrder( int o ) {
    sortingOrder = o;
    Reset();
  }
}
