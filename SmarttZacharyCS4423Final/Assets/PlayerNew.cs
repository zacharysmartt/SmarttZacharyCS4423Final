using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerNew : MonoBehaviour
{

    Rigidbody2D rb2d;
    public float speed = 2f;
    bool isDashing;
    public int capacity = 0;
    //public int capacityLimit = 10;
    public Text capacityText;
    // Start is called before the first frame update
    public int money = 0;
    public Text moneyText;
    public GameObject fist;
    public Text warningText;
    //public SellZone sellZone;
    public bool withinSellZone;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Sell") {
            withinSellZone = false;
            Debug.Log("Out of sell zone");
        }
    }

    void updateCapacityText() {
        capacityText.text = CapacityLimit.capacity.ToString();
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
