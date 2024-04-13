using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public LayerMask groundMask;
    public GameState State;
    public Player player;
    public Canvas menu_pause;

    [SerializeField] public Transform checkpoint;

    private void Awake()
    {
        Instance = this;
    }


    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void PauseGame()
    {
        State = GameState.Pause;
        player.IsPaused = true;
        Time.timeScale = 0f;
        menu_pause.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        State = GameState.Action;
        player.IsPaused = false;
        Time.timeScale = 1f;
        menu_pause.gameObject.SetActive(false);
    }


    public enum Scene
    {
        StartMenu,
        EndMenu,
        Level_1,
        Level_2,
        Level_3,
    }


    public enum GameState
    {
        Pause,
        Action,
        Shop,
        Loading
    }

}
