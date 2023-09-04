using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;
    [SerializeField]
    private GameObject _pauseMenuPanel;
    private UImanager _uimanager;
    private Animator _PauseAnimator;

    public void Start()
    {
        _uimanager = GameObject.Find("Canvas").GetComponent<UImanager>();
        _PauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        _PauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1);                //LoadScence(0) refers to current Screen
        }

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);                //LoadScence(0) refers to current Screen
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true);
            _PauseAnimator.SetBool("IsPaused",true);
            Time.timeScale = 0;

        }
    }

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
        
    }
    public void Gamelost()
    {
        _isGameOver = true;
    }


}
