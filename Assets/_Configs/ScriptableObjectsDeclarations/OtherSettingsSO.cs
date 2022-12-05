using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Extensions;
using _Scripts.Patterns;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "OtherSettingsSO", menuName = "KHPI/Game Settings/OtherSettingsSO")]
public class OtherSettingsSO : SingletonScriptableObject<OtherSettingsSO>
{
    [SerializeField] private float hitEnemyBodyForceMult = 5f;

    [SerializeField] private float delayLoadingNextLevel;
    [SerializeField] private float tutorialDuration = 5f;
    [SerializeField] private eWindowType menuWindowType;

    [Title("Player controls")] 
    [SerializeField] private float verticalSensitivity;
    [SerializeField] private float horizontalSensitivity = 0.035f;

    public float VerticalSensitivity => verticalSensitivity;
    public float HorizontalSensitivity => horizontalSensitivity;
    public float HitEnemyBodyForceMult => hitEnemyBodyForceMult;
    public float DelayLoadingNextLevel => delayLoadingNextLevel;
    public float TutorialDuration => tutorialDuration;
    public eWindowType MenuWindowType => menuWindowType;
}
