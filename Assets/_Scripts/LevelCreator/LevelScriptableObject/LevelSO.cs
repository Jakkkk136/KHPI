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
	
	[Serializable]
	public class ElementOnLevelScaleParams
	{
		public int nameHash;
		public Vector2 scale;
	}
	
	public byte[] serializedLevelTexture;
	public List<LevelComponentData> componentsData = new List<LevelComponentData>()
	{
		new LevelComponentData("switcher", new Vector2(300f, 500f), true),
		new LevelComponentData("other", new Vector2(200f, 150f), false),
		new LevelComponentData("switcher", new Vector2(100f, 50f), true)
	};

	public List<ElementOnLevelScaleParams> scaleParams = new List<ElementOnLevelScaleParams>();

	public Vector2 GetScaleParams(int elementNameHash)
	{
		ElementOnLevelScaleParams scaleParam = EnsureHaveScaleParam(elementNameHash);

		return scaleParam.scale;
	}

	public void SetXScaleParam(int elementNameHash, float xScaleParam)
	{
		ElementOnLevelScaleParams scaleParam = EnsureHaveScaleParam(elementNameHash);

		scaleParam.scale = new Vector2(xScaleParam, scaleParam.scale.y);
	}

	public void SetYScaleParam(int elementNameHash, float yScaleParam)
	{
		ElementOnLevelScaleParams scaleParam = EnsureHaveScaleParam(elementNameHash);

		scaleParam.scale = new Vector2(scaleParam.scale.x, yScaleParam);
	}

	private ElementOnLevelScaleParams EnsureHaveScaleParam(int elementNameHash)
	{
		ElementOnLevelScaleParams scaleParam = this.scaleParams.Find(s => s.nameHash == elementNameHash);

		if (scaleParam == null)
		{
			scaleParam = new ElementOnLevelScaleParams() { nameHash = elementNameHash, scale = Vector2.one };
			scaleParams.Add(scaleParam);
		}

		return scaleParam;
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
