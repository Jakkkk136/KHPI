using System;
using System.Collections.Generic;
using _Configs.ScriptableObjectsDeclarations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Core.Elements
{
	public class ElementInGame : ElementBase, IPointerDownHandler
	{
		private static List<ElementInGame> clickedElements = new();
		
		private bool pressed = false;
		private bool pressedInCorrectOrder = false;

		private int currentIndex;

		private void OnDisable()
		{
			clickedElements.Clear();
		}
		
		public void OnPointerDown(PointerEventData eventData)
		{
			_elementState = !_elementState;
			pressed = !pressed;

			if (pressed)
			{
				clickedElements.Add(this);
				var clickedElementsAmount = clickedElements.Count;
				currentIndex = clickedElementsAmount;
				UpdateTextIndex();
				TestIndexIsCorrect();
				SetCorrectSprite();
			}
			else
			{
				pressedInCorrectOrder = false;
				SetDefaultSprite();
				clickedElements.Remove(this);
				SetClearTextIndex();
				UpdateElementsWithHigherIndexes(currentIndex);
			}
		}

		public ElementInGame Init(ElementData data)
		{
			elementData = data;
			return this;
		}

		public void InitElementOnGameScene(Transform parent, Vector3 screenPos, Vector3 scale, Quaternion rotation,
			bool currentState, int correctPressOrder)
		{
			transform.parent = parent;
			transform.SetPositionAndRotation(screenPos, rotation);
			transform.localScale = scale;
			
			orderPressText.transform.rotation = Quaternion.identity;
			
			ElementState = currentState;
			_correctPressOrder = correctPressOrder;

			SetClearTextIndex();
			SetDefaultScaleOfText();
		}

		
		private void UpdateElementsWithHigherIndexes(int removedElementIndex)
		{
			foreach (var element in clickedElements)
				if (element.currentIndex >= removedElementIndex)
					element.DecreaseIndexByOne();

			foreach (var element in clickedElements)
				if (element.currentIndex >= removedElementIndex)
					element.TestIndexIsCorrect();

			foreach (var element in clickedElements)
				if (element.currentIndex >= removedElementIndex)
					element.SetCorrectSprite();
		}
		
		private void DecreaseIndexByOne()
		{
			currentIndex--;
			UpdateTextIndex();
		}

		private void TestIndexIsCorrect()
		{
			pressedInCorrectOrder = _correctPressOrder == currentIndex;
		}

		private void SetDefaultSprite()
		{
			activeStateSpriteHolder.sprite =
				_elementState ? elementData.activeStateSprite : elementData.inactiveStateSprite;
		}

		private void SetCorrectSprite()
		{
			if (_elementState == true)
			{
				activeStateSpriteHolder.sprite = clickedElements.TrueForAll(e => e.pressedInCorrectOrder)
					? elementData.correctActiveSprite
					: elementData.incorrectActiveSprite;

			}
			else
			{
				activeStateSpriteHolder.sprite = clickedElements.TrueForAll(e => e.pressedInCorrectOrder)
					? elementData.correctInactiveSprite
					: elementData.incorrectInactiveSprite;
			}
		}

		private void UpdateTextIndex()
		{
			orderPressText.SetText(currentIndex.ToString());
		}

		private void SetClearTextIndex()
		{
			orderPressText.SetText(string.Empty);
		}
	}
}