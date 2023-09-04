using System.Collections;
using System.Collections.Generic;  //libraries to code 
using UnityEngine;                // used to access Monobehaviour
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _speedmultiplier = 3f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _firerate = 0.15f;
    private float _canfire = -1;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnmanager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private bool _SpeedActive = false;
    [SerializeField]
    private bool _SheildActive = false;
    [SerializeField]
    private GameObject _SheildVisualizer;
    [SerializeField]
    private int _score;
    [SerializeField]
    private Text _powerlost;
    [SerializeField]
    private GameObject[] _engine;
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;
    [SerializeField]
    private int _magazine = 15;
    // private int _ammoCount;
    private UImanager _uimanager;




    void Start()        //start of the game 
    {
        transform.position = new Vector3(0,0,0);
        _spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uimanager = GameObject.Find("Canvas").GetComponent<UImanager>();
        _audioSource = GetComponent<AudioSource>();



        if(_spawnmanager == null)
        {
            Debug.LogError("THE SPAWN MANAGER IS NULL...");
        }

        if(_uimanager == null)
        {
            Debug.LogError("THE UI MANAGER IS NULL...");
        }
        if(_audioSource == null)
        {
            Debug.LogError("THE AUDIO ON THE PLAYER IS NULL...");
        }
        else{
            _audioSource.clip = _laserSoundClip;
        }
    }

    void Update()       // game loop
    {
        movement();
        lasershot();
    }

    void movement()
    {
        //PLAYER MOVEMENT

        float input_h = Input.GetAxis("Horizontal");
        float input_v = Input.GetAxis("Vertical");
    
        Vector3 dir = new Vector3(input_h,input_v,0);

        if(_SpeedActive == true)
        {
            transform.Translate(dir*_speed*_speedmultiplier*Time.deltaTime); 
        }
        else
        {
            transform.Translate(dir*_speed*Time.deltaTime); 
        }

        //PLAYER BOUNDS


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.8f,0) ,0); 

        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-9f,9f),transform.position.y ,0);


    }
    void lasershot() //LASER
    {
         if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)        
        {
            _canfire = Time.time + _firerate;
            if(_isTripleShotActive == true && _magazine > 0)
            {
                Instantiate(_TripleShotPrefab,transform.position,Quaternion.identity); 
            }
            else if(_isTripleShotActive == false && _magazine > 0)
            {
                Instantiate(_laserPrefab,transform.position + new Vector3(0,1.15f,0),Quaternion.identity);              
            }
          if(_magazine <= 0)
        {
            return;
        }
            _audioSource.Play();
            _magazine--;
            _uimanager.UpdateAmmo(_magazine);
        }
    }
        

    public void Damage()    
    { 
        if(_SheildActive == true)
        {
                _SheildActive = false;
                _SheildVisualizer.SetActive(false);
                return;
        }
        _lives--;

        _uimanager.UpdateLives(_lives);

        if(_lives == 2)
        {
            _engine[0].SetActive(true);
        }

        else if(_lives == 1)
        {
            _engine[1].SetActive(true);
        }

        else if(_lives <= 0)
        {
            _spawnmanager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotRoutine());
    }

    IEnumerator TripleShotRoutine()
    {
        if(_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(15.0f);
            _isTripleShotActive = false;
            _powerlost.text = "POWER LOST";
            _powerlost.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            _powerlost.gameObject.SetActive(false);
        }
    }

    public void SpeedActive()
    {
        _SpeedActive = true;
        StartCoroutine(SpeedRoutine());
    }

    IEnumerator SpeedRoutine()
    {
        if(_SpeedActive == true)
        {
            yield return new WaitForSeconds(15.0f);
            _SpeedActive = false;
            _powerlost.text = "POWER LOST";   
            _powerlost.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            _powerlost.gameObject.SetActive(false);
        }
    }

    public void SheildActive()
    {
        _SheildActive = true;
        _SheildVisualizer.SetActive(true);
        StartCoroutine(ShieldRoutine());
    }

    IEnumerator ShieldRoutine()
    {
        if(_SheildActive == true)
        {
            yield return new WaitForSeconds(15.0f);
            _SheildActive = false;
            _SheildVisualizer.SetActive(false);
            _powerlost.text = "POWER LOST";
            _powerlost.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            _powerlost.gameObject.SetActive(false);
        }
    }

    public void AmmoRefill()
    {
        _magazine = 15;
        _uimanager.UpdateAmmo(_magazine);
    }

    public void AddScore(int _points)
    {
        _score+=_points;
        _uimanager.UpdateScore(_score);        

    }

}


