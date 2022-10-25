using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Core.ClickableElements;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class ClickableIndexesSetter : OdinEditorWindow
{
	[MenuItem("KHPI/Clickable indexes setter")]
	private static void OpenWindow()
	{
		GetWindow<ClickableIndexesSetter>().Show();
	}
	
	[Button(ButtonSizes.Large)]
	public void SetIndexes()
	{
		List<Disconnector> allDisconnectors = FindObjectsOfType<Disconnector>().ToList();
		var disconnectorsGroupedByY = allDisconnectors.GroupBy(d => d.transform.position.y).ToList();
		var groupedDisconnectorsOrderedByY =
			disconnectorsGroupedByY.OrderByDescending(d => d.First().transform.position.y);

		int indexD = 1;
		
		foreach (IGrouping<float,Disconnector> disconnectors in groupedDisconnectorsOrderedByY)
		{
			var b = disconnectors.OrderBy(d => d.transform.position.x);
			
			foreach (Disconnector disconnector in b)
			{
				disconnector.SetText("P" + indexD++);
			}
		}

		List<Switcher> allSwitches = FindObjectsOfType<Switcher>().ToList();
		var switchesGroupedByY = allSwitches.GroupBy(d => d.transform.position.y).ToList();
		var groupedSwitchesOrderedByY =
			switchesGroupedByY.OrderByDescending(d => d.First().transform.position.y);

		indexD = 1;

		foreach (IGrouping<float, Switcher> switches in groupedSwitchesOrderedByY)
		{
			var b = switches.OrderBy(d => d.transform.position.x);

			foreach (Switcher switcher in b)
			{
				switcher.SetText("B" + indexD++);
			}
		}
	}
}
