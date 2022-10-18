using System.Text;
using _Scripts.Patterns;
using UnityEngine;
using UnityEngine.UI;

public sealed class PooledHitFx : PooledFX
{
    [SerializeField] private Text hitDamageText;

    public void SetHitDamageText(int damage)
    {
        hitDamageText.text = new StringBuilder().Append("-").Append(damage).ToString();
    }
}
