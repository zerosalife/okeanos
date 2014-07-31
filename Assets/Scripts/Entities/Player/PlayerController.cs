using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
  private bool canSpawn;
  private Vector3 currentPosition;
  public float moveSpeed = 1.5f;

  // Use this for initialization
  void Start () {
    currentPosition = gameObject.transform.position;

    // playerLevel = 1;
  }

  // Update is called once per frame
  void Update () {
    currentPosition = transform.position;

    // Always march up.
    Vector3 target = Vector3.up * moveSpeed + currentPosition;
    transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);

    // If we're off the top of the screen, go to the next screen.
    if(currentPosition.y >= 5.5) {
      Application.LoadLevel("Level02");
    }

  }

  void Die() {
    Application.LoadLevel("Gameover");
  }

  void AddExperience(int points) {
    // Add experience points, then check for level up.
    GameController.control.experiencePoints += points;
    CheckLevelUp();
  }

  void CheckLevelUp() {
    int playerLevel = GameController.control.playerLevel;
    int experiencePoints = GameController.control.experiencePoints;
    int cutoff = (int)Mathf.Ceil(Mathf.Pow((float)playerLevel, 1.5f));
    Debug.Log("Cutoff: " + cutoff);
    Debug.Log("Current xp: " + experiencePoints);
    if (experiencePoints >= cutoff) {
      LevelUp();
    }
  }

  void LevelUp() {
    GameController.control.experiencePoints = 0;
    GameController.control.playerLevel++;
    Debug.Log("Level: " + GameController.control.playerLevel);
  }

  void Knockback(Vector3 distance) {
    // Knock the player back by `distance'.
    transform.position = currentPosition + distance;
    currentPosition = transform.position;
  }
}
