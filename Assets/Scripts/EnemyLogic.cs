using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject _summonCircle;

    private float speed = 5;
    private Transform target;
    private float _healthPoint = 100f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerStay2D(Collider2D circle)
    {
        if (circle.tag == "AmplitudeCircle")
        {
            _healthPoint -= 5;
        }

        if(_healthPoint <= 0)
        {
            Destroy(transform.root.gameObject);
            GlobalData.Instance.Score += 50;
            //Debug.Log(GlobalData.Instance.Score);
            GameObject _circle = GameObject.Instantiate(_summonCircle, this.transform.position, this.transform.rotation);
            _circle.AddComponent<CircleLogic>();
        }

    }

}
