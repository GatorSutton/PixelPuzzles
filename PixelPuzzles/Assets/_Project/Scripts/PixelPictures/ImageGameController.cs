using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageGameController : MonoBehaviour {

    /*
    * set all the tiles to switches that will reveal the picture
    * clear the board after 10 seconds
    * show the answer and 2 extra images around the players
    * spawn 3 select boxes
    * notify correct or incorect
    */

    public List<Sprite> spriteList;
    public textController tC;

    Floor floor;
    List<Tile> tileList;
    PixelGrid gridOfPixels;
    List<Tile.States> statesMap;
    [SerializeField]
    ImageDefinitions.Direction correctDirection;
    bool answered = false;

    public List<SpriteRenderer> currentSpriteRenderers = new List<SpriteRenderer>();
    public List<Selector> selectors = new List<Selector>();
    public float timeToFlip;
    public progressBar pB;
    public progressBarText pBT;

    public Slider decisionSlider;
    

    // Use this for initialization
    void Start () {
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        tileList = floor.getAllTiles();

        StartCoroutine(multipleRounds(1));

	}

    void setUpCurrentSpriteList()
    {
        for (int i = 0; i < 3; i++)
        {
            var random = Random.Range(0, spriteList.Count);
            currentSpriteRenderers[i].sprite = spriteList[random];
            currentSpriteRenderers[i].GetComponent<PixelGrid>().setPixelMap();
            spriteList.RemoveAt(random);
        }

        //select randomly one of the three current sprites

        gridOfPixels = GetComponentsInChildren<PixelGrid>()[Random.Range(0, currentSpriteRenderers.Count)];
        print(gridOfPixels.GetComponent<SpriteRenderer>().sprite.name);
        correctDirection = gridOfPixels.transform.parent.GetComponentInChildren<Selector>().direction;

    }

    void setTileList()
    {
        statesMap = gridOfPixels.createStatesMap();
        for (int i = 0; i < tileList.Count; i++)
        {
            tileList[i].myState = Tile.States.FLIP;
            tileList[i].flippedState = statesMap[i];
        }
    }

    void setTilesRevealed()
    {
        for (int i = 0; i < tileList.Count; i++)
        {
            tileList[i].myState = statesMap[i];
        }
    }

    IEnumerator prepareForAnswer()
    {
        foreach(SpriteRenderer SR in currentSpriteRenderers)
        {
            SR.enabled = true;
            while (Vector3.Distance(SR.transform.localPosition, new Vector3(0f, 0f, 0f)) > .1f)
            {
                SR.transform.localPosition = Vector3.MoveTowards(SR.transform.localPosition, new Vector3(0f, 0f, 0f), Time.deltaTime);
                yield return null;
            }
        }

        foreach (SpriteRenderer SR in currentSpriteRenderers)
        {
            var selector = SR.transform.parent.GetComponentInChildren<Selector>();
            selector.startInitializeSelector();
            selector.toggleBoxCollider();
        }

        foreach (Tile tile in tileList)
        {
            tile.myState = Tile.States.NONE;
            tile.flipped = false;
        }
        foreach(Selector selector in selectors)
        {
            selector.on = true;
        }
        

    }

    IEnumerator playRound()
    {

        yield return StartCoroutine(getReady());
        setUpCurrentSpriteList();
        setTileList();
        print("Reveal the Image!");
        yield return StartCoroutine(countDownTimer(timeToFlip));
        yield return StartCoroutine(prepareForAnswer());
        print("Unanimous Decision");
        yield return decisionTimer(10);
        setTilesRevealed();
        yield return new WaitForSeconds(5f);
        resetImages();
        answered = false;

    }

    IEnumerator decisionTimer(float timeToDecide)
    {
        float timer = timeToDecide;
        while(answered == false && timer > 0)
        {
            timer -= Time.deltaTime;
            decisionSlider.value = (timer / timeToDecide);
            yield return null;
        }
        // decisionPB.setPercent(0f);
        decisionSlider.value = 0;
    }

    IEnumerator multipleRounds(int numOfRounds)
    {
        for (int i = 0; i < numOfRounds; i++)
        {
            yield return StartCoroutine(playRound());
        }
        Destroy(this.gameObject);
    }

    public void AnswerSelected(ImageDefinitions.Direction direction)
    {
        if(direction == correctDirection)
        {
            ScoreController.AddScore(5000);
            print("NOICE");
        }
        else
        {
            ScoreController.AddScore(-5000);
            print("WRONG");
        }

        foreach(SpriteRenderer SR in currentSpriteRenderers)
        {
            var selector = SR.transform.parent.GetComponentInChildren<Selector>();
            if(selector.direction != direction)
            {
                SR.enabled = false;
                SR.transform.localPosition = new Vector3(0f, 0f, -3f);
            }
        }


        foreach (Tile tile in tileList)
        {
            tile.myState = Tile.States.NONE;
        }
        foreach(Selector selector in selectors)
        {
            selector.on = false;
            selector.percentage = 0;
        }

        answered = true;

    }

    void OnDestroy()
    {
        //cleanup minigame
        floor.clearAllTiles();
    }

    IEnumerator countDownTimer(float timeToFlip)
    {
        float timer = timeToFlip;

        while (timer >= 0)
        {
            pBT.setString(Mathf.Ceil(timer).ToString());
            pB.setPercent(timer/timeToFlip);
            timer -= Time.deltaTime;
            yield return null;
        }
        foreach (Tile tile in tileList)
        {
            tile.myState = Tile.States.NONE;
            tile.flipped = false;
        }
        pB.setPercent(0f);
 
    }

    void resetImages()
    {
        foreach (SpriteRenderer SR in currentSpriteRenderers)
        {
            SR.transform.localPosition = new Vector3(0f, 0f, -3f);
            SR.enabled = false;
        }
    }

    IEnumerator getReady()
    {
        print("READY");
        tC.setText("READY");
        yield return new WaitForSeconds(2f);
        print("SET");
        tC.setText("SET");
        yield return new WaitForSeconds(2f);
        print("REVEAL");
        tC.setText("REVEAL");
        tC.startFadeOffScreen();
        yield return new WaitForSeconds(3f);

    }


}
