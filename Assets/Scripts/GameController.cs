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

  public void Save() {
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat",
                     FileMode.Open);

    PlayerData data = new PlayerData();

    // Add new persistent variables to be saved here.
    data.experiencePoints = experiencePoints;
    data.playerLevel = playerLevel;
    data.score = score;

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
  }
}