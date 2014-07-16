using UnityEngine;
using System.Collections;

public class BlueFishController : MonoBehaviour {

  private Vector3 currentPosition;
  public float moveSpeed;
  private Vector3 currentDirection;
  private bool changeDirection;

  public int experiencePoints = 1;

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
    if(changeDirection == true) {
      currentDirection = RandomMoveDirection();
      changeDirection = false;
    }

    Vector3 target = currentDirection * moveSpeed + currentPosition;
    transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);

    if(changeDirection == false) {
      if (Random.Range(0, 100) > 80) {
        changeDirection = true;
      }
    }

  }

  void OnTriggerEnter2D(Collider2D collision) {
    if(collision.gameObject.tag == "Player") {
      GameObject playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
      playerObject.SendMessage("AddExperience", experiencePoints);
      playerObject.SendMessage("Knockback", knockback);

      Destroy(this.gameObject);
    }
  }

  Vector3 RandomMoveDirection() {
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
