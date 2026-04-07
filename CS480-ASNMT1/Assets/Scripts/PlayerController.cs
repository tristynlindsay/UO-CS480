using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody _playerBody;
    private int _score;
    private bool _jump;
    private int _jumpCount;
    private const int MaxJumps = 2;
    private bool _isGrounded;


    void Start() {
        _playerBody = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            if (_jumpCount < MaxJumps) {
                _jump = true;
                _jumpCount++;
            }
        }
    }

    void FixedUpdate() {
        float x = Keyboard.current.dKey.isPressed ? 1.0f : (Keyboard.current.aKey.isPressed ? -1.0f : 0.0f);
        float z = Keyboard.current.wKey.isPressed ? 1.0f : (Keyboard.current.sKey.isPressed ? -1.0f : 0.0f);
        float y = 0.0f;
        if (_jump) {
            y = 100.0f;
            _jump = false;
        }

        Vector3 move = new Vector3 (x, y, z);
        _playerBody.AddForce (move * speed);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag ("Pickup")) {
            other.gameObject.SetActive (false);
        }
    }

    void OnCollisionEnter(Collision collision) {
        _jumpCount = 0;
        _isGrounded = true;
    }

    void OnCollisionExit(Collision collision) {
        _isGrounded = false;
    }

}
