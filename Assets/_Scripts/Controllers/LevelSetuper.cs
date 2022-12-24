using System.Collections;
using System.Collections.Generic;
using _Scripts.Patterns.SharedData;
using UnityEngine;

public class LevelSetuper : SharedDataUserBehaviour
{
	protected override void OnInit()
	{
		base.OnInit();
		
		LevelTextureHolder.Instance.SetTexture(sharedData.LevelSo.DeserializeSavedImage());

		foreach (LevelSO.LevelComponentData levelComponentData in sharedData.LevelSo.componentsData)
		{
			Debug.LogWarning(levelComponentData.componentName);
		}
	}
}
