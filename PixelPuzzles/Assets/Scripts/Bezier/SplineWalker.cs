using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    public bool lookForward;
    public BezierSpline spline;

    public Position position;
    public float duration;
    private float progress;
    private float target;

    private void Start()
    {
        //  spline = GameObject.FindGameObjectWithTag("path").GetComponent<BezierSpline>();
    }

    private void Update()
    {
        positionControls();
        setTarget(position);
        moveProgressTowardsTarget();
        if (spline != null)
        {
            transform.position = spline.GetPoint(progress);
        }
    }

    public enum Position
    {
        Left,
        Front,
        Right,
    }

    private void positionControls()
    {
        if(Input.GetKeyDown(KeyCode.P) && position != Position.Right)
        {
            position++;
            print(position);
        }

        if (Input.GetKeyDown(KeyCode.O) && position != Position.Left)
        {
            position--;
            print(position);
        }
    }

    public void setTarget(Position position)
    {
        switch (position)
        {
            case Position.Left:
                target = 0f;
                break;
            case Position.Front:
                target = .5f;
                break;
            case Position.Right:
                target = 1f;
                break;
            default:
                break;
        }
    }

    private void moveProgressTowardsTarget()
    {
        if(progress < target)
        {
            progress += Time.deltaTime / duration;
        }

        if (progress > target)
        {
            progress -= Time.deltaTime / duration;
        }
    }

    public void setRandomPosition()
    {
        int move = Random.Range(0, 2);
        switch (position)
        {
            case Position.Left:
                if(move == 0)
                {
                    position = Position.Front;
                }
                else
                {
                    position = Position.Right;
                }
                break;
            case Position.Front:
                if (move == 0)
                {
                    position = Position.Left;
                }
                else
                {
                    position = Position.Right;
                }
                break;
            case Position.Right:
                if (move == 0)
                {
                    position = Position.Left;
                }
                else
                {
                    position = Position.Front;
                }
                break;
            default:
                break;
        }
    }

    public bool isPositionReached()
    {
        setTarget(position);
        if (Mathf.Abs(target - progress) <= .01f)
        {
            return true;
        }
        return false;
    }

    public void assignPath(string path)
    {
        spline = GameObject.FindGameObjectWithTag(path).GetComponent<BezierSpline>();
    }

    public void setPosition(Position target)
    {
        position = target;
    }

    public void setProgress(float percentage)
    {
        progress = percentage;
    }
}