using System;
using UnityEngine;

namespace _Scripts.Core.ClickableElements
{
	public class Disconnector : ClickableElement
	{
		[SerializeField] private bool openByDefault;
		[SerializeField] private GameObject openLineObject;
		[SerializeField] private GameObject closedLineObject;

		private void Start()
		{
			openLineObject.SetActive(openByDefault);
			closedLineObject.SetActive(!openByDefault);
		}

		protected override void OnPress(bool pressed)
		{
			bool open = openLineObject.activeSelf;
			openLineObject.SetActive(!open);
			closedLineObject.SetActive(open);
		}
	}
}