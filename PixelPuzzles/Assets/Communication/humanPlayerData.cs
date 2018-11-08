using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class humanPlayerData : MonoBehaviour
{

    private SampleMessageListener ML;
    private bool[] boolList;

    // Use this for initialization
    void Start()
    {
        ML = GetComponent<SampleMessageListener>();
    }

    // Update is called once per frame
    void Update()
    {
        creatBoolList(ML.currentString);
    }

    private void creatBoolList(string data)
    {
        boolList = data.Select(x => x == '1').ToArray();
    }

    public bool[] getPlayerData()
    {
        return boolList;
    }
}
