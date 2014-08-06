using UnityEngine;
using System.Collections;

public class ScoreDisplay: MonoBehaviour {

  void OnGUI() {
    GUI.Label(new Rect(0, 0, 100, 30), "Score: " +
              GameController.control.score);
    GUI.Label(new Rect(100, 0, 100, 30), "Lvl: " +
              GameController.control.playerLevel);
  }

}
