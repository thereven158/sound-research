using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private float _timer;
    private float _spawnTimer = 1f;
    public int _currentDifficult = 0;

    public GameObject[] _enemyObj = new GameObject[10];

    string[] whereSpawn = { "Top", "Bottom", "Right", "Left" };

    private GameObject _playerObj;
    private Vector2 _playerPos;

    private float _randomX;
    private float _randomY;

    // Start is called before the first frame update
    void Start()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnTimer)
        {
            if(_playerObj != null)
            {
                _playerPos = _playerObj.transform.position;
                WhereSpawn();
            }            

            _timer = 0;
        }
    }

    private void SpawnEnemy(float minX, float maxX, float minY, float maxY)
    {


        _randomX = Random.Range(minX, maxX);
        _randomY = Random.Range(minY, maxY);

        var randomEnemy = Random.Range(0, _currentDifficult);
        GameObject newEnemy = GameObject.Instantiate(_enemyObj[randomEnemy]);
        newEnemy.transform.localPosition = new Vector2(_randomX, _randomY);
    }

    private void WhereSpawn()
    {
        var minX = 0f;
        var maxX = 0f;
        var minY = 0f;
        var maxY = 0f;

        var random = Random.Range(0, 3);
        var where = whereSpawn[random];

        if(where == "Top")
        {
            minX = _playerPos.x - 20f;
            maxX = _playerPos.x + 20f;
            minY = _playerPos.y + 3f;
            maxY = _playerPos.y + 10f;
        }
        else if (where == "Bottom")
        {
            minX = _playerPos.x - 20f;
            maxX = _playerPos.x + 20f;
            minY = _playerPos.y - 10f;
            maxY = _playerPos.y - 3f;
        }
        else if (where == "Right")
        {
            minX = _playerPos.x + 3f;
            maxX = _playerPos.x + 20f;
            minY = _playerPos.y - 10f;
            maxY = _playerPos.y + 10f;
        }
        else if (where == "Left")
        {
            minX = _playerPos.x - 3f;
            maxX = _playerPos.x - 20f;
            minY = _playerPos.y - 10f;
            maxY = _playerPos.y + 10f;

        }

        SpawnEnemy(minX, maxX, minY, maxY);
    }
}
