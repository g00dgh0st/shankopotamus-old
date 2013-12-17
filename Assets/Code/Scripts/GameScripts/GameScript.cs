using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

  // this script is for handling global game scripting

  void Awake() {
    Application.targetFrameRate = 60;
  }

  void Start() {
    // load global scripts here
    gameObject.AddComponent<"FadeScript">();
  }
}