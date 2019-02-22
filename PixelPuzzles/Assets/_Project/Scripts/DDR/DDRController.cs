using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rhythm;

public class DDRController : MonoBehaviour
{
    public RhythmTool rhythmTool;
    public RhythmEventProvider eventProvider;
    public GameObject linePrefab;
    public List<AudioClip> audioClips;
    public float zOffset;
    public int frameOffset;
    public GameObject NoteBar;

    private Floor floor;
    private List<Line> lines;
    private int currentSong;
    private ReadOnlyCollection<float> magnitudeSmooth;

    void Start()
    {
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        spawnNoteBar();
        currentSong = -1;
        Application.runInBackground = true;

        lines = new List<Line>();

        eventProvider.Onset += OnOnset;
        eventProvider.Beat += OnBeat;
        eventProvider.Change += OnChange;
        eventProvider.SongLoaded += OnSongLoaded;
        eventProvider.SongEnded += OnSongEnded;

        magnitudeSmooth = rhythmTool.low.magnitudeSmooth;

        if (audioClips.Count <= 0)
            Debug.LogWarning("no songs configured");
        else
            NextSong();
    }

    private void OnSongLoaded()
    {
        rhythmTool.Play();
    }

    private void OnSongEnded()
    {
        NextSong();
        Destroy(this.gameObject);
    }

    private void NextSong()
    {
        ClearLines();

        currentSong++;

        if (currentSong >= audioClips.Count)
            currentSong = 0;

        rhythmTool.audioClip = audioClips[currentSong];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NextSong();

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        if (!rhythmTool.songLoaded)
            return;

        UpdateLines();

       // rhythmTool.DrawDebugLines();
    }

    private void UpdateLines()
    {
        List<Line> toRemove = new List<Line>();
        foreach (Line line in lines)
        {
            if (line.index < rhythmTool.currentFrame + frameOffset || line.index > rhythmTool.currentFrame + eventProvider.offset)
            {
                Destroy(line.gameObject);
                toRemove.Add(line);
            }

            if(line == null)
            {
                toRemove.Add(line);
            }
        }

       
        foreach (Line line in toRemove)
            lines.Remove(line);

        float[] cumulativeMagnitudeSmooth = new float[eventProvider.offset + 1];
        float sum = 0;
        for (int i = 0; i < cumulativeMagnitudeSmooth.Length; i++)
        {
            int index = Mathf.Min(rhythmTool.currentFrame + i, rhythmTool.totalFrames - 1);

            sum += magnitudeSmooth[index];
            cumulativeMagnitudeSmooth[i] = sum;
        }

        foreach (Line line in lines)
        {
            Vector3 pos = line.transform.position;
            // pos.z = cumulativeMagnitudeSmooth[line.index - rhythmTool.currentFrame] * .01f;
            // pos.z -= magnitudeSmooth[rhythmTool.currentFrame] * .01f * rhythmTool.interpolation;
            pos.z = (line.index - rhythmTool.currentFrame) * .075f;
            pos.z -= zOffset;
            line.transform.position = pos;
        }
    }

    private void OnBeat(Beat beat)
    {
      //  lines.Add(CreateLine(beat.index, Color.white, 20, -40));
    }

    private void OnChange(int index, float change)
    {
       // if (change > 0)
         //   lines.Add(CreateLine(index, Color.yellow, 20, -60));
    }

    private void OnOnset(OnsetType type, Onset onset)
    {
        if (onset.rank < 4 && onset.strength < 5)
            return;

        if(onset.rank < 5)
        {
            return;
        }
        
        switch (type)
        {
            case OnsetType.Low:
                //  lines.Add(CreateLine(onset.index, Color.blue, onset.strength, -20));
                var random = Random.Range(0, 4);
                lines.Add(CreateLine(onset.index, Color.blue, onset.strength, -1.5f + (1 * random)));
               // lines.Add(CreateLine(onset.index, Color.blue, onset.strength, -5.5f + (1 * random)));
               // lines.Add(CreateLine(onset.index, Color.blue, onset.strength, 2.5f + (1 * random)));
                break;
            case OnsetType.Mid:
              //  lines.Add(CreateLine(onset.index, Color.green, onset.strength, 0));
                break;
            case OnsetType.High:
                //   lines.Add(CreateLine(onset.index, Color.yellow, onset.strength, 20));
                //lines.Add(CreateLine(onset.index, Color.blue, onset.strength, 0));
                break;
            case OnsetType.All:
              //  lines.Add(CreateLine(onset.index, Color.magenta, onset.strength, 40));
                break;
        }
    }

    private Line CreateLine(int index, Color color, float opacity, float xPosition)
    {
        GameObject lineObject = Instantiate(linePrefab) as GameObject;
        lineObject.transform.position = new Vector3(xPosition, 0, 0f);

        Line line = lineObject.GetComponent<Line>();
        line.Init(color, 255, index);

        line.transform.parent = this.transform;
        return line;
    }

    private void ClearLines()
    {
        foreach (Line line in lines)
            Destroy(line.gameObject);

        lines.Clear();
    }

    private void OnDestroy()
    {
        floor.clearAllTiles();
    }

    void spawnNoteBar()
    {
        GameObject noteBar = Instantiate(NoteBar, floor.getTile(0, 1).GetComponent<Transform>());
        noteBar.transform.parent = null;
    }
}
