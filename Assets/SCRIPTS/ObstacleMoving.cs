using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleMoving : MonoBehaviour
{
    private GameManager GM;
    private ScoreManager SM;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        SM = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        transform.parent.Translate(0,0, -Time.deltaTime * GM.obstacleSpeed);
        if(transform.parent.position.z < -10) Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Debug.Log("rip");
            GM.GameOver();
            SceneManager.LoadScene("EndMenu", LoadSceneMode.Single);
        }
    }

}
