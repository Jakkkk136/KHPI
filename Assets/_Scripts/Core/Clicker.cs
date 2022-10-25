using System;
using UnityEngine;

namespace _Scripts.Core
{
	public class Clicker : MonoBehaviour
	{
		private Camera mainCam;

		private void Awake()
		{
			mainCam = Camera.main;
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 25f))
				{
					hit.collider.GetComponent<ClickableElement>()?.Press();
				}
			}
		}
	}
}