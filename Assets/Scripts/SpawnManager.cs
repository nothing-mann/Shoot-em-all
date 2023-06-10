using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //create a coroutine that spawns gameobject every 5 seconds
    //Coroutine IEnumerator ... Yield
    //while loops (always infinite loop with coroutines with yield)

    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn= new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-5.0f, 5.0f), 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(1.0f, 3.5f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}