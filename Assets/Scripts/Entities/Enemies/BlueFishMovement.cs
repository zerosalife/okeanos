using UnityEngine;
using System.Collections;

public class BlueFishMovement : MonoBehaviour {
  private Vector3 currentPosition;
  public float moveSpeed;
  private Vector3 currentDirection;
  private bool changeDirection;

  void Start() {
    currentPosition = transform.position;

    // Always change direction on the first frame.
    currentDirection = Vector3.zero;
    changeDirection = true;
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