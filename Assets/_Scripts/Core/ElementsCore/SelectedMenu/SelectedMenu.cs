using System;
using System.Collections.Generic;
using _Scripts.Patterns;
using Ookii.Dialogs;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
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
		[SerializeField] private Button switchStateButton;
		[Space] 
		[SerializeField] private TMP_InputField correctPressOrderInputField;
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
			
			ActivateMenu();
		}

		private void DeleteElement()
		{
			CloseScaleTool();
			currentElement.DeleteElement();
		}

		private void DuplicateElement()
		{
			CloseScaleTool();
			currentElement.DuplicateElement();
		}

		private void RotateElement()
		{
			CloseScaleTool();

			currentElement.RotateElement();
		}

		private void SwitchElementState()
		{
			CloseScaleTool();

			currentElement.ElementState = !currentElement.ElementState;
		}

		private void OnChangeInputField(String newInput)
		{
			currentElement.CorrectPressOrder = Int32.Parse(newInput);
		}

		private void OpenScaleTool()
		{
			scaleButton.onClick.RemoveAllListeners();
			scaleButton.onClick.AddListener(CloseScaleTool);
			
			scaleToolPanel.SetActive(true);

			Vector2 savedScaleParams = currentElement.elementData.scale;

			horizontalScaleSlider.value = Mathf.InverseLerp(0.2f, 2.5f, savedScaleParams.x);
			verticalScaleSlider.value = Mathf.InverseLerp(0.2f, 2.5f, savedScaleParams.y);

			currentEditedElements = currentElement.creatorWindow.GetListOfSpawnedElements(currentElement);
			
			horizontalScaleSlider.onValueChanged.RemoveAllListeners();
			verticalScaleSlider.onValueChanged.RemoveAllListeners();
			
			horizontalScaleSlider.onValueChanged.AddListener(ChangeHorizontalScale);
			verticalScaleSlider.onValueChanged.AddListener(ChangeVerticalScale);
		}

		private void CloseScaleTool()
		{
			scaleButton.onClick.RemoveAllListeners();
			scaleButton.onClick.AddListener(OpenScaleTool);
			
			scaleToolPanel.SetActive(false);
		}

		
		private void ChangeHorizontalScale(float normalizedValue)
		{
			Vector2 newScale = currentElement.elementData.scale;
			newScale.x = Mathf.Lerp(0.2f, 2.5f, normalizedValue);

			foreach (ElementInEditMode element in currentEditedElements)
			{
				element.SetNewLocalScale(newScale);
			}

			currentElement.elementData.SetXScaleParam(newScale.x);
		}

		private void ChangeVerticalScale(float normalizedValue)
		{
			Vector2 newScale = currentElement.elementData.scale;
			newScale.y = Mathf.Lerp(0.2f, 2.5f, normalizedValue);

			foreach (ElementInEditMode element in currentEditedElements)
			{
				element.SetNewLocalScale(newScale);
			}

			currentElement.elementData.SetYScaleParam(newScale.y);
		}

		private void ActivateMenu()
		{
			deleteButton.gameObject.SetActive(true);
			duplicateButton.gameObject.SetActive(true);
			scaleButton.gameObject.SetActive(true);
			rotateButton.gameObject.SetActive(true);
			switchStateButton.gameObject.SetActive(true);
			
			deleteButton.onClick.RemoveAllListeners();
			duplicateButton.onClick.RemoveAllListeners();
			scaleButton.onClick.RemoveAllListeners();
			rotateButton.onClick.RemoveAllListeners();
			switchStateButton.onClick.RemoveAllListeners();
			
			correctPressOrderInputField.gameObject.SetActive(true);
			correctPressOrderInputField.onValueChanged.RemoveAllListeners();
			correctPressOrderInputField.onValueChanged.AddListener(OnChangeInputField);
			
			deleteButton.onClick.AddListener(DeleteElement);
			duplicateButton.onClick.AddListener(DuplicateElement);
			scaleButton.onClick.AddListener(OpenScaleTool);
			rotateButton.onClick.AddListener(RotateElement);
			switchStateButton.onClick.AddListener(SwitchElementState);
		}

		public void HideMenu()
		{
			deleteButton.gameObject.SetActive(false);
			duplicateButton.gameObject.SetActive(false);
			scaleButton.gameObject.SetActive(false);
			rotateButton.gameObject.SetActive(false);
			switchStateButton.gameObject.SetActive(false);

			correctPressOrderInputField.gameObject.SetActive(false);

			scaleToolPanel.SetActive(false);
		}
	}
}