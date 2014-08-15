using UnityEngine;
using System.Collections;

public class BigBaddieController: MonoBehaviour {

  public int hitsRequired;
  public int enemyLevel;    // The effective level required to hit the
                            // BigBaddie
  public int enemyScore;
  public float knockbackAmount;
  public GameObject hitCounterControllerObject;
  private Vector3 knockback;

  public AudioClip bigEnemyHit;

  void Start() {
    knockback = knockbackAmount * Vector3.down;
  }

  IEnumerator OnTriggerEnter2D(Collider2D collision) {
    if(collision.gameObject.tag == "Player") {
      GameObject playerObject = GameObject.FindGameObjectsWithTag("Player")[0];

      int playerLevel = GameController.control.playerLevel;

      if(playerLevel >= enemyLevel) {
        // We got hit, reduce hitsRequired, increase enemyLevel, and
        // knockback
        hitsRequired--;
        hitCounterControllerObject.SendMessage("DestroyHitCounter");
        audio.PlayOneShot(bigEnemyHit);
        enemyLevel++;

        GameController.control.score += enemyScore;
        // Check for our death.
        if(hitsRequired <= 0) {
          gameObject.renderer.enabled = false; // Make the sprite disappear.
          yield return new WaitForSeconds(0.397f);
          Destroy(this.gameObject);

          // Go to win scene and destroy player.
          playerObject.SendMessage("Die");
          //Application.LoadLevel("WinScreen");
        }

        playerObject.SendMessage("Knockback", knockback);

      } 
      else {
        Debug.Log("Player should die. playerLevel: " +
                  playerLevel + " enemyLevel: " +
                  enemyLevel);
        playerObject.SendMessage("Die");
        // Go to gameover scene.
        Application.LoadLevel("WinScreen");
      }
    }
  }
}