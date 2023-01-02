using System;
using System.Collections.Generic;
using _Scripts.Patterns;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Configs.ScriptableObjectsDeclarations
{
	[Serializable]
	[CreateAssetMenu(fileName = "ElementsDatabase", menuName = "KHPI/Core/ElementsDatabase")]
	public class ElementsDatabase : SingletonScriptableObject<ElementsDatabase>
	{
		public List<ElementData> elements;
		public ElementBase baseElementPrefab;
	}
}