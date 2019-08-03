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

    // Public GameObject PlayerObj;
    public GameObject[] respawnPosition;
    public int respawnP1 = 0;
    public int respawnP2 = 1;

    private int player1Count = 1;
    private int player2Count = 1;
    private int P1KillCount = 0;
    private int P2KillCount = 0;

    // Use this for initialization
    void Awake()
    {
        Screen.fullScreen = false;
        gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(player1Count < 1)
        {
            GameObject hold = Instantiate(Player1_Prefab, respawnPosition[respawnP1].transform.position, Quaternion.identity);

            Vector3 theScale = hold.transform.localScale;
            theScale.x *= -1;
            hold.transform.localScale = theScale;

            player1Count++;
        }

        if(player2Count < 1)
        {
            Instantiate(Player2_Prefab, respawnPosition[respawnP2].transform.position, Quaternion.identity);
            player2Count++;
        }
    }

    public void DecreasePlayerCount(int playerID)
    {
        if(playerID == 1)
            player1Count--;
        else if(playerID == 2)
            player2Count--;
    }

    public void IncreasePlayerKillCount(int playerID)
    {
        if (playerID == 1)
            P1KillCount++;
        else if (playerID == 2)
            P2KillCount++;
    }
}
