using UnityEngine;
using System.Collections;

public class OptionButton : MonoBehaviour {
  
  private int optionIndex;
  
  public void Setup( string t, int i, int count ) {
    gameObject.GetComponent<UILabel>().text = "\u2022 " + t;
    optionIndex = i;
    transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y + ( (float)count * 40f ), transform.localPosition.z );
  }
  
  void OnClick() {
    Game.dialogueManager.ChooseOption( optionIndex );
  }
  
  void OnHover( bool isOver ) {
    Game.CursorHover( isOver, "PointerCursor" );
  }
}
