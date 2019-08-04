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

    public GameObject Player1_Prefab;
    public GameObject Player2_Prefab;
    public int charID;

    public GameObject Player1;
    public GameObject Player2;

    public GameObject[] P1Lives;
    public GameObject[] P2Lives;
    public GameObject[] winScreen;
    public GameObject buttons;
    public GameObject resume;

    // Public GameObject PlayerObj;
    public GameObject[] respawnPosition;

    private int respawnP1 = 0;
    private int respawnP2 = 1;
    private int player1Count = 1;
    private int player2Count = 1;
    private int P1KillCount = 0;
    private int P2KillCount = 0;
    private int P1LivesLen;
    private int P2LivesLen;

    // Use this for initialization
    void Awake()
    {
        Screen.fullScreen = false;
        gameManager = this;
        P1LivesLen = P1Lives.Length - 1;
        P2LivesLen = P2Lives.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Player1.GetComponent<PlayerControl>().enabled = false;
            Player2.GetComponent<PlayerControl>().enabled = false;
            buttons.SetActive(true);
            resume.SetActive(true);
        }

        if(player1Count < 1)
        {
            Player1 = Instantiate(Player1_Prefab, respawnPosition[respawnP1].transform.position, Quaternion.identity);
            player1Count++;
        }

        if(player2Count < 1)
        {
            Player2 = Instantiate(Player2_Prefab, respawnPosition[respawnP2].transform.position, Quaternion.identity);
            player2Count++;
        }

        if(P1KillCount >= 3 || P2KillCount >= 3)
        {
            Player1.GetComponent<PlayerControl>().enabled = false;
            Player2.GetComponent<PlayerControl>().enabled = false;

            if(P1KillCount >= 3)
            {
                winScreen[0].SetActive(true);
            }
            if (P2KillCount >= 3)
            {
                winScreen[1].SetActive(true);
            }

            buttons.SetActive(true);
        }
    }

    public void DecreasePlayerCount(int playerID)
    {
        if (playerID == 1)
        {
            player1Count--;
            P1Lives[P1LivesLen].SetActive(false);
            P1LivesLen--;
        }
        else if (playerID == 2)
        {
            player2Count--;
            P2Lives[P2LivesLen].SetActive(false);
            P2LivesLen--;
        }
    }

    public void IncreasePlayerKillCount(int playerID)
    {
        if (playerID == 1)
            P1KillCount++;
        else if (playerID == 2)
            P2KillCount++;
    }

    public void resumeGame()
    {
        Player1.GetComponent<PlayerControl>().enabled = true;
        Player2.GetComponent<PlayerControl>().enabled = true;
        buttons.SetActive(false);
        resume.SetActive(false);
    }
}
