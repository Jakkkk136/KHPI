using System;
using _Configs.ScriptableObjectsDeclarations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Core.Elements
{
	[Serializable]
	public class ElementInEditMode : ElementInCreatorWindow
	{
		public int CorrectPressOrder
		{
			get => _correctPressOrder;
			set
			{
				_correctPressOrder = value;
				orderPressText.text = _correctPressOrder > 0 ? _correctPressOrder.ToString() : String.Empty;
			}
		}
		
		public override void OnPointerDown(PointerEventData eventData)
		{
			SetDragTarget(this);
			SelectedMenu.SelectedMenu.Instance.HideMenu();
		}
		
		public override void Init(CreatorWindow creatorWindow, ElementData data)
		{
			base.Init(creatorWindow, data);
			
			transform.localScale = data.scale;

			SetDefaultScaleOfText();
			
			ElementState = true;
			CorrectPressOrder = 0;
			
			creatorWindow.AddSpawnedElement(this);
		}

		protected override void SetDragTarget(ElementInEditMode target)
		{
			dragTarget = this;
		}

		public void DeleteElement()
		{
			creatorWindow.RemoveSpawnedElement(this);
			GameObject.DestroyImmediate(gameObject);
			SelectedMenu.SelectedMenu.Instance.HideMenu();
		}

		public void SetNewLocalScale(Vector3 localScale)
		{
			transform.localScale = localScale;
			
			SetDefaultScaleOfText();
		}

		public void DuplicateElement()
		{
			Vector3 newElementPos = transform.position;
			newElementPos.x *= 1.05f;
			newElementPos.y *= 0.95f;
			
			ElementInEditMode newElement = Instantiate(this, newElementPos, Quaternion.identity, transform.parent);
			newElement.Init(creatorWindow, elementData);
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