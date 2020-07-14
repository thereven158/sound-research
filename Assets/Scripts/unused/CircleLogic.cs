using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLogic : MonoBehaviour
{
    Vector3 newScale;
    private float _maxScale = 5;
    private float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        newScale = new Vector3(AudioPeer._amplitude * _maxScale, AudioPeer._amplitude * _maxScale, 0);
        this.transform.localScale = new Vector3(0, 0, 0);

        StartCoroutine(SelfDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = Vector3.Lerp(transform.localScale, newScale, speed * Time.deltaTime);
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(transform.root.gameObject);
    }
}
