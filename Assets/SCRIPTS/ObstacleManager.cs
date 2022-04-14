using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject nextObstacle;
    private Vector3 obstaclePoint;
    private float obstacleDistance = 200;
    private int obstacleID;
    private GameManager GM;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        Invoke("StartSpawningObstacles", 3f);
        obstaclePoint = new Vector3(0.0f, 0f, obstacleDistance);
    }


    void StartSpawningObstacles(){
        StartCoroutine("SpawnObstacle");
    }

    IEnumerator SpawnObstacle() {
    for(;;) {
            obstacleID = Random.Range(0,obstacles.Length);
            nextObstacle = obstacles[obstacleID];
            Instantiate(nextObstacle, obstaclePoint, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2,3));
        }
    }
}
