using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed=3f ;

    private Animator _anim;

    private Player _player;

    [SerializeField]
    private AudioClip _explosionSoundClip;
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _laserPrefab;
    private float _firerate = 3.0f;
    private float _canfire = -1;

    

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player==null)
        {
            Debug.LogError("PLAYER IS NULL...");
        }

        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.LogError("ANIMATOR IS NULL...");
        }

        _audioSource = GetComponent<AudioSource>();

        if(_audioSource == null)
        {
            Debug.LogError("THE AUDIO ON THE PLAYER IS NULL...");
        }
        else{
            _audioSource.clip = _explosionSoundClip;
        }
    }

    void Update()
    {
        enemymovement();

        if(Time.time > _canfire)
        {
            _firerate = Random.Range(3f,7f);
            _canfire = Time.time + _firerate;
            GameObject enemyLaser = Instantiate(_laserPrefab,transform.position,Quaternion.identity);
            laser[] lasers = enemyLaser.GetComponentsInChildren<laser>();
            for(int i = 0;i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }

        }
    }

    void enemymovement()
    {
        transform.Translate(Vector3.down*_speed*Time.deltaTime); 

        if(transform.position.y < -9f)
        {
            float randomx = Random.Range(-9f,9f);
            transform.position = new Vector3(randomx,7,0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
            player.Damage();       
            }                   
            _anim.SetTrigger("OnEnemyDeath"); 

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject,2f);  
            _audioSource.Play();
        }

        if(other.tag == "laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(Random.Range(5,12));
            }      
            _anim.SetTrigger("OnEnemyDeath");

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2f);  
            _audioSource.Play();      //enemy dead
        }
    }
}