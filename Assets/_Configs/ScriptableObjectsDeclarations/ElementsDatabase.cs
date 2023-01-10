using System;
using System.Collections.Generic;
using _Scripts.Core.Elements;
using _Scripts.Patterns;
using UnityEngine;

namespace _Configs.ScriptableObjectsDeclarations
{
	[Serializable]
	[CreateAssetMenu(fileName = "ElementsDatabase", menuName = "KHPI/Core/ElementsDatabase")]
	public class ElementsDatabase : SingletonScriptableObject<ElementsDatabase>
	{
		public List<ElementData> elements;
		public ElementInGame baseElementPrefab;

		public ElementInGame GetNewElement(string elementName)
		{
			ElementInGame newElement = Instantiate(baseElementPrefab).Init(elements.Find(e => e.name == elementName));
			return newElement;
		}

		public ElementData GetElementData(string elementName)
		{
			return elements.Find(e => e.name == elementName);
		}
	}
}