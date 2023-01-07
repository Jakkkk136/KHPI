using _Configs.ScriptableObjectsDeclarations;
using _Scripts.Core.Elements;
using _Scripts.Core.Elements.SelectedMenu;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementInCreatorWindow : ElementBase, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [PropertyOrder(2)] public CreatorWindow creatorWindow;

    protected ElementInEditMode dragTarget;

    public virtual void OnDrag(PointerEventData eventData)
    {
        DragProcess(eventData);
        SelectedMenu.Instance.HideMenu();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        SetDragTarget(CreateElementForEditMode());
        SelectedMenu.Instance.HideMenu();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        SelectedMenu.Instance.ActivateNearElement(dragTarget);
    }
    
    public virtual void Init(CreatorWindow creatorWindow, ElementData data)
    {
        this.creatorWindow = creatorWindow;
        this.elementData = data;
        activeStateSpriteHolder.sprite = data.activeStateSprite;
    }


    protected ElementInEditMode CreateElementForEditMode()
    {
        ElementInEditMode spawnedElementInEditMode =
            Instantiate(creatorWindow.ElementInEditModePrefab, transform.position, Quaternion.identity, creatorWindow.SpawnedElementsParent);
        spawnedElementInEditMode.Init(creatorWindow, this.elementData);

        return spawnedElementInEditMode;
    }
    
    protected void DragProcess(PointerEventData data)
    {
        dragTarget.transform.position = data.position;
    }

    protected virtual void SetDragTarget(ElementInEditMode target)
    {
        dragTarget = target;
    }


}
