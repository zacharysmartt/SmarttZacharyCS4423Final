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
    public float speed = 2f;
    public float defaultSpeed = 2f;
    public float powerSpeed = 5f;
    bool isDashing;

    [Header("Capacity")]
    public Text capacityText;

    [Header("Money")]
    public Text moneyText;

    [Header("Area Detection")]
    public bool withinSellZone;
    public bool withinAddTree;
    public bool withinAddCap;
    public bool withinLevelExit;

    [Header("Audio")]
    public AudioClip collectAudio;
    public AudioClip moneyAudio;
    public AudioClip walkAudio;
    public AudioClip speedAudio;
    public AudioClip deniedAudio;

    private TreeSpot[] TreeSpots;

    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        if (TreeSpots == null) {
            TreeSpots = FindObjectsOfType<TreeSpot>();
        }
    }

    void Start()
    {
        speed = defaultSpeed;
        updateCapacityText();
        updateMoneyText();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.E)) {
            if (withinSellZone) {
                if (CapacityLimit.money >= 0 && CapacityLimit.capacity > 0) {
                    AddMoney(CapacityLimit.capacity * 10);
                    CapacityLimit.capacity = 0;
                    GetComponent<AudioSource>().PlayOneShot(moneyAudio);
                    updateCapacityText();
                    updateMoneyText();
                }    
            }
            else if (withinAddTree) {
                    AddTree();
            }
            else if (withinAddCap) {
                if (CapacityLimit.money >= 100) {
                    CapacityLimit.money -= 100;
                    updateMoneyText();
                    updateCapacityText();
                    CapacityLimit.capacityLimit += 10;
                }
                else {
                    GetComponent<AudioSource>().PlayOneShot(deniedAudio);
                }
            }
            else if (withinLevelExit) {
                if (CapacityLimit.money >= CapacityLimit.moneyGoal) {
                    //scene transition to success/final results screen
                    SceneManager.LoadScene("Level Select");
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
                GetComponent<AudioSource>().PlayOneShot(speedAudio);
                AddSpeed();
            }
            else {
                if(CapacityLimit.capacity < CapacityLimit.capacityLimit) {
                    GetComponent<AudioSource>().PlayOneShot(collectAudio);
                    updateCapacityText();
                }
                updateCapacityText();
            }
            updateCapacityText();
        }
        if(other.tag == "Sell") {
            withinSellZone = true;
        }
        if(other.tag == "AddTree") {
            withinAddTree = true;
        }
        if(other.tag == "AddCapacity") {
            withinAddCap = true;
        }
        if(other.tag == "Level Exit") {
            withinLevelExit = true;
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
            withinAddCap = false;
        }
        if(other.tag == "Level Exit") {
            withinLevelExit = false;
        }
    }

    void AddMoney(int amount) {
        CapacityLimit.money += amount;
    }

    void AddSpeed() {
        StartCoroutine(SpeedBoost());
        IEnumerator SpeedBoost() {
            speed = powerSpeed;
            sr.sprite = speedSprite;
            yield return new WaitForSeconds(5f);
            speed = defaultSpeed;
            sr.sprite = normalSprite;
        }
    }

    void AddTree() {
        for (int i = 0; i < TreeSpots.Length; i++) {
            if (!(TreeSpots[i].hasTree) && CapacityLimit.money >= 50) {
                CapacityLimit.money -= 50;
                updateMoneyText();
                TreeSpots[i].GetTree();
                break;
            }
        }
    }

    void updateCapacityText() {
        capacityText.text = CapacityLimit.capacity.ToString() + " / " + CapacityLimit.capacityLimit.ToString();
    }

    void updateMoneyText() {
        moneyText.text = "$" + CapacityLimit.money.ToString();
    }

    void playWalkAudio() {
        StartCoroutine(WalkAudioRoutine());
        IEnumerator WalkAudioRoutine() {
            GetComponent<AudioSource>().PlayOneShot(walkAudio);
            yield return new WaitForSeconds(1f);
        }
    }
}
