using System.Collections.Generic;
using _Configs.ScriptableObjectsDeclarations;
using _Scripts.Controllers;
using _Scripts.Core.Elements;
using _Scripts.Patterns;
using UnityEngine;

public class CreatorWindow : Singleton<CreatorWindow>
{
    [SerializeField] private ElementInCreatorWindow elementInCreatorWindowPrefab;
    [SerializeField] private ElementInEditMode elementInEditModePrefab;
    [SerializeField] private Transform spawnedElementsParent;
    
    private Dictionary<ElementData, List<ElementInEditMode>> spawnedElements = new Dictionary<ElementData, List<ElementInEditMode>>();

    private bool inited;

    public Transform SpawnedElementsParent => spawnedElementsParent;
    public ElementInEditMode ElementInEditModePrefab => elementInEditModePrefab;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        if(inited) return;

        List<ElementData> elementDatas = ElementsDatabase.Instance.elements;
    
        ElementInCreatorWindow[] elements = new ElementInCreatorWindow[elementDatas.Count];

        for (int i = 0; i < elements.Length; i++)
        {
            if (i == 0)
            {
                elements[i] = elementInCreatorWindowPrefab;
            }
            else
            {
                elements[i] = Instantiate(elementInCreatorWindowPrefab, elementInCreatorWindowPrefab.transform.parent);
            }
            
            elements[i].Init(this, elementDatas[i]);
        }

        inited = true;
    }

    public void AddSpawnedElement(ElementInEditMode element)
    {
        if (spawnedElements.ContainsKey(element.elementData) == false)
        {
            spawnedElements.Add(element.elementData, new List<ElementInEditMode>());
        }
        
        spawnedElements[element.elementData].Add(element);
    }

    public void RemoveSpawnedElement(ElementInEditMode element)
    {
        if (spawnedElements.ContainsKey(element.elementData) == false)
        {
            spawnedElements.Add(element.elementData, new List<ElementInEditMode>());
        }

        spawnedElements[element.elementData].Remove(element);
    }

    public void ClearSpawnedElements()
    {
        foreach (List<ElementInEditMode> spawnedElementsList in spawnedElements.Values)
        {
            for (int i = spawnedElementsList.Count - 1; i >= 0; i--)
            {
                spawnedElementsList[i].DeleteElement();               
            }
        }
    }

    public void EditExistingLevelConfig()
    {
        LevelTextureHolder.Instance.SetTexture(LevelManager.Instance.levelSo.DeserializeSavedImage());
        ClearSpawnedElements();

        foreach (LevelSO.LevelComponentData levelComponentData in LevelManager.Instance.levelSo.elementsData)
        {
            ElementInEditMode spawnedElementInEditMode =
                Instantiate(
                    ElementInEditModePrefab, 
                    levelComponentData.elementScreenPos, 
                    levelComponentData.elementRotation, 
                    SpawnedElementsParent);
            
            spawnedElementInEditMode.Init(this, ElementsDatabase.Instance.GetElementData(levelComponentData.elementName));
            spawnedElementInEditMode.CorrectPressOrder = levelComponentData.elementCorrectPressOrder;
            spawnedElementInEditMode.ElementState = levelComponentData.elementState;
            spawnedElementInEditMode.SetNewLocalScale(levelComponentData.elementScale);
        }
    }

    public List<ElementInEditMode> GetListOfSpawnedElements(ElementInEditMode elementType)
    {
        if (spawnedElements.ContainsKey(elementType.elementData) == false)
        {
            spawnedElements.Add(elementType.elementData, new List<ElementInEditMode>());
        }

        return spawnedElements[elementType.elementData];
    }

    public void FillInLevelDataToConfig()
    {
        LevelManager.Instance.levelSo.AddElementsToDataList(spawnedElements);
    }
}
