using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float WalkSpeed = 2.5f;

    [SerializeField]
    private float JumpForce = 250f;

    Rigidbody rb;

    bool IsGrounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        IsGrounded = true;
    }

    // Update is called once per frame
    void FixedUpdate () {
        float h = Input.GetAxis("Horizontal") * WalkSpeed;
        float v = Input.GetAxis("Vertical") * WalkSpeed;

        h *= Time.deltaTime;
        v *= Time.deltaTime;


        transform.Translate(h, 0f, v);

        if (Input.GetKeyDown("space") && IsGrounded)
        {
            IsGrounded = false;
            float jump = JumpForce * Time.deltaTime;
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Terrain"))
            IsGrounded = true;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

}
