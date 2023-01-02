using System;
using UnityEngine;

namespace _Configs.ScriptableObjectsDeclarations
{
	[Serializable]
	[CreateAssetMenu(fileName = "ElementData", menuName = "KHPI/Core/ElementData")]
	public class ElementData : ScriptableObject
	{
		public Sprite activeStateSprite, inactiveStateSprite;

		private int nameHash = -1;

		public int NameHash
		{
			get
			{
				if (nameHash == -1)
				{
					nameHash = name.GetHashCode();
				}

				return nameHash;
			}
		}
	}
}