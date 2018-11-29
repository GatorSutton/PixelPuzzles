using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageText : MonoBehaviour
{

    [SerializeField]
    private float percentComplete;
    private Selector selector;

    private Text text;
    private SpriteRenderer SR;



    // Use this for initialization
    void Start()
    {
        selector = transform.parent.parent.GetComponentInChildren<Selector>();
        text = gameObject.GetComponent<Text>();
        //SR = transform.parent.parent.GetComponent<SpriteRenderer>();
        SR = transform.parent.parent.GetComponentInChildren<SpriteRenderer>();
    
    }

    // Update is called once per frame
    void Update()
    {
        percentComplete = selector.percentage;
        text.text = Mathf.Round((percentComplete * 100)) + "%";

        if (SR.enabled == true)
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }
    }
}
