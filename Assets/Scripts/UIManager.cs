using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text _score;
    [SerializeField]
    private GameObject _player;
    [SerializeField] 
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("GameManager could not be initialized.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _player = GameObject.FindWithTag("Player");
        if(_player != null ) 
        { 
            int score = _player.GetComponent<Player>().GetScore();
            _score.text = "Score:" + score;
        }
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _liveSprites[currentLives];
    }

    public void GameOverText()
    {
        //_gameOverText.text = "GAME OVER";
        //_restartText.text = "Press R to restart the game.";
        _gameManager.GameOver();

        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            _restartText.text = "Press R to restart the game.";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            _restartText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
