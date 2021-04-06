using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private GameObject _player;
    [Header("Bullet settings")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _pooledAmount = 5;
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] Transform firePlace;
    public List<GameObject> bulletPrefabs;
    private void Awake()
    {
        PoolBullets();
    }
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        AimToPlayer();
    }
    private void AimToPlayer()
    {
        Vector3 direction = _player.transform.position - transform.position;
        float rotateAngel = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(rotateAngel, Vector3.back);

        _rigidBody.rotation = Quaternion.Slerp(transform.rotation, rotation, 50 * Time.deltaTime);
        _rigidBody.freezeRotation = true;
    }
    //Animation Event (Animation clip)
    public void FireAttack()
    {
        if (_player != null && _rigidBody.velocity.x == 0 && _rigidBody.velocity.y == 0)
            for (int i = 0; i < bulletPrefabs.Count; i++)
            {
                if (!bulletPrefabs[i].activeInHierarchy)
                {
                    bulletPrefabs[i].transform.position = firePlace.transform.position;
                    bulletPrefabs[i].SetActive(true);
                    Vector3 moveDirection = (_player.transform.position - bulletPrefabs[i].transform.position).normalized * _bulletSpeed;
                    bulletPrefabs[i].GetComponent<Rigidbody>().velocity = moveDirection;
                    break;
                }
            }
    }
    private void PoolBullets()
    {
        bulletPrefabs = new List<GameObject>();
        for (int i = 0; i < _pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(_bulletPrefab);
            obj.SetActive(false);
            bulletPrefabs.Add(obj);
        }
    }
}
