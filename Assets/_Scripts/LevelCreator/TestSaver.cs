using _Scripts.LevelCreator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestSaver : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Button button;
    [SerializeField] private LevelSO level;

    private void Awake()
    {
        button.onClick.AddListener(Save);
    }

    private void Save()
    {
        level.saveString = input.text;

        string data = LevelSerializer.SerializeLevel(level);
        LevelSerializer.SaveSerializedLevelOnDesktop(data, "Test level");
    }
}
