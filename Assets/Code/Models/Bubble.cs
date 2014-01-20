using UnityEngine;
using System.Collections;

public class Bubble {
  public string text;
  public Transform trans;
  public float time;
  
  public Bubble( string tx, Transform tr ) {
    text = tx;
    trans = tr;
    time = 999f;
  }

  public Bubble( string tx, Transform tr, float ti ) {
    text = tx;
    trans = tr;
    time = ti;
  }
  
  public Rect GetRect() {
    Vector3 bubPos = Camera.main.WorldToScreenPoint( trans.position );
    Vector2 txtSize = Game.dialogueManager.speechBubbleStyle.CalcSize( new GUIContent( text ) );
    
    return new Rect( bubPos.x - ( txtSize.x / 2 ), Camera.main.pixelHeight - bubPos.y - txtSize.y, txtSize.x, txtSize.y );
  }
  
}