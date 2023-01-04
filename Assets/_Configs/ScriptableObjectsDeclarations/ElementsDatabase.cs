using System;
using System.Collections.Generic;
using _Scripts.Patterns;
using UnityEngine;

namespace _Configs.ScriptableObjectsDeclarations
{
	[Serializable]
	[CreateAssetMenu(fileName = "ElementsDatabase", menuName = "KHPI/Core/ElementsDatabase")]
	public class ElementsDatabase : SingletonScriptableObject<ElementsDatabase>
	{
		public List<ElementData> elements;
		public ElementBase baseElementPrefab;

		public ElementBase GetNewElement(string elementName)
		{
			ElementBase newElement = Instantiate(baseElementPrefab).Init(elements.Find(e => e.name == elementName));
			return newElement;
		}
	}
}