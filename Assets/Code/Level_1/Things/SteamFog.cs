using UnityEngine;
using System.Collections;

public class SteamFog : MonoBehaviour {
  
  public int noShower = 3;
  public int oneShower = 25;
  public int twoShower = 100;
  public int threeShower = 200;
  public int fourShower = 400;
  
  void OnEnable() {
    if( Game.cookies != null ) CheckShowers();
  }
  
  public void CheckShowers() {
    if( Game.cookies.Contains( "showersOn" ) ) {
      switch( (int)Game.cookies["showersOn"] ) {
        case 0:
          gameObject.particleSystem.emissionRate = noShower;
          break;
        case 1:
          gameObject.particleSystem.emissionRate = oneShower;
          break;
        case 2:
          gameObject.particleSystem.emissionRate = twoShower;
          break;
        case 3:
          gameObject.particleSystem.emissionRate = threeShower;
          break;
        case 4:
          gameObject.particleSystem.emissionRate = fourShower;
          break;
        default:
          gameObject.particleSystem.emissionRate = noShower;
          break;
      }
    } else {
      gameObject.particleSystem.emissionRate = noShower;
    }
  }
}
