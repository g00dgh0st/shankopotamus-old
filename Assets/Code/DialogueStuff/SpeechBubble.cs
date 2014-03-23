using UnityEngine;
using System.Collections;

public class SpeechBubble : MonoBehaviour {
  
  public string text;
  public Transform target = null;
  public float time;
  
  void Start() {
    gameObject.GetComponent<UILabel>().text = text;
    
    if( target != null ) transform.position = target.position;
    else transform.position = Vector3.zero;
  }
  
  void Update() {
    if( target != null ) {
      transform.position = target.position;
    }
    
    time -= Time.deltaTime;
    
    if( time <= 0f ) {
      Destroy( gameObject );
    }
  }
}
