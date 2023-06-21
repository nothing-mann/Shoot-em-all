using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 2f;
    [SerializeField]
    private GameObject _explosion;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, 3f, 0f);
        //_explosion = GameObject.Find("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime, Space.World);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser") 
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
        Destroy(_explosion, 3.0f);
    }
}
