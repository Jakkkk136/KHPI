using _Configs.ScriptableObjectsDeclarations;
using _Scripts.Core.Elements;
using _Scripts.Patterns.SharedData;
using UnityEngine;

public class LevelSetuper : SharedDataUserBehaviour
{
	[SerializeField] private Transform parentForSpawnedElements;
	
	protected override void OnInit()
	{
		base.OnInit();
		
		LevelTextureHolder.Instance.SetTexture(sharedData.LevelSo.DeserializeSavedImage());

		foreach (LevelSO.LevelComponentData levelComponentData in sharedData.LevelSo.elementsData)
		{
			ElementInGame newElement = ElementsDatabase.Instance.GetNewElement(levelComponentData.elementName);
			
			newElement.InitElementOnGameScene(
				parentForSpawnedElements, 
				levelComponentData.elementScreenPos,
				levelComponentData.elementScale, 
				levelComponentData.elementRotation, 
				levelComponentData.elementState, 
				levelComponentData.elementCorrectPressOrder);
		}
	}
}
