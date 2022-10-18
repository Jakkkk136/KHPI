using UnityEngine;

namespace _Configs.ScriptableObjectsDeclarations.Configs
{
	[CreateAssetMenu(fileName = "CameraScakeConfigSO", menuName = "Battleships/Camera shake Config", order = 0)]
	public class CameraShakeConfigSO : ScriptableObject
	{
		[SerializeField] private float impulseDuration;
		[SerializeField] private Vector3 impulseCameraLocalVector;
		[SerializeField] private AnimationCurve impulseShape;

		public float ImpulseDuration => impulseDuration;
		public Vector3 ImpulseCameraLocalVector => impulseCameraLocalVector;
		public AnimationCurve ImpulseShape => impulseShape;
	}
}