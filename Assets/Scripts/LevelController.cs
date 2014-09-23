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
  public LevelArray[] levels = new LevelArray[numLevels];
  private Dictionary<EnemyType, SpawnCoords> spawnCoords = new Dictionary<EnemyType, SpawnCoords>();
  public GameObject enemies;

  // private Dictionary<EnemyType, System.Action> spawnFuncs = new Dictionary<EnemyType, System.Action>();
  public int currentLevel;

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
                    new SpawnCoords(2,                     // xLim
                                    new float[] {0, -2},   // yLims
                                    // rotation
                                    Quaternion.Euler(new Vector3(0, -180, -90))));

    // Spawn enemies in the level
    /////////////////////////////

    foreach (EnemyType e in (EnemyType[]) System.Enum.GetValues(typeof(EnemyType))) {
      float xLim = spawnCoords[e].xLim;
      float topFence = spawnCoords[e].yLims[0];
      float botFence = spawnCoords[e].yLims[1];
      Quaternion rotation = spawnCoords[e].rotation;

      // Spawn a number of enemies of type e as defined in the
      // levels array.
      for (int i = 0; i < levels[currentLevel][(int) e]; i++) {
        float yCoord = Random.Range(topFence, botFence);
        float xCoord = Random.Range(-xLim, xLim);
        GameObject enemy = Instantiate(prefabs[(int) e],
                                       new Vector3(xCoord, yCoord, 0f),
                                       rotation) as GameObject;
        enemy.transform.parent = enemies.transform;
      }
    }
  }

  public void Clear() {
    LevelController.control = null;
    Destroy(this.gameObject);
  }

  public static int GetEnemyTypeCount() {
    return enemyTypeCount;
  }

}

public class SpawnCoords {
  public float xLim;
  public float[] yLims = new float[2];
  public Quaternion rotation;

  // Constructor
  public SpawnCoords(float xLim,
                     float[] yLims,
                     Quaternion rotation){
    this.xLim = xLim;
    this.yLims = yLims;
    this.rotation = rotation;

  }
}

[System.Serializable]
public class LevelArray {
  public int[] enemyTypeSpawnArray = new int[8];

  public int this[int index] {
    get {
      return enemyTypeSpawnArray[index];
    }

    set {
      enemyTypeSpawnArray[index] = value;
    }
  }
}
