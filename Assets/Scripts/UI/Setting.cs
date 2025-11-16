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

}