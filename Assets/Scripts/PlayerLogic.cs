using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    private float _moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector2(0, 1) * _moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector2(0, -1) * _moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector2(-1, 0) * _moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector2(1, 0) * _moveSpeed * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            _moveSpeed = 4f;
            Debug.Log("movespeed has reduced");
        }
        else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            _moveSpeed = 4f;
            Debug.Log("movespeed has reduced");
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            _moveSpeed = 4f;
            Debug.Log("movespeed has reduced");
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            _moveSpeed = 4f;
            Debug.Log("movespeed has reduced");
        }
        else
        {
            _moveSpeed = 5f;
        }

    }

    private void OnTriggerEnter2D(Collider2D enemyTag)
    {

        if (enemyTag.tag == "Enemy")
        {
            Destroy(transform.root.gameObject);
        }

    }
}
