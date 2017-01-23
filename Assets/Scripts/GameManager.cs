using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    /*private static GameManager instance;
    public static GameManager getInstance()
    {
        if (instance == null)
        {
            Debug.Log("GameManager Generated");
            instance = new GameManager();
        }
        return instance;
    }*/

    public Player[] player = new Player[2];
    public bool isGameOver;

    public enum PLAYER_CLASS
    {
        CLASS_SWORD, CLASS_DAGGER, CLASS_SHOOT
    }

    void InitPlayer()
    {
        switch (GameObject.Find("SelectClass").GetComponent<SelectClass>().player_1_selection)
        {
            case 0:
                player[0] = new Player(0, PLAYER_CLASS.CLASS_SWORD);
                break;
            case 1:
                player[0] = new Player(0, PLAYER_CLASS.CLASS_DAGGER);
                break;
            case 2:
                player[0] = new Player(0, PLAYER_CLASS.CLASS_SHOOT);
                break;
        }

        switch (GameObject.Find("SelectClass").GetComponent<SelectClass>().player_2_selection)
        {
            case 0:
                player[1] = new Player(1, PLAYER_CLASS.CLASS_SWORD);
                break;
            case 1:
                player[1] = new Player(1, PLAYER_CLASS.CLASS_DAGGER);
                break;
            case 2:
                player[1] = new Player(1, PLAYER_CLASS.CLASS_SHOOT);
                break;
        }

        for (int i = 0; i < 2; i++)
        {
            
            player[i].setGameObject(GameObject.Find("Player" + (i + 1)));
        }
    }

    void InitTerrain()
    {
        GameObject.Find("JumpZone").GetComponent<JumpZone>().player1 = player[0];
        GameObject.Find("JumpZone").GetComponent<JumpZone>().player2 = player[1];
    }

    void confirmMyPlayer(int i)
    {
        player[i].setMine();
    }

    public void ChangeScene()
    {
        Destroy(GameObject.Find("SelectClass"));
        SceneManager.LoadScene("ClassSelect");
    }

    void Awake()
    {
        isGameOver = false;
        InitPlayer();
        InitTerrain();
    }
}
