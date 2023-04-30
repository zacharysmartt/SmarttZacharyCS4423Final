using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerNew : MonoBehaviour
{

    Rigidbody2D rb2d;

    public SpriteRenderer sr;
    public Sprite normalSprite;
    public Sprite speedSprite;

    [Header("Movement")]
    public float speed = 25f;
    bool isDashing;

    [Header("Capacity")]
    public int capacity = 0;
    public Text capacityText;

    [Header("Money")]
    public int money;
    public Text moneyText;

    [Header("Area Detection")]
    public bool withinSellZone;
    public bool withinAddTree;
    public int withinAddCap;

    [Header("Audio")]
    public AudioClip moneyAudio;
    public AudioClip walkAudio;

    public GameObject fist;
    public GameObject tree;
    public Text warningText;

    private TreeSpot[] TreeSpots;
    public bool spawningTree = false;

    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        if (TreeSpots == null) {
            TreeSpots = FindObjectsOfType<TreeSpot>();
            Debug.Log("Number of Tree Spots in current scene: " + TreeSpots.Length);
        }
    }

    void Start()
    {
        money = 100;
        updateCapacityText();
        updateMoneyText();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space)) {
            Punch();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (withinSellZone) {
                if (money >= 0 && CapacityLimit.capacity > 0) {
                    AddMoney(CapacityLimit.capacity * 10);
                    CapacityLimit.capacity = 0;
                    GetComponent<AudioSource>().PlayOneShot(moneyAudio);
                    updateCapacityText();
                    updateMoneyText();
                }    
            }
            else if (withinAddTree) {
                if (money >= 50 && TreePool.instance.treeCount < TreePool.instance.treeLimit) {
                    spawningTree = true;
                    money -= 50;
                    updateMoneyText();
                    AddTree();
                }
            }
            else if (withinAddCap > 0) {
                if (money >= 100) {
                    money -= 100;
                    updateMoneyText();
                    updateCapacityText();
                    CapacityLimit.capacityLimit += 10;
                }
            }
        }
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            isDashing = true;
        }
        else {
            isDashing = false;
        }
        Move(isDashing);
    }

    private void Move(bool dash) {
        float speed;
        if (dash == true) {
            speed = this.speed * 2;
        }
        else {
            speed = this.speed;
        }
        rb2d.MovePosition(transform.position + (new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")) * Time.fixedDeltaTime * speed));
        //playWalkAudio();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag.Contains("Fruit")) {
            if (other.tag.Contains("Money")) {
                AddMoney(200);
                GetComponent<AudioSource>().PlayOneShot(moneyAudio);
                updateMoneyText();
            }
            else if (other.tag.Contains("Speed")) {
                AddSpeed();
            }
            else {
                if(CapacityLimit.capacity < CapacityLimit.capacityLimit) {
                    updateCapacityText();
                }
            }
        }
        if(other.tag == "Sell") {
            withinSellZone = true;
        }
        if(other.tag == "AddTree") {
            withinAddTree = true;
        }
        if(other.tag == "AddCapacity") {
            withinAddCap++;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Sell") {
            withinSellZone = false;
        }
        if(other.tag == "AddTree") {
            withinAddTree = false;
        }
        if(other.tag == "AddCapacity") {
            withinAddCap--;
        }
    }

    void AddMoney(int amount) {
        money += amount;
    }

    void AddSpeed() {
        StartCoroutine(SpeedBoost());
        IEnumerator SpeedBoost() {
            speed *= 2;
            sr.sprite = speedSprite;
            yield return new WaitForSeconds(10f);
            speed = speed / 2;
            sr.sprite = normalSprite;
        }
    }

    void AddTree() {
        for (int i = 0; i < TreeSpots.Length; i++) {
            if (!(TreeSpots[i].hasTree)) {
                TreeSpots[i].GetTree();
                break;
            }
        }
    }

    void updateCapacityText() {
        capacityText.text = CapacityLimit.capacity.ToString() + " / " + CapacityLimit.capacityLimit.ToString();
    }

    void updateMoneyText() {
        moneyText.text = "$" + money.ToString();
    }

    private void Punch() {
        GameObject punch;
        Vector2 FistPosition = new Vector2((transform.position.x + 0.5f),(transform.position.y));
        punch = Instantiate(fist, FistPosition, Quaternion.identity);
        Destroy(punch, 0.2f);
    }

    void playWalkAudio() {
        StartCoroutine(WalkAudioRoutine());
        IEnumerator WalkAudioRoutine() {
            GetComponent<AudioSource>().PlayOneShot(walkAudio);
            yield return new WaitForSeconds(1f);
        }
    }
}
