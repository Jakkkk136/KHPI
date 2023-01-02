using _Configs.ScriptableObjectsDeclarations.Configs;
using _Scripts.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Controllers
{
    [AutoCreateSingelton]
    public sealed class Loader : Singleton<Loader>
    {
        private void Awake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            if (Application.isEditor) Application.runInBackground = true;
            Input.multiTouchEnabled = false;
            
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if(!isCreatedOnLevel) OpenMainMenu();
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void OpenMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void OpenCreatorScene()
        {
            SceneManager.LoadScene("CreatorScene");
        }
    }
}
