using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelManager : MonoBehaviour
{
    private Renderer[] walls;
    private GameManager GM;
	void Start () {
        

        GM = FindObjectOfType<GameManager>();

        walls = new Renderer[] {
            transform.Find("Top").GetComponent<Renderer>(),
            transform.Find("Bottom").GetComponent<Renderer>(),
            transform.Find("Left").GetComponent<Renderer>(),
            transform.Find("Right").GetComponent<Renderer>(),
        };
	}
	void Update () {
        for (int i = 0; i < walls.Length; ++i) {
            walls[i].material.mainTextureOffset 
                += new Vector2(0.0f, + GM.textureSpeed * Time.deltaTime);
        }
	}
}
