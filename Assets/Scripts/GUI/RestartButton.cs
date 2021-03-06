using UnityEngine;
using System.Collections;

public class RestartButton: MonoBehaviour {

  void Awake() {
    // Clear the instances of various spawners and controllers that
    // are only needed for a single playthrough.
    PCSpawner.spawner.Clear();
    LevelController.control.Clear();
  }

  void OnGUI() {
    GUI.Label(new Rect(Screen.width / 2 - 30, 60, 100, 30), "Game Over");
    GUI.Label(new Rect(Screen.width / 2 - 35, 80, 100, 30), "Score: " +
              GameController.control.score);
    if(GUI.Button(new Rect(Screen.width / 2 - 50,
                           Screen.height / 2 + 60, 100, 50), "Main Menu")) {
      Application.LoadLevel("Opening");
    }

  }

}