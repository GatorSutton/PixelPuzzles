using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageGameController : MonoBehaviour {

    /*
    * set all the tiles to switches that will reveal the picture
    * clear the board after 10 seconds
    * show the answer and 2 extra images around the players
    * spawn 3 select boxes
    * notify correct or incorect
    */

    public List<Sprite> spriteList;

    Floor floor;
    List<Tile> tileList;
    PixelGrid gridOfPixels;
    List<Tile.States> statesMap;
    [SerializeField]
    ImageDefinitions.Direction correctDirection;
    bool answered = false;

    public List<SpriteRenderer> currentSpriteRenderers = new List<SpriteRenderer>();
    public List<Selector> selectors = new List<Selector>();
    

    // Use this for initialization
    void Start () {
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        tileList = floor.getAllTiles();

        StartCoroutine(multipleRounds(3));

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
        correctDirection = gridOfPixels.GetComponentInChildren<Selector>().direction;

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

    //clear the tiles
    //display sprites
    //spawn answer boxes
    void prepareForAnswer()
    {
        foreach(SpriteRenderer SR in currentSpriteRenderers)
        {
            SR.enabled = true;
            var selector = SR.GetComponentInChildren<Selector>();
            selector.startInitializeSelector();
            selector.toggleBoxCollider();
        }
        
        foreach (Tile tile in tileList)
        {
            tile.myState = Tile.States.NONE;
        }
        foreach(Selector selector in selectors)
        {
            selector.on = true;
        }
        

    }

    IEnumerator playRound()
    {
        setUpCurrentSpriteList();
        setTileList();
        yield return new WaitForSeconds(7f);
        prepareForAnswer();
        while (answered == false)
        {
            yield return null;
        }
        answered = false;

    }

    IEnumerator multipleRounds(int numOfRounds)
    {
        for (int i = 0; i < numOfRounds; i++)
        {
            yield return StartCoroutine(playRound());
        }
    }

    public void AnswerSelected(ImageDefinitions.Direction direction)
    {
        if(direction == correctDirection)
        {
            print("NOICE");
        }
        else
        {
            print("WRONG");
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
        foreach (SpriteRenderer spriteRenderer in currentSpriteRenderers)
        {
            spriteRenderer.enabled = false;
        }
        answered = true;

    }
	

}
