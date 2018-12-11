using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressBar : MonoBehaviour {

    [SerializeField]
    private float percentComplete;
    public Text text;

    public Color startColor;
    public Color endColor;

    public Image image;



    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = percentComplete;
        image.color = Color.Lerp(startColor, endColor, percentComplete);

        if (text != null)
        {
            if(percentComplete <= 0)
            {
                text.enabled = false;
            }
            else
            {
                text.enabled = true;
            }
        }
        
    }

    public void setPercent(float percentage)
    {
        percentComplete = percentage;
    }

}
