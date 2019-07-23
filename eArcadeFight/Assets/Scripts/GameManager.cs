using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public Image[] wUIImage;
    public Sprite[] wIdleUIImage;
    public Sprite[] wPressedUIImage;

    public GameObject[] wKnightPrefab;
    public int charID;

    // Public GameObject PlayerObj;
    public List<GameObject> wMonsterList = new List<GameObject>();
    public GameObject[] wRespawnPos;
    public GameObject wMonsterPrefab;
    public int respawnID = 0;
    // Use this for initialization
    void Awake()
    {
        Screen.fullScreen = false;

        gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
