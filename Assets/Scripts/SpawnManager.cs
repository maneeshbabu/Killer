using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject _speedPowerUpPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerUpContainer;

    private bool _isPlayerAlive = true;

    [SerializeField]
    private float _interval = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemyRoutine");
        StartCoroutine(SpawnPowerUpRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while(_isPlayerAlive)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9.0f, 9.0f), 7.4f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (_isPlayerAlive)
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 30.0f));
            int randomPowerUp = Random.Range(0, 3);

            GameObject tripleShotPowerUp = Instantiate(_powerups[randomPowerUp], new Vector3(Random.Range(-9.0f, 9.0f), 7.4f, 0), Quaternion.identity);
            tripleShotPowerUp.transform.parent = _powerUpContainer.transform;
        }
    }

    public void PlayerDied()
    {
        _isPlayerAlive = false;
    }
}
