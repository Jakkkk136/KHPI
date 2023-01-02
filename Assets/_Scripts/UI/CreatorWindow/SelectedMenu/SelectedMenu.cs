using System;
using System.Collections.Generic;
using _Scripts.Controllers;
using _Scripts.Patterns;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Core.Elements.SelectedMenu
{
	public class SelectedMenu : Singleton<SelectedMenu>
	{
		[SerializeField] private Button deleteButton;
		[SerializeField] private Button duplicateButton;
		[SerializeField] private Button scaleButton;
		[SerializeField] private Button rotateButton;
		[Space] 
		[SerializeField] private GameObject scaleToolPanel;
		[SerializeField] private Slider horizontalScaleSlider;
		[SerializeField] private Slider verticalScaleSlider;

		private Camera cam;

		private ElementInEditMode currentElement;

		private List<ElementInEditMode> currentEditedElements;

		private void Awake()
		{
			cam = Camera.main;
		}

		public void ActivateNearElement(ElementInEditMode element)
		{
			currentElement = element;
			
			Vector3 elementPos = element.transform.position;
			Vector3 thisPos = elementPos;
			
			bool activateFromLeft = cam.ScreenToViewportPoint(elementPos).x > 0.5f;
			
			thisPos.x = activateFromLeft ? elementPos.x - 75f : elementPos.x + 75f;

			transform.position = thisPos;
			
			ActivateButtons();
		}

		private void DeleteElement()
		{
			currentElement.DeleteElement();
		}

		private void DuplicateElement()
		{
			currentElement.DuplicateElement();
		}

		private void OpenScaleTool()
		{
			HideButtons();
			
			scaleToolPanel.SetActive(true);

			Vector2 savedScaleParams = LevelManager.Instance.levelSo.GetScaleParams(currentElement.data.NameHash);

			horizontalScaleSlider.value = Mathf.InverseLerp(0.2f, 2.5f, savedScaleParams.x);
			verticalScaleSlider.value = Mathf.InverseLerp(0.2f, 2.5f, savedScaleParams.y);

			currentEditedElements = currentElement.creatorWindow.GetListOfSpawnedElements(currentElement);
			
			horizontalScaleSlider.onValueChanged.RemoveAllListeners();
			verticalScaleSlider.onValueChanged.RemoveAllListeners();
			
			horizontalScaleSlider.onValueChanged.AddListener(ChangeHorizontalScale);
			verticalScaleSlider.onValueChanged.AddListener(ChangeVerticalScale);
		}

		private void RotateElement()
		{
			currentElement.RotateElement();
		}

		private void ChangeHorizontalScale(float normalizedValue)
		{
			Vector2 newScale = currentElement.transform.localScale;
			newScale.x = Mathf.Lerp(0.2f, 2.5f, normalizedValue);

			foreach (ElementInEditMode element in currentEditedElements)
			{
				element.transform.localScale = newScale;
			}
			
			LevelManager.Instance.levelSo.SetXScaleParam(currentElement.data.NameHash, newScale.x);
		}

		private void ChangeVerticalScale(float normalizedValue)
		{
			Vector2 newScale = currentElement.transform.localScale;
			newScale.y = Mathf.Lerp(0.2f, 2.5f, normalizedValue);

			foreach (ElementInEditMode element in currentEditedElements)
			{
				element.transform.localScale = newScale;
			}

			LevelManager.Instance.levelSo.SetYScaleParam(currentElement.data.NameHash, newScale.y);
		}

		private void ActivateButtons()
		{
			deleteButton.gameObject.SetActive(true);
			duplicateButton.gameObject.SetActive(true);
			scaleButton.gameObject.SetActive(true);
			rotateButton.gameObject.SetActive(true);
			
			deleteButton.onClick.RemoveAllListeners();
			duplicateButton.onClick.RemoveAllListeners();
			scaleButton.onClick.RemoveAllListeners();
			rotateButton.onClick.RemoveAllListeners();
			
			deleteButton.onClick.AddListener(DeleteElement);
			duplicateButton.onClick.AddListener(DuplicateElement);
			scaleButton.onClick.AddListener(OpenScaleTool);
			rotateButton.onClick.AddListener(RotateElement);
		}

		public void HideButtons()
		{
			deleteButton.gameObject.SetActive(false);
			duplicateButton.gameObject.SetActive(false);
			scaleButton.gameObject.SetActive(false);
			rotateButton.gameObject.SetActive(false);

			scaleToolPanel.SetActive(false);
		}
	}
}