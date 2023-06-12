using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _speed = 3.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1.0f, 0)* _speed * Time.deltaTime);
        if(transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) 
            {
                player.TripleShotActive();
            }
            Destroy(this.gameObject);
        }
    }
}
