using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text _score;
    [SerializeField]
    private GameObject _player;
    void Start()
    {
        
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
}
