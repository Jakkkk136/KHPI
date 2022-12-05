using _Scripts.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "CanvasConfig", menuName = "KHPI/Canvas Config", order = 0)]
public class CanvasConfig : ScriptableObject
{
    [SerializeField] private float delayShowingWinWindow = 1.5f;
    [SerializeField] private float delayShowingLoseWindow = 1.5f;
    [SerializeField] private float delayShowingPlayerProgress = 1f;
    [SerializeField] private float delayShowingNextLevelButton = 2.3f;
    
    [Space] 
    [SerializeField] private eWindowType startingWindow = eWindowType.Menu;
    
    public float DelayShowingWinWindow => delayShowingWinWindow;
    public float DelayShowingLoseWindow => delayShowingLoseWindow;
    public float DelayPlayerProgress => delayShowingPlayerProgress;
    public float DelayShowingNextLevelButton => delayShowingNextLevelButton;

    public eWindowType StartingWindow => startingWindow;
}