using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour
{
    [SerializeField] Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(StartNewGame);
    }


    private void StartNewGame()
    {
        GameManager.Instance.LoadScene(GameManager.Scene.Level_Block);
    }

}