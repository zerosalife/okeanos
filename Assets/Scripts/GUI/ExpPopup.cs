using UnityEngine;
using System.Collections;

public class ExpPopup : MonoBehaviour {
  private float _time;
  public float timeToFade;

  void Start() {
    _time = Time.time;
  }

  void Update() {
    transform.Translate(new Vector3(0f, 0.001f, 0f));
    // Fade the alpha out over time.
    Color fadeAlpha = guiText.material.color;
    fadeAlpha.a = Mathf.Cos((Time.time - _time) * ((Mathf.PI/2)/timeToFade));
    guiText.material.color = fadeAlpha;
    Destroy(gameObject, timeToFade);
  }

}