using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenShaker : MonoBehaviour {
  private bool shouldShake = false;
  private float magnitude = 0.1f;
  private float duration = 0.1f;

  void PlayShake(List<float> args) {
    // Expects a list of args:
    //   List<float> {magnitude, duration};
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
    // Original code at
    // http://unitytipsandtricks.blogspot.com/2013/05/camera-shake.html
    float elapsed = 0.0f;


    Vector3 originalCamPos = Camera.main.transform.position;
    float randomStart = UnityEngine.Random.Range(-1000.0f, 1000.0f);
    float speed = 1.0f;


    while(elapsed < duration) {
      elapsed += Time.deltaTime;

      float percentComplete = elapsed / duration;
      // Reduce the power to 0, starting halfway through.
      float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

      float alpha = randomStart + speed * percentComplete;

      // Map value to [-1, 1]
      float x = Mathf.PerlinNoise(alpha, 0.0f) * 2.0f - 1.0f;
      float y = Mathf.PerlinNoise(0.0f, alpha) * 2.0f - 1.0f;
      x *= magnitude * damper;
      y *= magnitude * damper;

      Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

      yield return null;
    }

    Camera.main.transform.position = originalCamPos;
  }


}