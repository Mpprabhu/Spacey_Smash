using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyPrefab;
    [SerializeField]
    private GameObject _EnemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawning = false;

    public void StartSpawning()
    {

        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false)
         {
            Vector3 spawnpos = new Vector3(Random.Range(-9f,9f),7f,0);
            GameObject newEnemy = Instantiate(_EnemyPrefab,spawnpos,Quaternion.identity); 
            newEnemy.transform.parent = _EnemyContainer.transform;    
            yield return new WaitForSeconds(2f);

         }
         
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        while(_stopSpawning == false)
        {
             Vector3 spawnpos = new Vector3(Random.Range(-9f,9f),7f,0);
             int randompowerup = Random.Range(0,4);
            Instantiate(powerups[randompowerup],spawnpos,Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(20,30));
            
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
