using UnityEngine;
using System.Collections;

public class PCSpawnAnimation : MonoBehaviour {
  private float _time;
  public float duration;
  public AnimationCurve scaleCurve;
  private PlayerController playerController;

  void Start() {
    _time = Time.time;
    playerController = gameObject.GetComponent<PlayerController>();
  }

  void Update() {
    Vector3 scale = gameObject.transform.localScale;
    // Evaluate the animation curve normalized to the total duration
    // of the animation to determine a multiplicative factor for
    // scaling.
    float factor = scaleCurve.Evaluate((Time.time - _time) / duration);
    gameObject.transform.localScale = scale * factor;

    // At duration, we destroy this script component.
    StartCoroutine(Exit(duration));
  }

  IEnumerator Exit(float timeToExit) {
    yield return new WaitForSeconds(timeToExit);

    // Turn on PlayerController component
    playerController.enabled = true;

    // Tell the PCSpawner that we are swimming, so it can't spawn
    // another PC fish.
    PCSpawner.isSwimming = true;

    // Disable the PCSpawnAnimation script.
    this.enabled = false;

  }

}