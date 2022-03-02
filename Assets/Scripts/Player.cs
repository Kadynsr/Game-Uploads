using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    [SerializeField] float moveSpeed = 5f;

    [Header("Screen Padding")]
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    [Header("Weapons")]
    [SerializeField] GameObject leftGun;
    [SerializeField] GameObject rightGun;


    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter lShooter;
    Shooter rShooter;

    void Awake() {
        lShooter = leftGun.GetComponent<Shooter>();
        rShooter = rightGun.GetComponent<Shooter>();

    }
    void Start() {
        InitBounds();
    }
    void Update() {
        Move();
    }

    void OnMove(InputValue value) {
        rawInput = value.Get<Vector2>();
    }
    void OnFire(InputValue value) {
        if (lShooter is not null) {
            lShooter.isFiring = value.isPressed;
        }
        if (rShooter is not null) {
            rShooter.isFiring = value.isPressed;
        }
    }

    void InitBounds() {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
    void Move() {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }
}
