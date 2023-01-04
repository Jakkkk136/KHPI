using System;
using _Configs.ScriptableObjectsDeclarations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementBase : MonoBehaviour
{
    [SerializeField] private Image activeStateHolder;
    [SerializeField] [PropertyOrder(-1)] private TextMeshProUGUI orderPressText;
    
    public ElementData elementData;

    private int correctPressOrder;
    private bool _elementState;
    private int _correctPressOrder;

    
    public bool ElementState
    {
        get => _elementState;
        set
        {
            _elementState = value;
            activeStateHolder.sprite = _elementState ? elementData.activeStateSprite : elementData.inactiveStateSprite;
        }
    }

    public int CorrectPressOrder
    {
        get => _correctPressOrder;
        set
        {
            _correctPressOrder = value;
            orderPressText.text = _correctPressOrder > 0 ? _correctPressOrder.ToString() : String.Empty;
        }
    }
    
    public ElementBase Init(ElementData data)
    {
        elementData = data;
        return this;
    }
    
    public void InitElement(Transform parent, Vector3 screenPos, Vector3 scale, Quaternion rotation, bool currentState, int correctPressOrder)
    {
        transform.parent = parent;
        transform.SetPositionAndRotation(screenPos, rotation);
        transform.localScale = scale;

        ElementState = currentState;
        CorrectPressOrder = correctPressOrder;
    }
}
