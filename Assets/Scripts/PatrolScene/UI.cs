using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(BehaviorManager.Instance.TogglePause);
    }
}
