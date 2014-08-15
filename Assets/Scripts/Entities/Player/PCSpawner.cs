using UnityEngine;
using System.Collections;

public class PCSpawner : MonoBehaviour {

  public static PCSpawner spawner;

  public GameObject playerPrefab;
  public GameObject playerObject;
  public bool canSpawn;
  private float playerSpawnY = -4.259603f;
  public bool isSwimming;

  void Awake() {
    if(spawner == null) {
      DontDestroyOnLoad(gameObject);
      spawner = this;
    } else if(spawner != this) {
      // There can be only one!
      Destroy(gameObject);
    }
  }

  // Use this for initialization
  void Start () {
    isSwimming = false;
    canSpawn = true;
  }

  // Update is called once per frame
  void Update () {
    //as long as the player isn't swimming, let user determine x position
    if (isSwimming == false && canSpawn == true) {
      Vector3 mPos = Input.mousePosition;
      mPos = Camera.main.ScreenToWorldPoint(mPos);

      //the player hits the left mouse button to select the X position
      if (Input.GetButtonDown("Fire1")){
        playerObject = Instantiate(playerPrefab,
                                   new Vector3(0f, -70.259603f, 0f),
                                   Quaternion.identity) as GameObject;
        playerObject.transform.position = new Vector3(mPos.x,
                                                      playerSpawnY,
                                                      0f);
        isSwimming = true;
      }
    }
  }

  public void Clear() {
    PCSpawner.spawner = null;
    Destroy(this.gameObject);
  }
}