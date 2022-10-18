using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
	[SerializeField] private bool rotateBackwards = true;
	
	private void OnEnable()
	{
		transform.LookAt(Camera.main.transform);
		if (rotateBackwards) transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
	}
}
