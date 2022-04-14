using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour
{
    private GameManager GM;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        Cursor.visible = true;
    }

    public void PlayAgain(){
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

    }

   public void QuitGame(){
        Application.Quit();
    }
}
