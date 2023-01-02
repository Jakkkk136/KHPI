using System.Collections.Generic;
using _Configs.ScriptableObjectsDeclarations;
using _Scripts.Core.Elements;
using UnityEngine;

public class CreatorWindow : MonoBehaviour
{
    [SerializeField] private ElementInCreatorWindow elementInCreatorWindowPrefab;
    [SerializeField] private ElementInEditMode elementInEditModePrefab;
    [SerializeField] private Transform spawnedElementsParent;
    
    private Dictionary<int, List<ElementInEditMode>> spawnedElements = new Dictionary<int, List<ElementInEditMode>>();

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
        if (spawnedElements.ContainsKey(element.data.NameHash) == false)
        {
            spawnedElements.Add(element.data.NameHash, new List<ElementInEditMode>());
        }
        
        spawnedElements[element.data.NameHash].Add(element);
    }

    public void RemoveSpawnedElement(ElementInEditMode element)
    {
        if (spawnedElements.ContainsKey(element.data.NameHash) == false)
        {
            spawnedElements.Add(element.data.NameHash, new List<ElementInEditMode>());
        }

        spawnedElements[element.data.NameHash].Remove(element);
    }

    public List<ElementInEditMode> GetListOfSpawnedElements(ElementInEditMode elementType)
    {
        if (spawnedElements.ContainsKey(elementType.data.NameHash) == false)
        {
            spawnedElements.Add(elementType.data.NameHash, new List<ElementInEditMode>());
        }

        return spawnedElements[elementType.data.NameHash];
    }
}
