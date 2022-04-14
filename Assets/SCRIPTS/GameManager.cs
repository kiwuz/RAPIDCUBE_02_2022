using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public float textureSpeed;
    public float obstacleSpeed;
    public int score;
    public int current_speed;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text speedText;
    [SerializeField] private Text speedUpText;

    [SerializeField] private float nextSpeedUp;
    private Animator speedUpAnim;
    [SerializeField] private Text scoreUpText;
    private Animator scoreUpAnim;
    private bool isSpeedingUp;
    static private int lastScore = 0;

    private void Awake() {
        speedUpAnim = speedUpText.GetComponent<Animator>();
        scoreUpAnim = scoreUpText.GetComponent<Animator>();
    }

    void Start()
    {
        Cursor.visible = false;
        obstacleSpeed = 50f;
        textureSpeed = 5f;

        score = 0;
        nextSpeedUp = 10f;
        isSpeedingUp = false;

        current_speed = 100;
    }

    void FixedUpdate()
    {
        score += 1;
        scoreText.text = score.ToString();

    }
    void Update(){
            if (isSpeedingUp == false) {
                if(score > 250){
                    StartCoroutine("SpeedUp");
                    isSpeedingUp = true;
                }
            }
            speedText.text = "SPEED: " + current_speed;
    }

    public void CollectScore(){
        scoreUpAnim.SetTrigger("ScoreUp");
    }

    IEnumerator SpeedUp(){
            while(true){
                speedUpAnim.SetTrigger("SpeedUpAnim");
                obstacleSpeed = obstacleSpeed + 5f;
                textureSpeed = textureSpeed + 0.25f;
                nextSpeedUp = Random.Range(6,22);
                current_speed = current_speed + 100;

                yield return new WaitForSeconds(nextSpeedUp);
        }
    }

    public void GameOver(){
        lastScore = score;
    }


    static public int getLastScore() {
        return lastScore;
    }

}
