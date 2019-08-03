using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    private int index = 0;

    public GameObject[] countUI;
    public GameObject Player1;
    public GameObject Player2;

    void Start()
    {
        InvokeRepeating("StartGame", 1.0f, 1.0f);
    }

    void StartGame()
    {
        if (index == 4)
        {
            if(Player1 != null)
                Player1.SetActive(true);
            if(Player2 != null)
                Player2.SetActive(true);
            countUI[index - 1].SetActive(false);
            return;
        }

        if (index != 0)
        {
            countUI[index - 1].SetActive(false);
        }
        countUI[index].SetActive(true);

        index++;
    }
}
