using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {

    public TextMeshProUGUI message;
    public List<GameObject> gameList = new List<GameObject>();
    public int numOfRounds;
    int roundNumber = 0;
    public GameObject readySwitch;

	// Use this for initialization
	void Start () {
        StartCoroutine(gameLoop());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(Time.timeScale == 1.0f)
            {
                Time.timeScale = 10.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }
	}

    IEnumerator gameLoop()
    {
        for (int i = 0; i < numOfRounds; i++)
        {
            foreach (GameObject game in gameList)
            {
                var currentSwitch = Instantiate(readySwitch);
                yield return new WaitUntil(() => currentSwitch == null);

                message.SetText(game.GetComponent<nameToBeDisplayed>().mName);
                yield return new WaitForSeconds(3f);
                message.ClearMesh();

                var currentGame = Instantiate(game);
                yield return new WaitUntil(() => currentGame == null);
            }
        }

    }

}
