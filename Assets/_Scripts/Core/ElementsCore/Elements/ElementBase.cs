using System;
using _Configs.ScriptableObjectsDeclarations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class ElementBase : MonoBehaviour
{
    [SerializeField] protected Image activeStateSpriteHolder;
    [SerializeField] [PropertyOrder(-1)] protected TextMeshProUGUI orderPressText;
    
    [Header("Sets Dynamically")]
    public ElementData elementData;

    protected bool _elementState;
    protected int _correctPressOrder;

    
    public bool ElementState
    {
        get => _elementState;
        set
        {
            _elementState = value;
            activeStateSpriteHolder.sprite = _elementState ? elementData.activeStateSprite : elementData.inactiveStateSprite;
        }
    }

    protected void SetDefaultScaleOfText()
    {
        Vector3 thisScale = transform.localScale;
        thisScale.z = 1f;
        Vector3 scaleForText = Vector3.one;

        scaleForText.x /= thisScale.x;
        scaleForText.y /= thisScale.y;
        scaleForText.z /= thisScale.z;

        orderPressText.transform.localScale = scaleForText;
    }

}
