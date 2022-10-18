using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Configs.ScriptableObjectsDeclarations.Configs;
using _Scripts.Controllers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public sealed class NewCharacterProgressUI : MonoBehaviour
{
        [SerializeField] private CanvasConfig config;
        [SerializeField] private GameObject progressContentGameObject;
        [SerializeField] private Image nextCharacterImage;
        [SerializeField] private Image progressFillImage;
        [SerializeField] private Text progressText;

        private void OnEnable()
        {
            StartCoroutine(ShowingProgressRoutine());
        }

        private IEnumerator ShowingProgressRoutine()
        {
            /*progressContentGameObject.SetActive(false);

            yield return new WaitForSeconds(config.DelayPlayerProgress);
            
            progressContentGameObject.SetActive(true);

            CharacterAndLevelToOpen currentCharacter = null;
            CharacterAndLevelToOpen nextCharacter = null;

            List<CharacterAndLevelToOpen> characters = CharactersSetSO.Instance.CharacterAndLevelToOpen.ToList();
            
            int levelForPlayerBeforeProgressAdded = SaveManager.LevelForPlayer - 1;
            
            for (int i = 0; i < characters.Count; i++)
            {
                if (levelForPlayerBeforeProgressAdded >= characters[i].LevelToOpen)
                    currentCharacter = characters[i];
            }

            if (currentCharacter == characters[^1])
            {
                progressContentGameObject.SetActive(false);
                yield break;
            }

            nextCharacter = characters[characters.IndexOf(currentCharacter) + 1];

            nextCharacterImage.sprite = nextCharacter.Character.CharacterProgressSprite;
            progressFillImage.sprite = nextCharacter.Character.CharacterProgressSprite;
            
            int difference = nextCharacter.LevelToOpen - currentCharacter.LevelToOpen;
            
            float tFromImage = 1f - Mathf.Max(0f, (levelForPlayerBeforeProgressAdded - currentCharacter.LevelToOpen) / (float)difference);
            float tToImage = 1f - (levelForPlayerBeforeProgressAdded + 1 - currentCharacter.LevelToOpen) / (float)difference;
            
            float tFromText = Mathf.Max(0f, (levelForPlayerBeforeProgressAdded - currentCharacter.LevelToOpen) / (float)difference);
            float tToText = (levelForPlayerBeforeProgressAdded + 1 - currentCharacter.LevelToOpen) / (float)difference;

            progressFillImage.fillAmount = tFromImage;
            progressFillImage.DOFillAmount(tToImage, 1f);
            
            progressText.text = Mathf.RoundToInt(tFromText * 100f) + "%";
            float textTween = tFromText * 100f;

            DOTween.To(() => textTween, x => textTween = x, tToText * 100f, 1f);
        
            float timer = 1.1f;

            while (timer > 0)
            {
                progressText.text = Mathf.RoundToInt(textTween) + "%";
                timer -= Time.deltaTime;
                
                yield return new WaitForEndOfFrame();
            }

            progressText.text = Mathf.RoundToInt(tToText * 100f) + "%";
            progressFillImage.fillAmount = tToImage;*/

            yield return null;
        }
    }