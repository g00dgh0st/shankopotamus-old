using UnityEngine;
using System.Collections;

public class Clicker : MonoBehaviour {

  public Vector3 movePoint = Vector3.zero;
  
  public enum CursorType { Hand, Chat, Eye, Door };
  public CursorType cursorType = CursorType.Hand;
  
  
  void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawSphere( movePoint, 0.02f );
  }
  

  protected void OnHover( bool isOver ) {
    string crs;
    switch( cursorType ) {
      case CursorType.Chat:
        crs = "ChatCursor";
        break;
      case CursorType.Door:
        crs = "DoorCursor";
        break;
      case CursorType.Eye:
        crs = "EyeCursor";
        break;
      case CursorType.Hand:
      default:
        crs = "HandCursor";
        break;
    }
    
    Game.CursorHover( isOver, crs );
  }
}
