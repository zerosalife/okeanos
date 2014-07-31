using UnityEngine;
using System.Collections;

public class BigBaddieController: MonoBehaviour {

  public int hitsRequired;
  public int enemyLevel;    // The effective level required to hit the
                            // BigBaddie

  public float knockbackAmount;
  private Vector3 knockback;

  void Start() {
    knockback = knockbackAmount * Vector3.down;
  }

  void OnTriggerEnter2D(Collider2D collision) {
    if(collision.gameObject.tag == "Player") {
      GameObject playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
      PlayerController playerScript = playerObject.GetComponent<PlayerController>();

      if(playerScript.playerLevel >= enemyLevel) {
        // We got hit, subtract a level from the player, reduce
        // hitsRequired and enemyLevel, and knockback
        hitsRequired--;
        enemyLevel++;

        // Check for death
        if(hitsRequired <= 0) {
          Destroy(this.gameObject);

          // Go to win scene.
          Application.LoadLevel("WinScreen");
        }

        playerObject.SendMessage("Knockback", knockback);
      } else // if(playerScript.playerLevel < enemyLevel)
          {
        Debug.Log("Player should die. playerLevel: " +
                  playerScript.playerLevel + " enemyLevel: " +
                  enemyLevel);
        // Go to gameover scene.
      }
    }
  }
}