using UnityEngine;
using System.Collections;

public class BlueFishController : MonoBehaviour {

  public int experiencePoints = 1;
  public int score;

  public float knockbackAmount;
  private Vector3 knockback;

  public AudioClip enemyHit;

  private bool alreadyCollided = false;

  public Shader flashShader;

  // For the Experience popup
  public GameObject guiExpPopupPrefab;
  private GameObject guiExpPopup;


  // Use this for initialization
  void Start () {
    // Calculate the direction for knockback.
    knockback = knockbackAmount * Vector3.down;
  }



  void OnTriggerEnter2D(Collider2D collision) {
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

        // Flash the sprite to white.
        renderer.material.shader = flashShader;

        playerObject.SendMessage("AddExperience", experiencePoints);

        // Instantiate exp popup.
        guiExpPopup = Instantiate(guiExpPopupPrefab,
                                  Camera.main.WorldToViewportPoint(gameObject.transform.position),
                                  Quaternion.identity) as GameObject;
        guiExpPopup.SendMessage("SetText", experiencePoints);

        playerObject.SendMessage("Knockback", knockback);
        GameController.control.score += score;

        alreadyCollided = true;
        Destroy(this.gameObject, 0.136f);
      } else if(GameController.control.playerLevel < experiencePoints) {
        // We are stronger than the player. So, kill the player.
        playerObject.SendMessage("Die");
      }
    }
  }
}
