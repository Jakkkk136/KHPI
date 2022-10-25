using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;

public abstract class ClickableElement : MonoBehaviour
{
    private static List<ClickableElement> clickedElements = new List<ClickableElement>();

    [SerializeField] private MeshRenderer[] thisRenderers;
    
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material correctMaterial;
    [SerializeField] private Material wrongMaterial;
    [Space]
    [SerializeField] private int correctPressIndex;
    [SerializeField] private TextMeshProUGUI pressedIndexText;
    
    private bool pressed = false;
    private bool pressedInCorrectOrder = false;

    private int currentIndex;
    
    private void Awake()
    {
        SetClearTextIndex();
    }

    private void OnDisable()
    {
        clickedElements.Clear();
    }

    public void Press()
    {
        pressed = !pressed;

        if (pressed)
        {
            clickedElements.Add(this);
            int clickedElementsAmount = clickedElements.Count;
            currentIndex = clickedElementsAmount;
            UpdateTextIndex();
            TestIndexIsCorrect();
            SetCorrectMaterial();
        }
        else
        {
            pressedInCorrectOrder = false;
            thisRenderers.ForEach(r => r.material = defaultMaterial);
            clickedElements.Remove(this);
            SetClearTextIndex();
            UpdateElementsWithHigherIndexes(currentIndex);
        }
        
        OnPress(pressed);
    }

    private void UpdateElementsWithHigherIndexes(int removedElementIndex)
    {
        foreach (ClickableElement element in clickedElements)
        {
            if (element.currentIndex >= removedElementIndex)
            {
                element.DecreaseIndexByOne();
            }
        }

        foreach (ClickableElement element in clickedElements)
        {
            if (element.currentIndex >= removedElementIndex)
            {
                element.TestIndexIsCorrect();
            }
        }

        foreach (ClickableElement element in clickedElements)
        {
            if (element.currentIndex >= removedElementIndex)
            {
                element.SetCorrectMaterial();
            }
        }
    }

    protected abstract void OnPress(bool pressed);

    private void DecreaseIndexByOne()
    {
        currentIndex--;
        UpdateTextIndex();
    }
    
    private void TestIndexIsCorrect()
    {
        pressedInCorrectOrder = correctPressIndex == currentIndex;
    }

    private void SetCorrectMaterial()
    {
        thisRenderers.ForEach(r => r.material =
            clickedElements.TrueForAll(ce => ce.pressedInCorrectOrder) ? correctMaterial : wrongMaterial);
    }

    private void UpdateTextIndex()
    {
        pressedIndexText.SetText(currentIndex.ToString());
    }

    private void SetClearTextIndex()
    {
        pressedIndexText.SetText(String.Empty);

    }

    public void SetText(string text)
    {
        pressedIndexText.SetText(text);
    }
}
