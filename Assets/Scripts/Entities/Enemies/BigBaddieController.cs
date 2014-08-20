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


  private int stunnedFrames = 10;
  private int stunnedCount = 10;


  void Start() {
    knockback = knockbackAmount * Vector3.down;
  }

  void Update() {
    if(stunnedCount < stunnedFrames) {
      stunnedCount++;
      Color c1 = new Color(Random.Range(0.0f, 1.0f),
                           Random.Range(0.0f, 1.0f),
                           Random.Range(0.0f, 1.0f));
      Color c2 = new Color(Random.Range(0.0f, 1.0f),
                           Random.Range(0.0f, 1.0f),
                           Random.Range(0.0f, 1.0f));
      renderer.material.SetColor("_Color1out", c1);
      renderer.material.SetColor("_Color2out", c2);
    } else {
      Color c1 = new Color(0.0f,
                           85.0f/255.0f,
                           136.0f/255.0f);
      Color c2 = new Color(0.0f, 0.0f, 0.0f);
      renderer.material.SetColor("_Color1out", c1);
      renderer.material.SetColor("_Color2out", c2);

    }

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

        stunnedCount = 0;

        GameController.control.score += enemyScore;
        // Check for our death.
        if(hitsRequired <= 0) {
          gameObject.renderer.enabled = false; // Make the sprite disappear.
          yield return new WaitForSeconds(0.397f);
          Destroy(this.gameObject);

          // Go to win scene.
          Application.LoadLevel("WinScreen");
        }

        playerObject.SendMessage("Knockback", knockback);


      } else // if(playerScript.playerLevel < enemyLevel)
        // The player should die
          {
        Debug.Log("Player should die. playerLevel: " +
                  playerLevel + " enemyLevel: " +
                  enemyLevel);
        // Go to gameover scene.
        Application.LoadLevel("WinScreen");
      }
    }
  }
}