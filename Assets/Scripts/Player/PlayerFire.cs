using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private Animator _animator;
    private Collider[] _hitObjects;
    [Header("Aim settings")]
    [SerializeField] private float _impactField =8f;
    [SerializeField] private LayerMask _whatToHit;
    private Collider closestEnemy;
    [Header("Bullet settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int pooledAmount = 5;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] Transform firePlace;
    public List<GameObject> bulletPrefabs;
    private void Awake()
    {
        PoolBullets();
    }
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        AimToClosestEnemy();
    }
    private void AimToClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        _hitObjects = Physics.OverlapSphere(transform.position, _impactField, _whatToHit);
        closestEnemy = null;
        foreach (Collider currentEnemy in _hitObjects)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
            Vector3 direction = closestEnemy.transform.position - transform.position;
            float rotateAngel = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(rotateAngel, Vector3.back);

            _rigidBody.rotation = Quaternion.Slerp(transform.rotation, rotation, 50 * Time.deltaTime);
            _rigidBody.freezeRotation = true;

            _animator.SetFloat("Speed", _rigidBody.velocity.x);
        }
        //Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _impactField);
    }
    //Animation Event (Animation clip)
    public void FireAttack()
    {
        if (closestEnemy != null && _rigidBody.velocity.x == 0 && _rigidBody.velocity.y == 0)
            for (int i = 0; i < bulletPrefabs.Count; i++)
            {
                if (!bulletPrefabs[i].activeInHierarchy)
                {
                    bulletPrefabs[i].transform.position = firePlace.transform.position; ;
                    bulletPrefabs[i].SetActive(true);
                    Vector3 moveDirection = (closestEnemy.transform.position - bulletPrefabs[i].transform.position).normalized * bulletSpeed;
                    bulletPrefabs[i].GetComponent<Rigidbody>().velocity = moveDirection;
                    break;
                }
            }
    }
    private void PoolBullets()
    {
        bulletPrefabs = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(bulletPrefab);
            obj.SetActive(false);
            bulletPrefabs.Add(obj);
        }
    }
}
