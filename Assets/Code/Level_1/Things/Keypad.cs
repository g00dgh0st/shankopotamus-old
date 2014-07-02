using UnityEngine;
using System.Collections;

public class Keypad : MonoBehaviour {

  private int solution = 123456;
  private UILabel label;
  public bool open = false;
  
  void Awake() {
    label = GameObject.Find( "Keypad_Label" ).GetComponent<UILabel>();
  }
  
  void OnEnable() { 
    if( !open )
      label.text = "";
    else
      label.text = "GO AWAY";
  }
  
  public void PressButton( int number ) {
    if( open ) return;
    
    int n;
    if( !int.TryParse( label.text, out n ) ) label.text = "";
    
    if( number >= 0 && label.text.Length < 6 ) {
      label.text += number;
    } else if( number == -1 && label.text.Length > 0 && int.Parse( label.text ) == solution ) {
      label.text = "YUP";
      GameObject.Find( "Door_Hallway_Stairway" ).GetComponent<Door>().Unlock();
      open = true;
      StartCoroutine( HideKeypad() );
    } else if( number == -1 && label.text.Length > 0 && int.Parse( label.text ) != solution ) {
      string[] texts = new string[] { "NO", "NO", "NO", "NO", "NO", "LOL NO", "HELL NO", "NOPE", "WRONG", "UR DUMB", "WTF NO" };
      label.text = texts[Random.Range( 0, texts.Length - 1 )];
    } else if( number == -2 ) {
      label.text = "";
    } else {
      Debug.Log( "buzz" );
    }
  }
  
  private IEnumerator HideKeypad() {
    yield return new WaitForSeconds( 0.5f );
    GameObject.Find( "ClickOverlay" ).GetComponent<ClickOverlay>().OnClick();
  }

}