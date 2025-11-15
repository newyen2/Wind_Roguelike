using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowStageInfo : MonoBehaviour
{

    public Text myText;
    // Start is called before the first frame update
    void Start()
    {
        int recordsCount = GlobalManager.Instance.recordsCount;
        int targetScore = GlobalManager.Instance.records[recordsCount].targetScore;
        myText.text = $"Stage {recordsCount + 1} \nGoal: {targetScore}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
