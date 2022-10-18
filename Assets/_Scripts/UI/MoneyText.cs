using System.Collections;
using _Scripts.Controllers;
using _Scripts.Patterns.Events;
using Pawsome.SideGameplayTool;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public sealed class MoneyText : MonoBehaviour
    {
        [SerializeField] private Text moneyText;
        [SerializeField] private GameObject money_fx;

        private int lastPlayerMoneyAmount;

        private void OnEnable()
        {
            this.Subscribe<bool>(EventID.PLAYER_MONEY_CHANGED, UpdateMoneyText);
            ChangeMoneyText(SaveManager.PlayerMoney);
        }

        private void OnDisable()
        {
            this.Unsubscribe<bool>(EventID.PLAYER_MONEY_CHANGED, UpdateMoneyText);
        }
        
        private void ChangeMoneyText(int amount)
        {
            lastPlayerMoneyAmount = amount;
            moneyText.text = MoneyDisplayFormat.Format(amount);
        }

        private void UpdateMoneyText(bool withAnim)
        {
            if (withAnim)
            {
                UpdateMoneyTextWithAnim();
            }
            else
            {
                int amount = SaveManager.PlayerMoney;
                ChangeMoneyText(amount);
            }
        }

        public void UpdateMoneyTextWithAnim()
        {
            StartCoroutine(AddMoneyWithAnim());
        }
        
        private IEnumerator AddMoneyWithAnim()
        {
            int amount = SaveManager.PlayerMoney - lastPlayerMoneyAmount;
            
            if(amount == 0) yield break;
            
            if (amount > 0 && money_fx != null)
            {
                money_fx.SetActive(true);
            }
            
            int playerMoney = SaveManager.PlayerMoney;

            float startTime = Time.time;
            float duration = 1.5f;
            float timeStep = 0.2f;
        
            int moneyStep = Mathf.FloorToInt(amount / (duration / timeStep));
            
            while (startTime + duration >= Time.time)
            {
                amount -= moneyStep;
                
                if (amount > 0)
                {
                    if (playerMoney - amount + moneyStep > playerMoney)
                    {
                        ChangeMoneyText(playerMoney);
                        break;
                    }
                }
                else
                {
                    if (playerMoney - amount + moneyStep < playerMoney)
                    {
                        ChangeMoneyText(playerMoney);
                        break;
                    }
                }
                
                ChangeMoneyText(playerMoney - amount + moneyStep);

                yield return new WaitForSeconds(timeStep);
            }

            ChangeMoneyText(playerMoney);
        }
    }
}
