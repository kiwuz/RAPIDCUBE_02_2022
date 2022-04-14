using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectibles : MonoBehaviour
{
    [SerializeField] private AudioClip coinSound;
    private GameManager GM;

    private const float SPEED = 50F;

    private void Start() {
        GM = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        transform.Rotate( 1 * SPEED * Time.deltaTime ,1 * SPEED * Time.deltaTime,1 * SPEED * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player")){
            AudioSource.PlayClipAtPoint(coinSound,transform.position);
            gameObject.SetActive(false);
            GM.score+=1000;
            GM.CollectScore();
        }
    }
}
