using Core;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public static Setting Instance;
    public GameObject setUI;
    void Awake()
    {
        Instance = this;
    }
    

    void Start()
    {
        setUI.SetActive(false);
    }

    public void SetUIButton()
    {
        if(!setUI.activeSelf) setUI.SetActive(true);
        else setUI.SetActive(false);
    }

    public void SetBGMVolume(float v)
    {
        AudioManager.Instance.BGM_volume = v;
        AudioManager.Instance.RefreshVolume();
    }

    public void SetSFXVolume(float v)
    {
        AudioManager.Instance.SFX_volume = v;
        // SFX 通常是一次性的，不需要即時刷新
        // 若你也想調整 SFX，需改 Pool 的 source.volume
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void BackToMenu()
    {
        GameManager.Instance.SwitchScene("Menu");
    }

    public void SetFullScreen()
    {
        #if UNITY_EDITOR
            Debug.LogError("你不能在編輯器內切換全螢幕 !");
        #else
            GameManager.Instance.is_full_screen = !GameManager.Instance.is_full_screen;
            Screen.fullScreen = GameManager.Instance.is_full_screen;
        #endif
    }
}