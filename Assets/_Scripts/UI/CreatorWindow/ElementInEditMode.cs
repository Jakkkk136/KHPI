using System;
using _Configs.ScriptableObjectsDeclarations;
using _Scripts.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Core.Elements
{
	[Serializable]
	public class ElementInEditMode : ElementInCreatorWindow
	{
		public override void Init(CreatorWindow creatorWindow, ElementData data)
		{
			base.Init(creatorWindow, data);

			Vector2 scale = LevelManager.Instance.levelSo.GetScaleParams(data.NameHash);
			transform.localScale = (Vector2)scale;
			
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
			SelectedMenu.SelectedMenu.Instance.HideButtons();
		}

		protected override void SetDragTarget(Transform target)
		{
			dragTarget = transform;
		}

		public void DeleteElement()
		{
			creatorWindow.RemoveSpawnedElement(this);
			GameObject.DestroyImmediate(gameObject);
			SelectedMenu.SelectedMenu.Instance.HideButtons();
		}

		public void DuplicateElement()
		{
			Vector3 newElementPos = transform.position;
			newElementPos.x *= 1.15f;
			newElementPos.y *= 0.85f;
			
			ElementInEditMode newElement = Instantiate(this, newElementPos, Quaternion.identity, transform.parent);
			newElement.Init(creatorWindow, data);
			SelectedMenu.SelectedMenu.Instance.ActivateNearElement(newElement);
		}

		public void RotateElement()
		{
			transform.rotation *= Quaternion.Euler(0f, 0f, -90f);
		}
	}
}