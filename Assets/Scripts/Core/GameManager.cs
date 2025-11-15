using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
public enum SceneType
{
    Menu,
    Map,
    Stage,
    GameOver,
    Result,
    Building,
}

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        [Button]
        public void SwitchScene(string s)
        {
            SceneManager.LoadScene(s);
        }
    }
}
