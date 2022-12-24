using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
[CreateAssetMenu(fileName = "NewLevelSO", menuName = "KHPI/Level")]
public class LevelSO : ScriptableObject
{
	[Serializable]
	public class LevelComponentData
	{
		public string componentName;
		public Vector2 componentScreenPos;
		public bool componentState;

		public LevelComponentData(string componentName, Vector2 componentScreenPos, bool componentState)
		{
			this.componentName = componentName;
			this.componentScreenPos = componentScreenPos;
			this.componentState = componentState;
		}
	}
	
	public byte[] serializedLevelTexture;
	public List<LevelComponentData> componentsData = new List<LevelComponentData>()
	{
		new LevelComponentData("switcher", new Vector2(300f, 500f), true),
		new LevelComponentData("other", new Vector2(200f, 150f), false),
		new LevelComponentData("switcher", new Vector2(100f, 50f), true)
	};

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
