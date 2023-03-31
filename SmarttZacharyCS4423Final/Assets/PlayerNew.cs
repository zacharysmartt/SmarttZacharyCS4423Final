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
    public Text capacityText;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        updateCapacityText();
    }

    // Update is called once per frame
    void Update()
    {
        //rb2d.MovePosition(transform.position + (new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")) * Time.fixedDeltaTime * speed));   
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
            //rb2d.MovePosition(transform.position + (new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")) * Time.fixedDeltaTime * speed));
        }
        rb2d.MovePosition(transform.position + (new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")) * Time.fixedDeltaTime * speed));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Collectible") {
            capacity += 1;
            updateCapacityText();
        }
    }

    void updateCapacityText() {
        capacityText.text = capacity.ToString();
    }
}
