using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor( typeof(Clicker), true )]
[CanEditMultipleObjects]
public class ClickerEditor : Editor {

    void OnSceneGUI() {
      Clicker clicker = Selection.activeGameObject.GetComponent<Clicker>();
      clicker.movePoint = Handles.PositionHandle( clicker.movePoint, Quaternion.identity );
      
      if (GUI.changed) EditorUtility.SetDirty (target);
    }
 
}