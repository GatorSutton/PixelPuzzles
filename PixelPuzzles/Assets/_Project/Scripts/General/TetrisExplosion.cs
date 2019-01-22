using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisExplosion : MonoBehaviour {

    public List<GameObject> tetrisShapes = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Explode(Transform location, float time)
    {
        float timer = 0;

        while(timer < time)
        {
            timer += Time.deltaTime;
            Instantiate(tetrisShapes[Random.Range(0, tetrisShapes.Count)], location);
            yield return null;
        }
    }

    public void startExplode(Transform location, float time)
    {
        StartCoroutine(Explode(location, time));
    }
}
