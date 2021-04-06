using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] private int _rows = 8, _cols = 8;
    [SerializeField] private Color _deskColor1 = Color.white, _deskColor2 = Color.black;
    [SerializeField] GameObject[] _enemies;
    [SerializeField] GameObject _player;
    private void Start()
    {
        GenerateGameField();
        SpawnEnemies();
        SpawnPlayer();
    }
    private void GenerateGameField()
    {
        GameObject referenceGameFieldCube = (GameObject)Instantiate(Resources.Load("GameFieldCube"));
        Color[] colors = new Color[] { _deskColor1, _deskColor2 };
        
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                GameObject cube = (GameObject)Instantiate(referenceGameFieldCube, transform);
                cube.GetComponent<MeshRenderer>().material.color = (((col + row) % 2) == 0) ? _deskColor1 : _deskColor2;
                cube.transform.position = new Vector3(col, row, 0);
                //Generate borders
                if (cube.transform.position.x == 0|| cube.transform.position.x == _cols - 1|| cube.transform.position.y == 0|| cube.transform.position.y == _rows - 1)
                {
                    cube.GetComponent<MeshRenderer>().material.color = Color.black;
                    cube.GetComponent<BoxCollider>().size = new Vector3(1, 1, 2);
                }
                //Generate obsacles
                if (cube.transform.position.x == 4 && cube.transform.position.y >= 5 && cube.transform.position.y <= 7)
                {
                    cube.GetComponent<MeshRenderer>().material.color = Color.black;
                    cube.GetComponent<BoxCollider>().size = new Vector3(1, 1, 2);
                    cube.gameObject.tag = "Obstacle";
                }
                if (cube.transform.position.y == 7 && cube.transform.position.x >= 5 && cube.transform.position.x <= 7)
                {
                    cube.GetComponent<MeshRenderer>().material.color = Color.black;
                    cube.GetComponent<BoxCollider>().size = new Vector3(1, 1, 2);
                    cube.gameObject.tag = "Obstacle";
                }
                //Generate doors
                if (cube.transform.position.y == 15 && cube.transform.position.x >= 4 && cube.transform.position.x <= 6)
                {
                    cube.GetComponent<MeshRenderer>().material.color = Color.red;
                    cube.gameObject.tag = "Doors";
                }
            }
        }
        Destroy(referenceGameFieldCube);
    }
    private void SpawnEnemies()
    {
        for (int row = _rows/2; row < _rows-1; row++)
        {
            for (int col = 1; col < _cols-1; col++)
            {
                Vector3 spawnPosition = new Vector3( col, row, -1f);
                RandomEnemySpawn(spawnPosition, Quaternion.identity);
            }
        }
    }
    private void RandomEnemySpawn(Vector3 positionToSpawn, Quaternion rotatonToSpawn)
    {
        int randomIndex = Random.Range(0, _enemies.Length);
        GameObject clone = Instantiate(_enemies[randomIndex], positionToSpawn, rotatonToSpawn);
    }
    private void SpawnPlayer()
    {
        Vector3 spawnPosition = new Vector3(_cols / 2, _rows - _rows + 1, -1f);
        Instantiate(_player, spawnPosition, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                Gizmos.DrawWireCube(new Vector3(transform.position.x + col, transform.position.y + row, 0), new Vector3(1, 1, 1));
            }
        }
    }
    private void GenerateObstacles()
    {

    }
}
