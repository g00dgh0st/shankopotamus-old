using UnityEngine;
using System.Collections;

public class Keypad : MonoBehaviour {

  private int solution = 123456;
  public bool open = false;
  
  private string input = "";
  
  private Transform num_1;
  private Transform num_2;
  private Transform num_3;
  private Transform num_4;
  private Transform num_5;
  private Transform num_6;
  private Transform num_7;
  private Transform num_8;
  
  void Awake() {
    num_1 = transform.Find( "num_1" );
    num_2 = transform.Find( "num_2" );
    num_3 = transform.Find( "num_3" );
    num_4 = transform.Find( "num_4" );
    num_5 = transform.Find( "num_5" );
    num_6 = transform.Find( "num_6" );
    num_7 = transform.Find( "num_7" );
    num_8 = transform.Find( "num_8" );
  }
  
  void OnEnable() { 
    ClearNumbers();
    if( open ){
      SetChar( "0", 3 );
      SetChar( "P", 4 );
      SetChar( "E", 5 );
      SetChar( "N", 6 );
    } 
  }
  
  public void PressButton( int number ) {
    if( open ) return;
    
    if( input == "_" ) {
      ClearNumbers(); 
      input = "";
    }

    if( number >= 0 && input.Length < 8 ) {
      input += number;
      SetChar( number.ToString(), input.Length );
    } else if( number == -1 && input.Length > 0 && int.Parse( input ) == solution ) {
      ClearNumbers();
      SetChar( "0", 3 );
      SetChar( "P", 4 );
      SetChar( "E", 5 );
      SetChar( "N", 6 );
      GameObject.Find( "Door_Hallway_Stairway" ).GetComponent<Door>().Unlock();
      open = true;
      StartCoroutine( HideKeypad() );
    } else if( number == -1 && input.Length > 0 && int.Parse( input ) != solution ) {
      input = "_";
      string[] texts = new string[] { "NO", "NO", "NO", "LOL NO", "HELL NO", "NOPE", "POOP" };
      switch( texts[Random.Range( 0, texts.Length - 1 )] ) {
        case "NO":
        default:
          ClearNumbers();
          SetChar( "N", 4 );
          SetChar( "0", 5 );
          break;
        case "LOL NO":
          ClearNumbers();
          SetChar( "L", 2 );
          SetChar( "0", 3 );
          SetChar( "L", 4 );
          SetChar( "N", 6 );
          SetChar( "0", 7 );
          break;
        case "HELL NO":
          ClearNumbers();
          SetChar( "H", 1 );
          SetChar( "E", 2 );
          SetChar( "L", 3 );
          SetChar( "L", 4 );
          SetChar( "N", 6 );
          SetChar( "0", 7 );
          break;
        case "NOPE":
          ClearNumbers();
          SetChar( "N", 3 );
          SetChar( "0", 4 );
          SetChar( "P", 5 );
          SetChar( "E", 6 );
          break;
        case "POOP":
          ClearNumbers();
          SetChar( "P", 3 );
          SetChar( "0", 4 );
          SetChar( "0", 5 );
          SetChar( "P", 6 );
          break;
      }
      StartCoroutine( ClearWrong() );
    } else if( number == -2 ) {
      input = "";
      ClearNumbers();
    } else {
      // Buzz sound effect 
      Debug.Log( "buzz" );
    }
  }
  
  private IEnumerator HideKeypad() {
    yield return new WaitForSeconds( 0.5f );
    GameObject.Find( "ClickOverlay" ).GetComponent<ClickOverlay>().OnClick();
  }
  
  private IEnumerator ClearWrong() {
    yield return new WaitForSeconds( 2f );
    if( input == "_" ) ClearNumbers();
  }
  
  private void ClearNumbers() {
    foreach( Transform child in num_1 ) GameObject.Destroy(child.gameObject);
    foreach( Transform child in num_2 ) GameObject.Destroy(child.gameObject);
    foreach( Transform child in num_3 ) GameObject.Destroy(child.gameObject);
    foreach( Transform child in num_4 ) GameObject.Destroy(child.gameObject);
    foreach( Transform child in num_5 ) GameObject.Destroy(child.gameObject);
    foreach( Transform child in num_6 ) GameObject.Destroy(child.gameObject);
    foreach( Transform child in num_7 ) GameObject.Destroy(child.gameObject);
    foreach( Transform child in num_8 ) GameObject.Destroy(child.gameObject);
  }
  
  private void SetChar( string character, int slot ) {
    GameObject obj;
    
    switch( character ) {
      case "1":
        obj = Instantiate( Resources.Load( "Things/keypad_num_1" ) ) as GameObject;
        break;
      case "2":
        obj = Instantiate( Resources.Load( "Things/keypad_num_2" ) ) as GameObject;
        break;
      case "3":
        obj = Instantiate( Resources.Load( "Things/keypad_num_3" ) ) as GameObject;
        break;
      case "4":
        obj = Instantiate( Resources.Load( "Things/keypad_num_4" ) ) as GameObject;
        break;
      case "5":
        obj = Instantiate( Resources.Load( "Things/keypad_num_5" ) ) as GameObject;
        break;
      case "6":
        obj = Instantiate( Resources.Load( "Things/keypad_num_6" ) ) as GameObject;
        break;
      case "7":
        obj = Instantiate( Resources.Load( "Things/keypad_num_7" ) ) as GameObject;
        break;
      case "8":
        obj = Instantiate( Resources.Load( "Things/keypad_num_8" ) ) as GameObject;
        break;
      case "P":
        obj = Instantiate( Resources.Load( "Things/keypad_P" ) ) as GameObject;
        break;
      case "H":
        obj = Instantiate( Resources.Load( "Things/keypad_H" ) ) as GameObject;
        break;
      case "E":
        obj = Instantiate( Resources.Load( "Things/keypad_E" ) ) as GameObject;
        break;
      case "N":
        obj = Instantiate( Resources.Load( "Things/keypad_N" ) ) as GameObject;
        break;
      case "U":
        obj = Instantiate( Resources.Load( "Things/keypad_U" ) ) as GameObject;
        break;
      case "L":
        obj = Instantiate( Resources.Load( "Things/keypad_L" ) ) as GameObject;
        break;
      case "C":
        obj = Instantiate( Resources.Load( "Things/keypad_C" ) ) as GameObject;
        break;
      case "0":
      default:
        obj = Instantiate( Resources.Load( "Things/keypad_num_0" ) ) as GameObject;
        break;
    }
    
    obj.transform.parent = transform.Find( "num_" + slot );
    obj.transform.localPosition = Vector3.zero;
    obj.transform.localScale = Vector3.one;
  }

}