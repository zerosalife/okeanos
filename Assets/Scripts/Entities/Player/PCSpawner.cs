using UnityEngine;
using System.Collections;

public class PCSpawner : MonoBehaviour {

  public static PCSpawner spawner;

  public GameObject playerPrefab;
  public GameObject playerObject;
  public bool canSpawn;
  private float playerSpawnY = -4.259603f;
  private float playerSpawnX = 0f;
  private PlayerController pc;
  public PCSpawnAnimation pcsa;
  public static bool isSwimming;

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
    playerObject = Instantiate(playerPrefab,
                               new Vector3(playerSpawnX, playerSpawnY, 0f),
                               Quaternion.identity) as GameObject;
    pc = playerObject.GetComponent<PlayerController>();
    pc.enabled = false;
    pcsa = playerObject.GetComponent<PCSpawnAnimation>();
    pcsa.enabled = false;
  }

  // Update is called once per frame
  void Update () {
    //as long as the player isn't swimming, let user determine x position
    if (isSwimming == false && canSpawn == true) {
      Vector3 mPos = Input.mousePosition;
      mPos = Camera.main.ScreenToWorldPoint(mPos);

      //the player hits the left mouse button to select the X position
      if (Input.GetButtonDown("Fire1")){
        playerObject.transform.position = new Vector3(mPos.x,
                                                      playerSpawnY,
                                                      0f);
        pcsa.enabled = true;
      }
    }
  }

  public void Clear() {
    PCSpawner.spawner = null;
    Destroy(this.gameObject);
  }
}