using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenShaker : MonoBehaviour {
  private bool shouldShake = false;
  private float magnitude = 0.1f;
  private float duration = 0.1f;

  void PlayShake(List<float> args) {
    shouldShake = true;
    magnitude = args[0];
    duration = args[1];
  }

  void Update () {
    if(shouldShake) {
      shouldShake = false;
      StartCoroutine("Shake");
    }
  }


  IEnumerator Shake () {
    float elapsed = 0.0f;


    Vector3 originalCamPos = Camera.main.transform.position;

    while(elapsed < duration) {
      elapsed += Time.deltaTime;

      float percentComplete = elapsed / duration;
      float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

      // Map value to [-1, 1]
      float x = UnityEngine.Random.value * 2.0f - 1.0f;
      float y = UnityEngine.Random.value * 2.0f - 1.0f;
      x *= magnitude * damper;
      y *= magnitude * damper;

      Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

      yield return null;
    }

    Camera.main.transform.position = originalCamPos;
  }


}