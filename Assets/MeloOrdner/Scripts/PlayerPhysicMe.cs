using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerPhysicMe : MonoBehaviour
{
    private Rigidbody body;
    private Collider col;
    private int forward;
    private int sideways;
    private bool jump;
    private bool grab;
    private Quaternion direction;
    [SerializeField]
    private List<Waypoint> wayPoints;

    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float globalMaxSpeed;
    [SerializeField]
    private float maxHorizontalSpeed = 60;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float bremsFaktor;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    public Transform spawnPoint;
    [SerializeField]
    public Transform spawnPoint2;


    int check = 0;

    public static Text coins;
    [SerializeField]
    GameObject boss;

    GameObject[] arr = new GameObject[5];
    Vector3[] arr2 = new Vector3[5];
    public void Spawn()
    {
        if(check==0)
        {
            transform.position = spawnPoint.position;
        }
        else if(check==1)
        {
            transform.position = spawnPoint2.position;

        }
      
    }

    void Start()
    {
        body = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        direction = transform.rotation;
        Spawn();
        coins = GameObject.Find("Money").GetComponent<Text>();
        GameData.lives = 3;
        updateCoins();

        arr[0] = GameObject.Find("Bomb_Red");
        arr[1] = GameObject.Find("Bomb_Red1");
        arr[2] = GameObject.Find("Bomb_Red2");
        arr[3] = GameObject.Find("Bomb_Red3");
        arr[4] = GameObject.Find("Bomb_Red4");

        for(int i=0;i<arr.Length;i++)
        {
            arr2[i] = arr[i].transform.position;
        }
    }

    private void updateCoins()
    {
        coins.text = "Lives: " + GameData.lives.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag ==
        "DeathZone")
        {
            Spawn();
            GameData.lives--;
        }
        if (other.gameObject.tag == "SpawnPoint2")
        {
            check++;
        }
        if(other.gameObject.tag=="Fire")
        {
            GameData.lives--;
        }
        if(other.gameObject.tag=="Boss")
        {
            GameData.lives--;
        }
        if(other.gameObject.tag=="Boss2")
        {
            Instantiate(boss, new Vector3(7.352182f, 9.134621f, -0.4905853f), Quaternion.identity);
            Destroy(other);
        }

    }

    public List<Waypoint> GetWaypoints()
    {
        return wayPoints;
    }

    private void FixedUpdate()
    {
        //Gravitational force
        body.AddForce(0, gravity, 0);
        reduceSpeed();
    }

    private void Update()
    {
        getMoveInput();
        getJumpInput();
        applyMovement();
        applyJump();
        updateCoins();
        grabObject();
        lives();
        reduceHorizontalSpeed();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            Collider col = other.gameObject.GetComponent<Collider>();
            Collider mycol = this.gameObject.GetComponent<Collider>();
            if (enemy.invincible)
            {
                OnDeath();
            }
            else if (mycol.bounds.center.y - mycol.bounds.extents.y >
            col.bounds.center.y + 0.5f * col.bounds.extents.y)
            {
                GameData.score += 10;
                JumpedOnEnemy(enemy.bumpSpeed);
                enemy.OnDeath();
            }
            else
            {
                OnDeath();
            }
        }
        if(other.tag=="Boss")
        {
            GameData.lives--;
        }
    }

    private void getJumpInput()
    {
        jump = Input.GetKeyDown(KeyCode.Space);
    }

    private void applyJump()
    {
        if (jump && grounded())
            body.AddForce(0, jumpForce, 0, ForceMode.Impulse);
    }

    private bool grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);
    }

    private void reduceHorizontalSpeed()
    {
        float speed = Mathf.Abs(body.velocity.x);
        speed += Mathf.Abs(body.velocity.z);
        if (speed > maxHorizontalSpeed)
        {
            float reduceFactor = maxHorizontalSpeed / speed;
            body.velocity = new Vector3(body.velocity.x * reduceFactor, body.velocity.y, body.velocity.z * reduceFactor);
        }
    }

    private void applyMovement()
    {
        Vector3 vector = transform.TransformDirection(acceleration * forward, 0, acceleration * sideways);
        body.AddForce(vector, ForceMode.Force);
        if (grounded())
        {
            //bremst den Spieler ab
            float x = body.velocity.x;
            float z = body.velocity.z;
            if (forward == 0)
            {
                x = x * bremsFaktor;
            }
            if (sideways == 0)
            {
                z = z * bremsFaktor;
            }
            body.velocity = new Vector3(x, body.velocity.y, z);
        }
    }

    private void getMoveInput()
    {
        float turn = Input.GetAxis("Mouse X");
        direction *= Quaternion.AngleAxis(turn * turnSpeed * Time.deltaTime, Vector3.up);
        transform.rotation = direction;

        forward = 0;
        sideways = 0;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            forward++;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            forward--;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            sideways--;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            sideways++;
        }
        grab = Input.GetMouseButton(0);
    }

    private void reduceSpeed()
    {
        float speed = Mathf.Abs(body.velocity.x);
        speed += Mathf.Abs(body.velocity.y);
        speed += Mathf.Abs(body.velocity.z);
        if (speed > globalMaxSpeed)
        {
            float reduceFactor = globalMaxSpeed / speed;
            body.velocity = new Vector3(body.velocity.x * reduceFactor, body.velocity.y * reduceFactor, body.velocity.z * reduceFactor);
        }
    }
 
    void OnDeath()
    {
        Spawn();
    }
    void JumpedOnEnemy(float bumpSpeed)
    {
        body.velocity = new Vector3(body.velocity.x, bumpSpeed, body.velocity.z);
    }

    void grabObject()
    {
       
            for (int i = 0; i < arr.Length; i++)
            {
                if (Vector3.Distance(transform.position, arr[i].transform.position) < 10f && grab == true)
                {
                    arr[i].transform.position =
                    new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
                    return;
                }
            }
    }

    public void bombSpawn(GameObject b)
    {
        
        for(int i=0;i<arr.Length;i++)
        {
            if (arr[i]==b)
            {
                b.transform.position = new Vector3(100, 100, 100);
                b.transform.position = new Vector3(arr2[i].x, arr2[i].y + 200, arr2[i].z);
                b.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
    }

    private void lives()
    {
        if (GameData.lives == 0)
        {
            SceneManager.LoadScene("DeathScene");
        }
    }
}
