using UnityEngine;
using System.Collections;

public class BlueFishController : MonoBehaviour {

  private Vector3 currentPosition;
  public float moveSpeed;
  private Vector3 currentDirection;
  private bool changeDirection;

  public int experiencePoints = 1;
  public int score;

  public float knockbackAmount;
  private Vector3 knockback;

  // Use this for initialization
  void Start () {
    currentPosition = transform.position;

    // Always change direction on the first frame.
    currentDirection = Vector3.zero;
    changeDirection = true;

    // Calculate the direction for knockback.
    knockback = knockbackAmount * Vector3.down;
  }

  // Update is called once per frame
  void Update () {

    currentPosition = transform.position;

    // Change direction to a random direction.
    if(changeDirection == true) {
      currentDirection = RandomMoveDirection();
      changeDirection = false;
    }

    // Move toward a desired target in the current direction.
    Vector3 target = currentDirection * moveSpeed + currentPosition;
    transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);

    // Only change direction 20% of the time.
    if(changeDirection == false) {
      if (Random.Range(0, 100) > 79) { // Subtract 1 from 80 because
                                       // of 0.
        changeDirection = true;
      }
    }

  }

  void OnTriggerEnter2D(Collider2D collision) {
    // If we collide with the player, add our experience points
    // `experiencePoints' to the player's experience, knock the player
    // back by `knockbackAmount', and destroy ourself.
    if(collision.gameObject.tag == "Player") {
      GameObject playerObject = GameObject.FindGameObjectsWithTag("Player")[0];

      Debug.Log(GameController.control);

      // If the player has a higher level than our experience points,
      // we dead.  TODO: Consider having a separate `enemyLevel'
      // variable?
      if (GameController.control.playerLevel >= experiencePoints) {
        // Is the player stronger than our experience level?  Then we
        // die.
        playerObject.SendMessage("AddExperience", experiencePoints);
        playerObject.SendMessage("Knockback", knockback);
        GameController.control.score += score;

        Destroy(this.gameObject);
      } else if(GameController.control.playerLevel < experiencePoints) {
        // We are stronger than the player. So, kill the player.
        playerObject.SendMessage("Die");
      }
    }
  }

  Vector3 RandomMoveDirection() {
    // Choose a random direction to move in.  Typically called to
    // change `currentDirection'.
    Vector3 direction = Vector3.zero;
    switch(Flip()) {
    case 0:
      direction = Vector3.right;
      break;
    case 1:
      direction = Vector3.left;
      break;
    }
    return direction;
  }

  int Flip() {
    // Simulate a coin flip to produce 50:50 chance of [0, 1]
    return Random.Range(0, 2);
  }
}
