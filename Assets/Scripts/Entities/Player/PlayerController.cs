using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
  public static PlayerController pcontrol;
  private bool _canSpawn;
  private bool _swimming;   // To track whether the player has begun his fishy journey
  private Vector3 currentPosition;
  public float moveSpeed = 1.5f;



  void Awake() {
    if(pcontrol == null) {
      DontDestroyOnLoad(gameObject);
      pcontrol = this;
    } else if(pcontrol != this) {
      // There can be only one!
      Destroy(gameObject);
    }
  }

  // Use this for initialization
  void Start () {
    currentPosition = gameObject.transform.position;

    // playerLevel = 1;
  }

  // Update is called once per frame
  void Update () {
    currentPosition = transform.position;

    if(_swimming) {
      // Always march up.
      Vector3 target = Vector3.up * moveSpeed + currentPosition;
      transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);

      // If we're off the top of the screen, go to the next screen.
      if(currentPosition.y >= 5.5 && Application.loadedLevelName.Equals("Level01")) {
        Application.LoadLevel("Level02");
        transform.position = new Vector3(0f, -4f, 0f);
      } //If we're off the top of the boss screen, the game is over!
      else if (currentPosition.y >= 5.5 && Application.loadedLevelName.Equals("Level02")) {
        Die();
      }
    }
  }



  ///////////////////////////
  //// Setters & Getters   //
  /////////////////////////// 

  public bool Swimming {
    get { return this._swimming; }
    set { this._swimming = value; }
  }

  ///////////////////////////
  //// Our Utility Methods 
  ///////////////////////////

  void Die() {
    // Application.LoadLevel("Gameover");
    Application.LoadLevel("WinScreen");
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
