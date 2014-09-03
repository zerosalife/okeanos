using UnityEngine;
using System.Collections;

public class ExpPopupController : MonoBehaviour {
  private float _time;
  public float timeToFade;

  void Start() {
    _time = Time.time;
  }

  void Update() {
    transform.Translate(new Vector3(0f, 0.001f, 0f));
    // Fade the alpha out over time.
    Color fadeAlpha = guiText.material.color;
    // At timeToFade, the alpha will approach 0.
    fadeAlpha.a = Mathf.Cos((Time.time - _time) * ((Mathf.PI/2)/timeToFade));
    guiText.material.color = fadeAlpha;
    // At timeToFade, we destroy this gameObject.
    Destroy(gameObject, timeToFade);
  }

  void SetText(int xp) {
    guiText.text = "+ " + xp + " exp";
  }
}