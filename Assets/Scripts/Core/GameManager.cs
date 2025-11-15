using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }


        void Start()
        {
            
        }

        void Update()
        {

        }

        void StartGame()
        {
            //SceneManager.LoadScene("");
        }


    }
}
