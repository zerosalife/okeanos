using UnityEngine;
using System.Collections;

public class PCSpawnAnimation : MonoBehaviour {
  private float _time;
  public float timeToFade;
  private PlayerController pc;

  void Start() {
    _time = Time.time;
    pc = gameObject.GetComponent<PlayerController>();
  }

  void Update() {
    // Fade the alpha out over time.
    Color fadeAlpha = renderer.material.color;
    // At timeToFade, the alpha will approach 1.
    fadeAlpha.a = Mathf.Sin((Time.time - _time) * ((Mathf.PI/2)/timeToFade));
    renderer.material.color = fadeAlpha;
    // At timeToFade, we destroy this script component.
    StartCoroutine(Exit(timeToFade));
  }

  IEnumerator Exit(float timeToExit) {
    yield return new WaitForSeconds(timeToExit);

    pc.enabled = true;
    PCSpawner.isSwimming = true;

    this.enabled = false;

  }

}