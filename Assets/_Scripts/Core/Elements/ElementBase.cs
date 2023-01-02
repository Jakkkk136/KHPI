using System.Collections;
using System.Collections.Generic;
using _Configs.ScriptableObjectsDeclarations;
using UnityEngine;
using UnityEngine.UI;

public class ElementBase : MonoBehaviour
{
    [SerializeField] private Image activeStateHolder;
    [SerializeField] private Image inactiveStateHolder;

    public void InitElement(Vector2 screenPos, Vector2 scale, bool currentState)
    {
        
    }
    
    public void InitElement(Sprite activeState, Sprite inactiveState, bool currentState)
    {
        
    }
}
