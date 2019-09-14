using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private Powerups _powerUpType = Powerups.TRIPLE_SHOT;

    public enum Powerups { TRIPLE_SHOT, SPEED, SHEILD};

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 7.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
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
                switch (_powerUpType)
                {
                    case Powerups.SPEED:
                        {
                            Debug.Log("HIT SPEED POWER UP");
                            player.PowerUp(1);
                        }
                        break;
                    default:
                        {
                            player.PowerUp(0);
                        }
                        break;
                } 
            
            }
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            //Destroy(other.gameObject);
        }
        else if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
