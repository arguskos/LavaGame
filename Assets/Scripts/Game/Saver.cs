using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ResourceData {
    public ResourceType ResourceType;
    public int Count;
}

[System.Serializable]
public class SaveData {
    public List<ResourceData> Resources = new List<ResourceData>();
}

public class Saver : MonoBehaviour {
    [SerializeField] ResourceHolder _resourceHolder;

    public void SaveGame() {
        var saveData = new SaveData();
        var resources = _resourceHolder.GetResources();
        foreach (var resource in resources) {
            saveData.Resources.Add(
                new ResourceData() {
                    ResourceType = resource.Key,
                    Count = resource.Value
                });
        }
        var path = GetSavePath();
        Debug.Log(path);
        var json = JsonUtility.ToJson(saveData);
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, json);
    }

    public void LoadGame() {
        var path = GetSavePath();
        if (!File.Exists(path)) {
            return;
        }
        var json = File.ReadAllText(path);
        var saveData = JsonUtility.FromJson<SaveData>(json);
        foreach (var resource in saveData.Resources) {
            _resourceHolder.Add(resource.ResourceType, resource.Count);
        }
    }

    private string GetSavePath() {
        var path = Application.persistentDataPath;
        path = Path.Combine(path, SceneManager.GetActiveScene().name, "save");
        return path;


    }
}
