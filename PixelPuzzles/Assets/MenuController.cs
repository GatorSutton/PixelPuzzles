using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {


    public textController tC;
    public List<GameObject> gameList = new List<GameObject>();
    int roundNumber = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(gameLoop());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator gameLoop()
    {
        foreach(GameObject game in gameList)
        {
            //print the game and wait for rea
            tC.setText(game.GetComponent<nameToBeDisplayed>().name);
            yield return new WaitForSeconds(5f);
            tC.startFadeOffScreen();
            yield return new WaitForSeconds(3f);
            var currentGame = Instantiate(game);
            yield return new WaitUntil(() => currentGame == null);
        }
        yield return null;
    }

}
