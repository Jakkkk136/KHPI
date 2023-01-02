using _Configs.ScriptableObjectsDeclarations;
using _Scripts.Controllers;
using _Scripts.Core.Elements;
using _Scripts.Core.Elements.SelectedMenu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementInCreatorWindow : MonoBehaviour
{
    [SerializeField] private Image spriteHolder;

    [Header("Sets Dynamically")]
    public ElementData data;
    public CreatorWindow creatorWindow;

    protected Transform dragTarget;
    
    public virtual void Init(CreatorWindow creatorWindow, ElementData data)
    {
        this.creatorWindow = creatorWindow;
        this.data = data;
        spriteHolder.sprite = data.activeStateSprite;
    }

    public virtual void PointerDownHandler(BaseEventData data)
    {
        SetDragTarget(CreateElementForEditMode().transform);
        SelectedMenu.Instance.HideButtons();
    }
    
    public virtual void DragHandler(BaseEventData data)
    {
        DragProcess(data);
        SelectedMenu.Instance.HideButtons();
    }

    protected ElementInEditMode CreateElementForEditMode()
    {
        ElementInEditMode spawnedElementInEditMode =
            Instantiate(creatorWindow.ElementInEditModePrefab, transform.position, Quaternion.identity, creatorWindow.SpawnedElementsParent);
        spawnedElementInEditMode.Init(creatorWindow, this.data);

        return spawnedElementInEditMode;
    }
    
    protected void DragProcess(BaseEventData data)
    {
        PointerEventData pointerEventData = (PointerEventData)data;
        dragTarget.position = pointerEventData.position;
    }

    protected virtual void SetDragTarget(Transform target)
    {
        dragTarget = target;
    }
    
}
