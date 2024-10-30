using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class PlayerPhysic : MonoBehaviour
    {
        private Rigidbody body;
        private Collider col;
        private int forward;
        private int sideways;
        private bool jump;
        private GameObject lastPlatform;

    public Transform spawnPoint;
    public static Text playerStats;
    public CameraFollowA camerai;
    public Boulder Boulder;
    public GameObject Boulder_obj;
    private bool run = true;
    private bool invincible = false;


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
        public Transform lastSpawn;

    private void Awake()
        {
            lastPlatform = null;
        }
        // Start is called before the first frame update
        void Start()
        {
            body = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            direction = transform.rotation;

        Spawn();
        playerStats = GameObject.Find("PlayerStats").GetComponent<Text>();
        UpdateStats();
    }

    public static void UpdateStats()
    {
        playerStats.text = "Lives " + GameData.lives.ToString();

    }
    IEnumerator Dead()
    {
        if(GameData.lives != 0)
        {
            camerai.lever = 1;
            Boulder_obj.SetActive(false);
            Boulder.SetPosition();
            Spawn();
            yield return 0;
        }
        else
        {
            SceneManager.LoadScene("DeathScene");
        }
    }

    IEnumerator DeadBoulder()
    {
        if (GameData.lives != 0)
        {
            yield return new WaitForSeconds(0.3f);
            transform.localScale = new Vector3(1f, 0.3f, 1f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z);
            yield return new WaitForSeconds(1);
            transform.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z);
            camerai.lever = 1;
            Boulder.SetPosition();
            Boulder_obj.SetActive(false);
            Spawn();
            yield return 0;
        }
        else
        {
            SceneManager.LoadScene("DeathScene");
        }
    }
    public void Spawn()
    {
        transform.position = lastSpawn.position;
        body.velocity = Vector3.zero;
        invincible = false;
        run = true;
    }
    private void FixedUpdate()
        {
        if (run == true)
        {
            //Gravitational force
            body.AddForce(0, gravity, 0);
            reduceSpeed();
        }
        }

        private void Update()
        {
        if (run == true)
        {


            getMoveInput();
            getJumpInput();
            applyMovement();
            applyJump();
            reduceHorizontalSpeed();
        }
    }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject other = collision.gameObject;
            if (other.tag == "colorPlatform" && other != lastPlatform)
            {
                lastPlatform = other;
                other.GetComponent<PlatformColor>().toggleMaterial();
            }
        }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !invincible)
        {
            GameData.lives -= 1;
            UpdateStats();
            StartCoroutine(Dead());
        }
        //mit der Deathzone
        if (other.gameObject.tag ==
        "DeathZone")
        {
            GameData.lives -= 1;
            UpdateStats();
            StartCoroutine(Dead());
        }


        if (other.gameObject.tag == "Boulder" && !invincible)
        {
            run = false;
            invincible = true;
            //ani
            GameData.lives -= 1;
            UpdateStats();
            StartCoroutine(DeadBoulder());
        }

        if (other.gameObject.tag == "Respawn")
            checkPoint(other.transform);
    }

    public void checkPoint(Transform point)
    {
         if (lastSpawn != point)
         {
            Destroy(lastSpawn.gameObject);
            lastSpawn = point;
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
            Vector3 vector = transform.TransformDirection(acceleration * sideways, 0, acceleration * forward);
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
                sideways++;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                sideways--;
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
