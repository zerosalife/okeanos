using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController: MonoBehaviour {
  public static LevelController control;

  enum EnemyType {
    BlueFish
  }

  public static int enemyTypeCount = System.Enum.GetNames(typeof(EnemyType)).Length;
  public static int numLevels = 1;
  public GameObject[] prefabs;  // Must correspond to EnemyType enum
  public int[,] levelArray = new int[numLevels, enemyTypeCount];
  private Dictionary<EnemyType, SpawnCoords> spawnCoords = new Dictionary<EnemyType, SpawnCoords>();
  public GameObject enemies;

  // private Dictionary<EnemyType, System.Action> spawnFuncs = new Dictionary<EnemyType, System.Action>();
  private int currentLevel;

  void Awake() {
    if(control == null) {
      DontDestroyOnLoad(gameObject);
      control = this;
    } else if(control != this) {
      // There can be only one!
      Destroy(gameObject);
    }
  }

  void Start() {
    currentLevel = GameController.control.stage;

    // Set up all enemies
    /////////////////////

    // BlueFish enemy setup
    spawnCoords.Add(EnemyType.BlueFish,
                    new SpawnCoords(2, new float[] {0, -2}));

    // Spawn enemies in the level
    /////////////////////////////

    foreach (EnemyType e in (EnemyType[]) System.Enum.GetValues(typeof(EnemyType))) {
      float xLim = spawnCoords[e].xLim;
      float topFence = spawnCoords[e].yLims[0];
      float botFence = spawnCoords[e].yLims[1];

      // Spawn a number of enemies of type e as defined in the
      // levelArray.
      for (int i = 0; i < levelArray[currentLevel, (int) e]; i++) {
        float yCoord = Random.Range(topFence, botFence);
        float xCoord = Random.Range(-xLim, xLim);
        GameObject enemy = Instantiate(prefabs[(int) e],
                                       new Vector3(xCoord, yCoord, 0f),
                                       Quaternion.identity) as GameObject;
        enemy.transform.parent = enemies.transform;
      }
    }
  }

  public void Clear() {
    LevelController.control = null;
    Destroy(this.gameObject);
  }

}

public class SpawnCoords {
  public float xLim;
  public float[] yLims = new float[2];

  // Constructor
  public SpawnCoords(float xLim,
                     float[] yLims){
    this.xLim = xLim;
    this.yLims = yLims;

  }

}
