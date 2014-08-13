using UnityEngine;
using System.Collections;

public class BlueFishController : MonoBehaviour {

  public int experiencePoints = 1;
  public int score;

  public float knockbackAmount;
  private Vector3 knockback;

  public AudioClip enemyHit;

  private bool alreadyCollided = false;

  // Use this for initialization
  void Start () {
    // Calculate the direction for knockback.
    knockback = knockbackAmount * Vector3.down;
  }



  IEnumerator OnTriggerEnter2D(Collider2D collision) {
    // If we collide with the player, add our experience points
    // `experiencePoints' to the player's experience, knock the player
    // back by `knockbackAmount', and destroy ourself.
    if(collision.gameObject.tag == "Player" & !alreadyCollided) {
      GameObject playerObject = GameObject.FindGameObjectsWithTag("Player")[0];

      Debug.Log(GameController.control);

      // If the player has a higher level than our experience points,
      // we dead.  TODO: Consider having a separate `enemyLevel'
      // variable?
      if (GameController.control.playerLevel >= experiencePoints) {
        // Is the player stronger than our experience level?  Then we
        // die.

        audio.PlayOneShot(enemyHit);
        // TODO: FIXME: Refactor to remove AddExperience method from
        // player, seems like it's too complicated.  Just go ahead and
        // add experience directly to the GameController.control, just
        // like we do with the score below.
        playerObject.SendMessage("AddExperience", experiencePoints);
        playerObject.SendMessage("Knockback", knockback);
        GameController.control.score += score;

        gameObject.renderer.enabled = false; // Make the sprite disappear.
        alreadyCollided = true;
        yield return new WaitForSeconds(0.136f); // Wait for the audio to play.
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
