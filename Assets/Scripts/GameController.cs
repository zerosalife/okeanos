using UnityEngine;
using System.Collections;

public class GameController: MonoBehaviour {
  public static GameController control;

  public int experiencePoints;
  public int playerLevel;
  // public seed randomSeed;

  void Awake() {
    if(control == null) {
      DontDestroyOnLoad(gameObject);
    } else if(control != this) {
      // There can be only one!
      Destroy(gameObject);
    }
  }
}