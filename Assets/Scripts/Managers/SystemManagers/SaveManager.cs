using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string defaultPath = Path.Combine(Application.dataPath, "Save");
    private readonly List<ScriptableObject> objects = new();
    void Start()
    {
        LoadGame();
    }
    void OnApplicationQuit()
    {
        SaveGame();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SaveGame();

        if (Input.GetKeyDown(KeyCode.Space))
            LoadGame();
    }

    public void SaveGame()
    {

        foreach (var obj in objects)
        {
            var json = JsonUtility.ToJson(obj, true);

            File.WriteAllText(defaultPath + "/" + obj.name + ".json", json);
            Debug.Log($"Player's {obj.name} saved");

        }


    }
    public void LoadGame()
    {
        foreach (var obj in objects)
        {
            var json = File.ReadAllText(defaultPath + "/" + obj.name + ".json");

            JsonUtility.FromJsonOverwrite(json, obj);

            Debug.Log($"Player's {obj.name} loaded");
        }

    }
}
