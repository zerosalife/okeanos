using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController: MonoBehaviour {
  public static GameController control;

  public int experiencePoints;
  public int playerLevel;
  public int score;
  public int stage;
  private bool sleep = false;
  private float sleepDelta;
  private float sleepStartTime;
  // Add new persistent variables here.

  // public seed randomSeed;

  void Awake() {
    if(control == null) {
      DontDestroyOnLoad(gameObject);
      control = this;
    } else if(control != this) {
      // There can be only one!
      Destroy(gameObject);
    }
  }

  void Update() {
    // See Sleep method:
    if(sleep == true) {
      Time.timeScale = 0f;
      if(Time.realtimeSinceStartup - sleepStartTime >= sleepDelta) {
        Time.timeScale = 1.0f;
        sleep = false;
      }
    }
  }

  public void Save() {
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat",
                     FileMode.Open);

    PlayerData data = new PlayerData();

    // Add new persistent variables to be saved here.
    data.experiencePoints = experiencePoints;
    data.playerLevel = playerLevel;
    data.score = score;
    data.stage = stage;

    bf.Serialize(file, data);
    file.Close();
  }

  public void Load() {
    if(File.Exists(Application.persistentDataPath + "/gameInfo.dat")) {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat",
                                  FileMode.Open);
      PlayerData data = (PlayerData)bf.Deserialize(file);
      file.Close();

      // Add new persistent variables to be loaded here.
      experiencePoints = data.experiencePoints;
      playerLevel = data.playerLevel;
      score = data.score;
    }
  }

  [Serializable]
  class PlayerData {
    // TODO: see about making gets and sets.
    // TODO: automate the generation of this data structure.  See:
    // http://forums.devx.com/showthread.php?170650-How-to-dynamically-add-property-to

    // Add new variables for loading and saving here.
    public int experiencePoints;
    public int playerLevel;
    public int score;
    public int stage;
  }

  public void Sleep(float delta) {
    sleep = true;
    sleepDelta = delta;
    sleepStartTime = Time.realtimeSinceStartup;
  }
}