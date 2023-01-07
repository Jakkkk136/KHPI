using System;
using System.Collections.Generic;
using _Configs.ScriptableObjectsDeclarations;
using _Scripts.Core.Elements;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewLevelSO", menuName = "KHPI/Level")]
public class LevelSO : ScriptableObject
{
	[Serializable]
	public class LevelComponentData
	{
		public string elementName;
		public Vector2 elementScreenPos;
		public bool elementState;
		public Quaternion elementRotation;
		public Vector3 elementScale;
		public int elementCorrectPressOrder;

		public LevelComponentData(string elementName, Vector2 elementScreenPos, bool elementState,
			Quaternion elementRotation, Vector3 elementScale, int elementCorrectPressOrder)
		{
			this.elementName = elementName;
			this.elementScreenPos = elementScreenPos;
			this.elementState = elementState;
			this.elementRotation = elementRotation;
			this.elementScale = elementScale;
			this.elementCorrectPressOrder = elementCorrectPressOrder;
		}
	}
	
	public byte[] serializedLevelTexture;
	public List<LevelComponentData> elementsData = new List<LevelComponentData>() { };

	public void AddElementsToDataList(Dictionary<ElementData, List<ElementInEditMode>> spawnedElementsInCreatorMode)
	{
		elementsData.Clear();

		foreach (List<ElementInEditMode> spawnedElementsList in spawnedElementsInCreatorMode.Values)
		{
			foreach (ElementInEditMode element in spawnedElementsList)
			{
				var elementTransform = element.transform;
				elementsData.Add(new LevelComponentData(
					element.elementData.name, 
					elementTransform.position, 
					element.ElementState, 
					elementTransform.rotation, 
					elementTransform.localScale, 
					element.CorrectPressOrder));
			}
		}
	}
	
	public void SetLevelTexture(Texture2D texture)
	{
		serializedLevelTexture = texture.EncodeToPNG();
		LevelTextureHolder.Instance.SetTexture(texture);
	}

	public Texture2D DeserializeSavedImage()
	{
		if (serializedLevelTexture.Length == 0) return null;
		Texture2D deserializedImage = new Texture2D(1, 1);
		deserializedImage.LoadImage(serializedLevelTexture);
		return deserializedImage;
	}
}
