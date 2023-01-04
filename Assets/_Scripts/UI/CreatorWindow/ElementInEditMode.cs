using System;
using _Configs.ScriptableObjectsDeclarations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Core.Elements
{
	[Serializable]
	public class ElementInEditMode : ElementInCreatorWindow
	{
		[SerializeField][PropertyOrder(-1)] private TextMeshProUGUI orderPressText;
		
		private bool _elementState;
		private int _correctPressOrder;
		
		public bool ElementState
		{
			get => _elementState;
			set
			{
				_elementState = value;
				spriteHolder.sprite = _elementState ? data.activeStateSprite : data.inactiveStateSprite;
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
		
		public override void Init(CreatorWindow creatorWindow, ElementData data)
		{
			base.Init(creatorWindow, data);
			
			transform.localScale = data.scale;
			ElementState = true;
			CorrectPressOrder = 0;
			
			creatorWindow.AddSpawnedElement(this);
		}

		public override void PointerDownHandler(BaseEventData data)
		{
			SetDragTarget(transform);
		}
		
		public void PointedUpHandler(BaseEventData data)
		{
			SelectedMenu.SelectedMenu.Instance.ActivateNearElement(this);
		}
		
		public override void DragHandler(BaseEventData data)
		{
			DragProcess(data);
			SelectedMenu.SelectedMenu.Instance.HideMenu();
		}

		protected override void SetDragTarget(Transform target)
		{
			dragTarget = transform;
		}

		public void DeleteElement()
		{
			creatorWindow.RemoveSpawnedElement(this);
			GameObject.DestroyImmediate(gameObject);
			SelectedMenu.SelectedMenu.Instance.HideMenu();
		}

		public void DuplicateElement()
		{
			Vector3 newElementPos = transform.position;
			newElementPos.x *= 1.15f;
			newElementPos.y *= 0.85f;
			
			ElementInEditMode newElement = Instantiate(this, newElementPos, Quaternion.identity, transform.parent);
			newElement.Init(creatorWindow, data);
			newElement.ElementState = ElementState;
			SelectedMenu.SelectedMenu.Instance.ActivateNearElement(newElement);
		}

		public void RotateElement()
		{
			transform.rotation *= Quaternion.Euler(0f, 0f, -90f);
			orderPressText.transform.rotation = Quaternion.identity;
		}
	}
}