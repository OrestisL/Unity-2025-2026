using UnityEngine;

[System.Serializable]
public class SerializedPlayerData
{
    public float Score = 0;
}

public class PlayerData : GenericSingleton<PlayerData>
{
    [SerializeField]
    SerializedPlayerData _data;

    public void Init()
    {
        if (!Utilities.LoadData("data", out _data))
            _data = new();
    }

    public void AddScore(float score)
    {
        _data.Score += score;
    }

    private void OnApplicationQuit()
    {
        Utilities.SaveData(_data, "data");
    }
}
