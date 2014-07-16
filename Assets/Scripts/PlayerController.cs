using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
  private bool canSpawn;
  private Vector3 currentPosition;
  private int playerLevel;
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


  }

  void AddExperience(int points) {
    Debug.Log(points);
  }

  void Knockback(Vector3 distance) {
    transform.position = currentPosition + distance;
    currentPosition = transform.position;
  }
}
