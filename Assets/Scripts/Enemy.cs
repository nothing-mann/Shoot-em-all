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
    void Start()
    {
        transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 4, 0);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
