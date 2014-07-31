using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController: MonoBehaviour {
  public static GameController control;

  public int experiencePoints;
  public int playerLevel;
  // public seed randomSeed;

  void Awake() {
    if(control == null) {
      DontDestroyOnLoad(gameObject);
    } else if(control != this) {
      // There can be only one!
      Destroy(gameObject);
    }
  }

  public void Save() {
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat",
                     FileMode.Open);

    PlayerData data = new PlayerData();
    data.experiencePoints = experiencePoints;
    data.playerLevel = playerLevel;

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

      experiencePoints = data.experiencePoints;
      playerLevel = data.playerLevel;
    }
  }

  [Serializable]
  class PlayerData {
    // TODO: see about making gets and sets.
    public int experiencePoints;
    public int playerLevel;
  }
}