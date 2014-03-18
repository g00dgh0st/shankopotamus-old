using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {
  public string name;
  public string label;
  public string description;
  public Sprite sprite;
  public ItemCombo[] combos;
}