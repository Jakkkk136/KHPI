using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public sealed class CameraFOVSetter : MonoBehaviour
{


	private Tween fovTween;
	private CinemachineVirtualCamera m_camera;

	private bool cameraAjustedInFirstUpdate;
	
	[SerializeField]
	private float m_fieldOfView = 60f;
	

	private void Update()
	{
		if (!cameraAjustedInFirstUpdate)
		{
			RefreshCamera();
			cameraAjustedInFirstUpdate = true;
		}
	}
	
	
	[Button]
	public void RefreshCamera()
	{
		if( !m_camera )
			m_camera = GetComponent<CinemachineVirtualCamera>();

		AdjustCamera( m_camera.m_Lens.Aspect );
	}

	private void AdjustCamera( float aspect )
	{
		// Credit: https://forum.unity.com/threads/how-to-calculate-horizontal-field-of-view.16114/#post-2961964
		float _1OverAspect = 1f / aspect;
		m_camera.m_Lens.FieldOfView = /*2f **/ Mathf.Atan( Mathf.Tan( m_fieldOfView * Mathf.Deg2Rad * 0.5f ) * _1OverAspect ) * Mathf.Rad2Deg;
	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		RefreshCamera();
	}
	
	private void OnDisable()
	{
		UnityEditor.EditorApplication.update -= Update;
	}
	
#endif
}
