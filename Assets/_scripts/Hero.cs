using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{

    [Header("Set In Inspector")]
    public GameObject targetPrefab;
    public float moveTime;
    static public int heroMaxHP = 10;

    [Header("Set Dynamically")]
    static public Vector3 mouse_pos;
    static public int HeroCurrHP = 3;
    public Transform target;
    public Vector3 object_pos;
    public float clickTime;
    static public float timer = 1f;
    public GameObject targetFind;
    public float moveVelocity;
    static public bool goalMet = false;
    Coub mainScript;
    public Vector3 moveRight;

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HeroCurrHP = 3;
        mainScript = Camera.main.GetComponent<Coub>();
        targetFind = GameObject.Find("target");
        if (targetFind == null)
        {
            GameObject targetSpawn = Instantiate<GameObject>(targetPrefab);
            targetSpawn.transform.position = new Vector3(pos.x,pos.y,pos.z-1);
            targetSpawn.transform.parent = this.transform;
        }
        target = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        //angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        //Vector3 rotVect = new Vector3(0, 0, angle);
        //transform.rotation = Quaternion.Euler(rotVect);

        moveVelocity = 4f * timer * Time.deltaTime;

        if (Time.time - clickTime >= 1f)
        {
            timer = 1f;
        }
        else
        {
            timer = -(clickTime - Time.time);
        }

        if (Input.GetMouseButton(0) && !timerScript.gameEnd)// && Time.time - clickTime >= 1f)
        {
            Vector3 p0 = pos;
            clickTime = Time.time;
            pos = (1 - moveVelocity) * p0 + moveVelocity * mouse_pos;
        }
    }
    void FixedUpdate()
    {
        if (border.pointRight)
        {
            moveRight = new Vector3(0.003f * mainScript.levelNumber, 0f, 0f);
            transform.position += moveRight;
        }
        if (border.pointLeft)
        {
            transform.position -= moveRight;
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collidedWith = coll.gameObject;
        switch (collidedWith.tag)
        {
            case "Goal":
                goalMet = true;
                break;
            case "Border":
                transform.position = new Vector3(0f, 0f, 0f);
                HeroCurrHP -= 1;
                Debug.Log("hero hp: " + HeroCurrHP);
                break;
        }
    }
}

