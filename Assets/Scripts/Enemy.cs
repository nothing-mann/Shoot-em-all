using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 3.5f;
    private float _outOfBoundsY = -6.8f;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private Player _player;
    private int _randomRange;
    private Animator _enemyExplosionAnim;

    void Start()
    {
        transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 4, 0);
        _randomRange = Random.Range(5, 10);
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Error!! Player gameobject not found");
        }
        transform.localScale = new Vector3(_randomRange*0.1f, _randomRange*0.1f, _randomRange*0.1f);
        _enemyExplosionAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);
        if (transform.position.y <= _outOfBoundsY)
        {
            float RandomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(RandomX, -_outOfBoundsY, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            _enemyExplosionAnim.SetTrigger("OnEnemyDeath");
            _speed = _speed/2;
            Destroy(this.gameObject, 1.3f);
        }

        else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player!= null)
            {
                _player.AddScore(Random.Range(5,10));
            }
            _enemyExplosionAnim.SetTrigger("OnEnemyDeath");
            _speed = _speed/2;
            Destroy(this.gameObject, 1.3f);
            
        }
    }
}
