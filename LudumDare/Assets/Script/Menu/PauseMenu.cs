using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button _resumePlay;

    void Start()
    {
        _resumePlay.onClick.AddListener(ResumeGame);
    }


    private void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }
}
