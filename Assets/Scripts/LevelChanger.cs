using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public string LevelName;

    private void OnTriggerEnter(Collider other)
    {
        if (string.IsNullOrWhiteSpace(LevelName))
            return;

        Utilities.ChangeScene(LevelName, this);
    }
}
