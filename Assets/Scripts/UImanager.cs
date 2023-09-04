using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UImanager : MonoBehaviour
{  
    [SerializeField]
    private Text _scoretext , bestText;
    

    // private int _score;

    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _GameOvertext;
    [SerializeField]
    private Image _Gameover;
    [SerializeField]
    private Text _Restarttext;
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private GameManager _gamemanager;



    // Start is called before the first frame update
    void Start()
    {
        _scoretext.text = "Score : "+ 0;
        _gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gamemanager == null)
        {
            Debug.LogError("GAME MANAGER IS NULL ... ");
        }
    }

    // Update is called once per frame
    public void UpdateScore(int _points)
    {
        _scoretext.text = "Score : " + _points.ToString();
        _GameOvertext.gameObject.SetActive(false);
    }

    public void UpdateLives(int currentlives)
    {
        _LivesImg.sprite = _liveSprites[currentlives];

        if(currentlives<=0)
        {   
            
            GameOverSequence();
        }
    }

    public void UpdateAmmo(int _value)
    {
        _ammoText.text = "Ammo : " + _value.ToString();
    }

    void GameOverSequence()
    {
        _gamemanager.Gamelost();
        _GameOvertext.gameObject.SetActive(true);
        _Gameover.gameObject.SetActive(true);
        _GameOvertext.text = "GAME OVER";
        _Restarttext.gameObject.SetActive(true);
        StartCoroutine(FlickGameoverRoutine());
    }
    IEnumerator FlickGameoverRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.15f);
            _GameOvertext.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.15f);
            _GameOvertext.gameObject.SetActive(true);
            
        }
    }

    public void ResumePlay()
    {
        GameManager gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        gm.ResumeGame();
    }

    public void RestartPlay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);  
    }
}
