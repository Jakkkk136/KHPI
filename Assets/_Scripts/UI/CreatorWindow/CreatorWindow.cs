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

        List<ElementData> datas = ElementsDatabase.Instance.elements;
    
        ElementInCreatorWindow[] elements = new ElementInCreatorWindow[datas.Count];

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
            
            elements[i].Init(this, datas[i]);
        }

        inited = true;
    }

    public void AddSpawnedElement(ElementInEditMode element)
    {
        if (spawnedElements.ContainsKey(element.data) == false)
        {
            spawnedElements.Add(element.data, new List<ElementInEditMode>());
        }
        
        spawnedElements[element.data].Add(element);
    }

    public void RemoveSpawnedElement(ElementInEditMode element)
    {
        if (spawnedElements.ContainsKey(element.data) == false)
        {
            spawnedElements.Add(element.data, new List<ElementInEditMode>());
        }

        spawnedElements[element.data].Remove(element);
    }

    public List<ElementInEditMode> GetListOfSpawnedElements(ElementInEditMode elementType)
    {
        if (spawnedElements.ContainsKey(elementType.data) == false)
        {
            spawnedElements.Add(elementType.data, new List<ElementInEditMode>());
        }

        return spawnedElements[elementType.data];
    }

    public void FillInLevelData()
    {
        LevelManager.Instance.levelSo.AddElementsToDataList(spawnedElements);
    }
}
