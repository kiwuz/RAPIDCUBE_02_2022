using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float flipOffset = 0.0f;
    private PlayerManager PM;
    private GameObject player;
    public Floor currentFloor;

    void Start()
    {
        PM = FindObjectOfType<PlayerManager>();
        player =  GameObject.FindGameObjectWithTag("Player");
        currentFloor = Floor.Bottom;
    }


    public IEnumerator SmoothFlipCoroutine(Vector3 target, float distance, float rate)
    {
        float angle = 0;
        Vector3 newPoint;
        Vector3 trueTarget = target;
        trueTarget.z = -20.0f;
        float currentDistance = 0;
        float angleRate = 180.0f * rate;
        float translateRate = rate / 50.0f;
        while (angle < 180.0f)
        {

            currentDistance += rate;
            angle += angleRate;
            newPoint = player.transform.position;
            newPoint.z = -20.0f;
            switch (currentFloor)
            {
                case Floor.Bottom:
                    newPoint.y -= flipOffset;
                    break;
                case Floor.Left:
                    newPoint.x -= flipOffset;
                    break;
                case Floor.Top:
                    newPoint.y += flipOffset;
                    break;
                case Floor.Right:
                    newPoint.x += flipOffset;
                    break;
                default:
                    break;
            }

            gameObject.transform.RotateAround(newPoint, new Vector3(0.0f, 0.0f, 1.0f), angleRate);
            angleRate = (float)Mathf.Sqrt(angleRate*5.0f);
            rate = rate - 10.0f * Time.deltaTime;
            yield return null;
        }
        newPoint = gameObject.transform.position;
        gameObject.transform.localEulerAngles = new Vector3(20.0f, 0.0f, 0.0f);
        gameObject.transform.localPosition = new Vector3(0.0f, 2.5f, -5.0f);
        yield return null;
    }


    IEnumerator SmoothRotateCoroutine(float difference, float duration)
    {
        float angle = 0;
        Vector3 newPoint;
        while (Mathf.Abs(angle) < Mathf.Abs(difference))
        {
            angle += difference * Time.deltaTime / 0.1f;
            newPoint = player.transform.position;
            gameObject.transform.RotateAround(newPoint,new Vector3(0.0f, 0.0f, 1.0f),difference * Time.deltaTime / 0.1f);
            yield return null;
        }
        gameObject.transform.localPosition = new Vector3(0.0f, 2.5f, -5.0f);
        gameObject.transform.localEulerAngles = new Vector3(12.0f, 0.0f, 0.0f);
        yield return null;
    }

    public void SmoothRotate(Floor NewFloor)
    {
        if (currentFloor != NewFloor)
        {
            if (
                (NewFloor == Floor.Bottom && currentFloor == Floor.Left) 
                ||(NewFloor > currentFloor && (NewFloor != Floor.Left || currentFloor != Floor.Bottom)))
            {

                Vector3 point = player.transform.position;
                gameObject.transform.RotateAround(point, new Vector3(0.0f, 0.0f, 1.0f), -90.0f);
                StartCoroutine(SmoothRotateCoroutine(90.0f, 1.0f));
            }
            else
            {
                Vector3 point = player.transform.position;
                gameObject.transform.RotateAround(point, new Vector3(0.0f, 0.0f, 1.0f), 90.0f);
                StartCoroutine(SmoothRotateCoroutine(-90.0f, 1.0f));
            }
            if (gameObject.transform.position.z < -20.0f)
            {
                gameObject.transform.Translate(
                    0.0f, 0.0f, -20.0f - gameObject.transform.position.z);
            }
            currentFloor = NewFloor;
        }
    }

   public void unchild()
    {
        gameObject.transform.parent = null;
    }
    public void child(Transform t)
    {
        gameObject.transform.parent = t;
    }


    public void SmoothFlip(Vector3 target, float distance, float rate)
    {
        StartCoroutine(SmoothFlipCoroutine(target, distance, rate));
    }
}
