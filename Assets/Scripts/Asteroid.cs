using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotatespeed = 7.0f;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _explosionPrefab;

    private SpawnManager _spawnmanager;

        


    void Start()
    {
        _spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward*_rotatespeed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "laser")
        {
            Instantiate(_explosionPrefab,transform.position,Quaternion.identity);
            Destroy(other.gameObject);
            _spawnmanager.StartSpawning();
            Destroy(this.gameObject,0.25f);
        }
    }
    
}
