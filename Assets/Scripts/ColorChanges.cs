using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanges : MonoBehaviour
{
    float hue;
    float saturation;
    float value;

    private float _timer;
    private float _maxTimerChanges = 0.5f;

    private GameObject _circleObj;
    float GetCurrentHue;

    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        _circleObj = transform.root.gameObject;

        hue = 0f;
        saturation = 0.6f;
        value = 0.9f;
        rend = _circleObj.GetComponent<Renderer>();
        //rend.material.color = Random.ColorHSV(hue, 1f, 0.6f, 0.6f, 0.9f, 0.9f);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _maxTimerChanges)
        {
            if (Time.timeScale == 0) return;

            if(hue >= 360f)
            {
                hue = 0f;
                Debug.Log(hue);
            }

            hue += 0.02f;
            rend.material.color = Color.HSVToRGB(hue, saturation, value);

            _timer = 0;
        }

        
    }
}
