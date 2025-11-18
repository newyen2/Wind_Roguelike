using System;
using TMPro;
using UnityEngine;

public class Guild : MonoBehaviour
{
    public static Guild Instance;
    int page = 0;
    public GameObject[] guilds;
    public Transform guildTrans;
    public string[] texts;
    public TMP_Text text;

    void Start()
    {
        Instance = this;
        SwitchPage();
    }

    public void ChangePage(int num)
    {
        page += num;
        if(page < 0) page = 0;
        if(page > 3) page = 3;
        SwitchPage();
    }

    public void SwitchPage()
    {
        Destroy(guildTrans.GetChild(0).gameObject);

        Instantiate(guilds[page], guildTrans);
        text.text = texts[page];
    }

    public void ShowGuild()
    {
        transform.localPosition = new Vector2(0, 0);
    }

    public void Close()
    {
        transform.localPosition = new Vector2(1920, -9999);
    }
}