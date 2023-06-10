using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 15.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0)*_speed*Time.deltaTime);
        if(transform.position.y >= 6.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
