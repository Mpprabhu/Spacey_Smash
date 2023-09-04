using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]     //0 - TripleShot | 1 - Speed | 2 - Sheild
    private int _powerupID;
    [SerializeField]
   private AudioClip _clip;
    void Update()
    {
        transform.Translate(Vector3.down*_speed*Time.deltaTime); 

        if(transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip,transform.position);
            if (player != null)
            {
                switch(_powerupID)
                {
                    case 0:{
                        player.TripleShotActive();
                    }
                    break;

                    case 1:{
                        player.SpeedActive();
                    }
                    break;

                    case 2:{
                        player.SheildActive();
                    }
                    break;

                    case 3:{
                        player.AmmoRefill();
                    }
                    break;

                    default:{
                        Debug.LogError("PowerUp ID not found!");
                    }
                    break;
                }
            }                 
            Destroy(this.gameObject);  
        }

    }
}