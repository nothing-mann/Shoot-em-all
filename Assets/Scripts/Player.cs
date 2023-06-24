//Namespaces ..........
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Custom Class ............ the class name should be the same as the file name for the script to work
public class Player : MonoBehaviour //MonoBehaviour is Unity specific and it is what allows us to drag and drop the script and behaviours onto the game object
{
    /*For variables in C#,
            indicate whether public or private
            datatype
            variablename
            assignment is optional

            using _ before the name is the .net standard for the private variable and we can make use of attributes to control it from the unity even when private
     */
    [SerializeField]
    private float _speed = 6f;
    private float _originalSpeed;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private bool _tripleShotActive = false;
    [SerializeField]
    private bool _speedUpActive = false;
    [SerializeField]
    private bool _shieldUpActive = false;
    [SerializeField]
    private GameObject _shield;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _rightDamage;
    [SerializeField]
    private GameObject _leftDamage;
    private AudioSource _laserAudio;
    [SerializeField]
    private AudioClip _laserClip;
    [SerializeField]
    private int _score;


    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new Position(0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _laserAudio = GetComponent<AudioSource>();
        if(_laserAudio == null)
        {
            Debug.LogError("Audiosource in the player is null");
        }
        else
        {
            _laserAudio.clip = _laserClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CreateMovement();
        

        //For space key pressed
        // if (spacekey pressed
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
           FireLaser(); 
            
        }
        
    }

    void CreateMovement()
    {
        //for userinput
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");



        //transform.Translate(Vector3.left);
        //transform.Translate(Vector3.right * Time.deltaTime);
        //transform.Translate(new Vector3(1, 0, 0)* horizontalInput * _speed * Time.deltaTime); // Time.deltaTime helps us to get and incorporate the real time. In other words it takes from frame rate dependent to Real time dependent.
        //transform.Translate(new Vector3(0, 1, 0) * verticalInput * _speed * Time.deltaTime);
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        /* if transform.position.x > ..... or transform.position.y > ......
         *       transform.position.x = same
         *       transform.position.y = same
         * elseif transform.position.x < .... or transform.position.y < ......
         *       transform.position.x = same
         *       transform.position.y = same
          */
        float outOfBoundsX = 11.32177f;
        float outOfBoundsY = 6f;
        float stopY = -4.5f;
        if (transform.position.x >= outOfBoundsX)
        {
            transform.position = new Vector3(-outOfBoundsX, transform.position.y, 0);
        }
        else if (transform.position.x <= -outOfBoundsX)
        {
            transform.position = new Vector3(outOfBoundsX, transform.position.y, 0);
        }
        else if (transform.position.y >= outOfBoundsY)
        {
            transform.position = new Vector3(transform.position.x, -outOfBoundsY, 0);
        }
        else if (transform.position.y <= stopY)
        {
            transform.position = new Vector3(transform.position.x, stopY, 0);
        }
    }

    void FireLaser()
    {
        //Debug.Log("Space key pressed!!");
        _canFire = Time.time + _fireRate;

        if(_tripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        }

        else 
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        }
        _laserAudio.Play();
        
    }

    public void Damage()
    {
        if(_shieldUpActive == true)
        {
            _shieldUpActive = false;
            _shield.SetActive(false);
            return;
        }


        _lives--;

        if(_lives == 2)
        {
            _leftDamage.SetActive(true);
        }
        if(_lives == 1)
        {
            _rightDamage.SetActive(true);
        }
        _uiManager.UpdateLives(_lives);

        if(_lives <1)
        {
            _spawnManager.OnPlayerDeath();
            _uiManager.GameOverText();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    { 
        _tripleShotActive = true;
        StartCoroutine(TripleShotDisableRoutine());
    }
    
    IEnumerator TripleShotDisableRoutine() 
    {
        while (_tripleShotActive == true)  
        {
            yield return new WaitForSeconds(5.0f);
            _tripleShotActive = false;
        }
    }

    public void SpeedUpActive()
    {
        _speedUpActive = true;
        StartCoroutine(SpeedUpActiveRoutine());
    }

    IEnumerator SpeedUpActiveRoutine()
    {
        while (_speedUpActive == true) 
        {
            _originalSpeed = _speed;
            _speed = 20.0f;
            yield return new WaitForSeconds(5.0f);
            _speedUpActive = false;
            _speed = _originalSpeed;
        }
        
    }

    public void ShieldUpActive()
    {
        _shieldUpActive= true;
        //StartCoroutine(ShieldUpActiveRoutine());
        _shield.SetActive(true);
    }

    public int AddScore(int points) 
    {
        return _score += points;
    }

    public int GetScore()
    {
        return _score;
    }

}
