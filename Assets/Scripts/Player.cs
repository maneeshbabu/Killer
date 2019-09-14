using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _defaultSpeed = 5.0f;
    [SerializeField]
    private Transform _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private float _life = 3;
    [SerializeField]
    private Transform _tripleLaserPrefab;
    [SerializeField]
    private Transform _thrustPrefab;
    [SerializeField]
    private GameObject _sheildView;

    private bool _isTripleShotEnabled = false;
    private bool _isSheildEnabled = false;
    private bool _isSpeedEnabled = false;

    private float _speed = 5.0f;
    private float _canFire = -1f;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // take current position to user
        transform.position = new Vector3(0, 0, 0);
        _sheildView.SetActive(false);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.Log("No Spawn manager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    // MovePlayer is called to move the player
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (_isSpeedEnabled)
        {
            _speed = 2 * _defaultSpeed;
        }
        else
        {
            _speed = _defaultSpeed;
        }

        if (horizontalInput != 0.0f || verticalInput != 0.0f)
        {
            Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
   
        _canFire = Time.time + _fireRate;
        if (_isTripleShotEnabled) {
            Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0f, 0.8f, 0f), Quaternion.identity);
        }
    }

    void ShowThruster()
    {

    }

    public void Damage()
    {
        if (_isSheildEnabled == true) {
            _isSheildEnabled = false;
            _sheildView.SetActive(false);
            return;
        }

        _life -= 1;

        if (_life < 1)
        {
            _spawnManager.PlayerDied();
            Destroy(this.gameObject);
        }
    }

    public void PowerUp(int type)
    {
        if (type == 0) {
            _isTripleShotEnabled = true;
        } else if (type == 1)
        {
            _isSpeedEnabled = true;
        }else if (type == 2)
        {
            _sheildView.SetActive(true);
            _isSheildEnabled = true;
        }

        StartCoroutine(PowerDownTripleShotRoutine());
    }

    IEnumerator PowerDownTripleShotRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotEnabled = false;
        _isSpeedEnabled = false;
        _isSheildEnabled = false;
        _sheildView.SetActive(false);
    }
}
