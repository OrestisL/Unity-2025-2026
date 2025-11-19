using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;

public static class Utilities
{
    public static void ChangeScene(string name, MonoBehaviour runner)
    {
        runner.StartCoroutine(LoadSceneAsync(name));
    }

    private static IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        operation.allowSceneActivation = false;
        while (operation.progress < 0.9f)
        {
            yield return null;
        }
        operation.allowSceneActivation = true;
    }

    public static void SaveData<T>(T data, string name, string directory = "") where T : class
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        if (string.IsNullOrEmpty(directory))
            directory = Application.persistentDataPath;

        if (!name.EndsWith(".json"))
            name = string.Format("{0}.json", name);

        string fullPath = Path.Combine(directory, name);
        File.WriteAllText(fullPath, json);
    }

    public static bool LoadData<T>(string name, out T result, string directory = "") where T : class
    {
        result = default;

        if (string.IsNullOrWhiteSpace(name))
            throw new System.Exception($"Cannot read file without a name");

        if (string.IsNullOrWhiteSpace(directory))
            directory = Application.persistentDataPath;

        if (!name.EndsWith(".json"))
            name = string.Format("{0}.json", name);

        string fullPath = Path.Combine (directory, name);
        if (!File.Exists(fullPath))
            return false;

        string json = File.ReadAllText(fullPath);

        result = JsonConvert.DeserializeObject<T>(json);

        return result != null;
    }
}
