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
    }

    public void SetSFXVolume(float v)
    {
        AudioManager.Instance.SFX_volume = v;
    }
}