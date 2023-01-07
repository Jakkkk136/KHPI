using System;
using UnityEngine;

namespace _Configs.ScriptableObjectsDeclarations
{
	[Serializable]
	[CreateAssetMenu(fileName = "ElementData", menuName = "KHPI/Core/ElementData")]
	public class ElementData : ScriptableObject
	{
		public Sprite activeStateSprite, inactiveStateSprite, 
			correctActiveSprite, correctInactiveSprite, 
			incorrectActiveSprite, incorrectInactiveSprite;
		
		public Vector2 scale = Vector2.one;

		public void SetXScaleParam(float xScaleParam)
		{
			scale.x = xScaleParam;
		}

		public void SetYScaleParam(float yScaleParam)
		{
			scale.y = yScaleParam;
		}
	}
}