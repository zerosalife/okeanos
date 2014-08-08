using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HitCounterController : MonoBehaviour {
  public GameObject hitCounterPrefab;
  public List<GameObject> hitCounters = new List<GameObject>();

  public GameObject baseEnemy;
  private Transform baseTransform;

  public Vector3 radius;


  void Start() {
    baseTransform = baseEnemy.transform;
  }

  void SpawnHitCounters(int numberToSpawn) {
    // Spawn a number of hitCounterPrefab GameObjects and add them to
    // the hitCounters list.
  }

  void DestroyHitCounter() {
    GameObject hc = hitCounters[0];
    hitCounters.RemoveAt(0);
    Destroy(hc);
  }



}