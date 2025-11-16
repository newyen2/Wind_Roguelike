using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Go2Map() { 
        GameManager.Instance.SwitchScene("Map");
    }

    public void Go2Stage()
    {
        GameManager.Instance.SwitchScene("Stage");
    }
    
    public void Go2Menu()
    {
        GameManager.Instance.SwitchScene("Menu");
    }

    public void Go2Initial()
    {
        GameManager.Instance.SwitchScene("InitialReward");
    }

}
