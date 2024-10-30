using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerPhysicMa : MonoBehaviour
{
    private AllPlatforms level;
    private Rigidbody body;
    private Collider col;
    private int forward;
    private int sideways;
    private bool jump;
    private bool run;
    public static bool onStart;
    private GameObject lastPlatform;

    private Quaternion direction;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float globalMaxSpeed;
    [SerializeField]
    private float maxHorizontalSpeed;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float bremsFaktor;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Transform lastSpawn;
    private void Awake()
    {
        lastPlatform = null;
        run = false;
        onStart = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        GameData.initialize();
        StartCoroutine(spawning());
        direction = transform.rotation;
        level = GameObject.Find("AllPlatforms").GetComponent<AllPlatforms>();
    }
    private void FixedUpdate()
    {
        //Gravitational force
        body.AddForce(0, gravity, 0);
        reduceSpeed();
    }

    private void Update()
    {
        if (run)
        {
            getMoveInput();
            getJumpInput();
            applyMovement();
            applyJump();
        }
        turn();
        reduceHorizontalSpeed();
        levelFinished();
    }

    private void LateUpdate()
    {
        //Einfacher als eine Death zone
        if(transform.position.y < 0)
        {
            run = false;
            GameData.die();
            spawn();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "startArea")
        {
            onStart = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "startArea")
        {
            onStart = false;
        }
    }

    private void levelFinished()
    {
        if(onStart && PlatformColor.allActivated)
        {
            GameData.loadNextScene();
        }
        else if(GameData.lives < 1)
        {
            level.generateRandomLevel();
            GameData.initialize();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag=="colorPlatform" && other != lastPlatform)
        {
            lastPlatform = other;
            other.GetComponent<PlatformColor>().toggleMaterial();
        } else if(other.tag=="breakingPlatform")
        {
            other.GetComponent<BreakingPlatform>().shrink();
        }
    }

    private void spawn()
    {
        transform.position = lastSpawn.position;
        body.velocity = Vector3.zero;
        run = false;
        StartCoroutine(spawning());
    }

    private IEnumerator lockedInfo(int time)
    {
        System.DateTime startTime = System.DateTime.Now;
        while (!run)
        {
            System.DateTime now = System.DateTime.Now;
            double diff = now.Subtract(startTime).TotalSeconds;
            double left = (double)time - diff;
            GameData.locked(left);
            yield return null;
        }
    }

    private IEnumerator spawning()
    {
        StartCoroutine(lockedInfo(2));
        yield return new WaitForSeconds(2);
        run = GameData.lives > 0;
    }

    private void getJumpInput()
    {
        jump = Input.GetKeyDown(KeyCode.Space);
    }

    private void applyJump()
    {
        if(jump && grounded())
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
        if(speed > maxHorizontalSpeed)
        {
            float reduceFactor = maxHorizontalSpeed / speed;
            body.velocity = new Vector3(body.velocity.x * reduceFactor, body.velocity.y, body.velocity.z * reduceFactor);
        }
    }

    private void turn()
    {
        float turn = Input.GetAxis("Mouse X");
        direction *= Quaternion.AngleAxis(turn * turnSpeed * Time.deltaTime, Vector3.up);
        transform.rotation = direction;
    }

    private void applyMovement()
    {
        Vector3 vector = transform.TransformDirection(acceleration * forward, 0, acceleration * sideways);
        body.AddForce(vector, ForceMode.Force);
        if(grounded())
        {
            //bremst den Spieler ab
            float x = body.velocity.x;
            float z = body.velocity.z;
            if(forward == 0)
            {
                x = x * bremsFaktor;
            }
            if(sideways == 0)
            {
                z = z * bremsFaktor;
            }
            body.velocity = new Vector3(x, body.velocity.y, z);
        }
    }

    private void getMoveInput()
    {
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
}
