using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerNew : MonoBehaviour
{

    Rigidbody2D rb2d;

    [Header("Movement")]
    public float speed = 2f;
    bool isDashing;

    [Header("Capacity")]
    public int capacity = 0;
    //public int capacityLimit = 10;
    public Text capacityText;
    //public Text capacityLimitText;

    [Header("Money")]
    public int money;
    public Text moneyText;

    [Header("Area Detection")]
    public bool withinSellZone;
    public bool withinAddTree;
    public bool withinAddCap;

    public GameObject fist;
    public GameObject tree;
    public Text warningText;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        money = 100;
        updateCapacityText();
        updateMoneyText();
    }

    // Update is called once per frame
    void Update()
    {
        //rb2d.MovePosition(transform.position + (new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")) * Time.fixedDeltaTime * speed));   
        if (Input.GetKeyDown(KeyCode.Space)) {
            Punch();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (withinSellZone) {
                money += CapacityLimit.capacity * 10;
                CapacityLimit.capacity = 0;
                Debug.Log("Cha-ching!");
                updateCapacityText();
                updateMoneyText();
            }
            else if (withinAddTree) {
                if (money >= 50 && CapacityLimit.treeCount < CapacityLimit.treeLimit) {
                    money -= 50;
                    updateMoneyText();
                    GameObject newTree;
                    Vector2 treePosition = new Vector2(Random.Range(-7f, 7f), Random.Range(-0.5f, 3f));
                    newTree = Instantiate(tree, treePosition, Quaternion.identity);
                    CapacityLimit.treeLimit++;
                }
            }
            else if (withinAddCap) {
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
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Collectible") {
            if(CapacityLimit.capacity < CapacityLimit.capacityLimit) {
                CapacityLimit.capacity += 1;
                Destroy(other.gameObject);
                updateCapacityText();
            }
        }
        if(other.tag == "Sell") {
            withinSellZone = true;
            Debug.Log("Within sell zone");
        }
        if(other.tag == "AddTree") {
            withinAddTree = true;
            Debug.Log("Within tree zone");
        }
        if(other.tag == "AddCapacity") {
            withinAddCap = true;
            Debug.Log("Within cap zone");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Sell") {
            withinSellZone = false;
            Debug.Log("Out of sell zone");
        }
        if(other.tag == "AddTree") {
            withinAddTree = false;
            Debug.Log("Out of tree zone");
        }
        if(other.tag == "AddCapacity") {
            withinAddCap = false;
            Debug.Log("Out of cap zone");
        }
    }

    void updateCapacityText() {
        capacityText.text = CapacityLimit.capacity.ToString() + " / " + CapacityLimit.capacityLimit.ToString();
        //capacityLimitText.text = "/ " + CapacityLimit.capacityLimit.ToString();
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
}
