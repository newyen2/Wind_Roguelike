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

        public int total_score = 0; // 這個是結算頁面顯示的分數

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

        private void Start()
        {
            
            AudioManager.Instance.Stop("bgm_main");
            AudioManager.Instance.Play("bgm_cover");
        }

        [Button]
        public void SwitchScene(string s)
        {
            SceneManager.LoadScene(s);
        }

        public void StartGame()
        {
            SceneManager.LoadScene("InitialReward");
            GlobalManager.Instance.AlreadyGetBuilding = new();
        }

        public void OpenArchive()
        {
            Archive.Instance.OpenArchive(GlobalManager.Instance.buildingPrefabs, GlobalManager.Instance.windPrefabs);
        }

        public void OpenArchiveInGame()
        {
            Archive.Instance.OpenArchive(GlobalManager.Instance.buildingPrefabs, DeckManager.Instance.startingDeck.ToArray());
        }
    }
}
