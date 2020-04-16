using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplaySceneController : MonoBehaviour
{

    [SerializeField]
    GameObject _circle;

    [SerializeField]
    private Text _currentDifficult;

    [SerializeField]
    private Text _textTimer;

    private float _timer;
    private float _maxTimer = 1f;
    private float _z;

    private float _timerSong;
    private float _lengthSong;
    private float _currentTime;
    
    private float speed = 50f;
    private float _maxScale = 15;
    private bool isGamePlaying = false;

    private int _counterDifficult = 1;


    private GameObject _eightFreqObj;
    private GameObject _spawnController;
    private GameObject _panelMainMenu;
    private GameObject _panelGameOver;
    private GameObject _panelWin;
    private GameObject _audioSourceObj;
    private GameObject _player;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _eightFreqObj = GameObject.FindGameObjectWithTag("EightFreq");
        _spawnController = GameObject.FindGameObjectWithTag("SpawnController");
        _panelMainMenu = GameObject.FindGameObjectWithTag("PanelMainMenu");
        _panelGameOver = GameObject.FindGameObjectWithTag("PanelGameOver");
        _panelWin = GameObject.FindGameObjectWithTag("PanelWin");
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioSourceObj = GameObject.FindGameObjectWithTag("AudioSource");

        _audioSource = _audioSourceObj.GetComponent<AudioSource>();

        _spawnController.SetActive(false);
        _player.SetActive(false);
        _panelGameOver.SetActive(false);
        _panelWin.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGamePlaying)
        {
            _timerSong += Time.deltaTime;
            _currentTime = _lengthSong - _timerSong;

            if(_currentTime <= 0)
            {
                _currentTime = 0;
            }

            _timer += Time.deltaTime;

            if (_timer < _maxTimer)
            {
                _z += 0.5f;
                if(_eightFreqObj != null)
                {
                    _eightFreqObj.transform.eulerAngles = new Vector3(0, 0, _z);
                }
            }
            else
            {
                _timer = 0;
            }
            
            if(_circle != null)
            {
                _circle.transform.localScale = new Vector2(AudioPeer._amplitude * _maxScale, AudioPeer._amplitude * _maxScale);
            }

            if (_player == null)
            {
                GameOver();
                isGamePlaying = false;
            }

            if (!_audioSource.isPlaying)
            {
                Win();
                isGamePlaying = false;
            }

            UpdateTimeLeftText();
        }
        
    }

    private void UpdateTimeLeftText()
    {
        string minutes = Mathf.Floor(_currentTime / 60).ToString("00");
        string seconds = Mathf.Floor(_currentTime % 60).ToString("00");

        _textTimer.text = minutes + " : " + seconds;
    }

    private void Win()
    {
        DestroyAll("Enemy");
        _panelWin.SetActive(true);
        _spawnController.SetActive(false);
    }

    private void GameOver()
    {
        //Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        DestroyAll("Enemy");
        _panelGameOver.SetActive(true);
        _spawnController.SetActive(false);
    }

    public void StartGame()
    {
        _lengthSong = _audioSource.clip.length;
        isGamePlaying = true;
        _audioSource.Play();
        _player.SetActive(true);
        _panelMainMenu.SetActive(false);
        _spawnController.SetActive(true);
        _spawnController.GetComponent<SpawnController>()._currentDifficult = _counterDifficult;
    }

    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void BtnPrevPressed()
    {
        _counterDifficult--;
        if(_counterDifficult <= 0)
        {
            _counterDifficult = 2;
        }
        _currentDifficult.text = "" + _counterDifficult;
    }

    public void BtnNextPressed()
    {
        _counterDifficult++;

        if (_counterDifficult >= 3)
        {
            _counterDifficult = 1;
        }

        _currentDifficult.text = "" + _counterDifficult;
    }

    void DestroyAll(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }


}
