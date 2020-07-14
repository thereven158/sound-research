using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySplitting : MonoBehaviour
{

    [SerializeField]
    private GameObject _summonCircle;

    private float speed = 5;
    private Transform target;
    private float _healthPoint = 100f;
    private float _timer;
    private float _timeSplit = 5f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        _timer += Time.deltaTime;

        if (_timer > _timeSplit)
        {
            Vector3 currentPos = this.transform.localPosition;
            Vector3 newPos = new Vector3(currentPos.x, currentPos.y + 3f, currentPos.z);

            GameObject anotherSplit = GameObject.Instantiate(transform.root.gameObject, transform.position + this.transform.forward, transform.rotation);
            anotherSplit.GetComponent<Rigidbody2D>().AddForce(transform.position + this.transform.forward);

            _timer = 0;
        }

        if (target != null)
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

        if (_healthPoint <= 0)
        {
            Destroy(transform.root.gameObject);
            GlobalData.Instance.Score += 50;
            //Debug.Log(GlobalData.Instance.Score);
            GameObject _circle = GameObject.Instantiate(_summonCircle, this.transform.position, this.transform.rotation);
            _circle.AddComponent<CircleLogic>();
        }

    }

}
