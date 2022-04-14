using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private GameManager GM;
    private int yourScore;
    [SerializeField] private Text yourScoreTxT; 
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        int lastScore = GameManager.getLastScore();
        yourScoreTxT.text = "your score: " + lastScore.ToString();
    }



}
