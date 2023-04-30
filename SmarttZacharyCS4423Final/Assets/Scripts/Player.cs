
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb2d;
    public float speed = 2f;
    public float buttonTime = 0.2f;
    public float jump = 2;
    float jumpTime;
    bool isJumping;
    /*public float rise = 20f;
    public float fall = 30f;*/

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Transform>().position += new Vector3(Input.GetAxis("Horizontal"),0,0) * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)) {
            isJumping = true;
            jumpTime = 0;
        }
        if (isJumping) {
            //rb2d.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            rb2d.velocity = new Vector2(rb2d.velocity.x, jump);
            jumpTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space) | jumpTime > buttonTime) {
            isJumping = false;
        }
        /*if (rb2d.velocity.y >= 0) {
            rb2d.gravityScale = rise;
        }
        else if (rb2d.velocity.y < 0) {
            rb2d.gravityScale = fall;
        }*/
    }

    void FixedUpdate()
    {
        //rb2d.MovePosition(transform.position + (new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Jump")) * Time.fixedDeltaTime * speed));
        //Move(speed);
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.L)) {
            if (Input.GetKey(KeyCode.Z)) {
                Move(speed * 2);
            }
            else {
                Move(speed);
            }
        }
    }

    private void Move(float speed) {
        //rb2d.velocity = (new Vector3(Input.GetAxisRaw("Horizontal"))) * speed;
        rb2d.MovePosition(transform.position + (new Vector3(Input.GetAxisRaw("Horizontal"),0) * Time.fixedDeltaTime * speed));
        //rb2d.velocity = new Vector3(rb2d.velocity.x, speed);
    }
}
