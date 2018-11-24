using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressBar : MonoBehaviour {

    [SerializeField]
    private float percentComplete;
    private ScanFloorForMatch sFFM;

    public Color startColor;
    public Color endColor;

    public Image image;



    // Use this for initialization
    void Start()
    {
        sFFM = gameObject.GetComponentInParent<ScanFloorForMatch>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        percentComplete = sFFM.percentComplete;
        image.fillAmount = sFFM.percentComplete;
        image.color = Color.Lerp(startColor, endColor, percentComplete);
        
    }
}
