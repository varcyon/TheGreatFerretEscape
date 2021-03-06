﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
public class Movement : MonoBehaviourPunCallbacks {
    public CharacterController controller;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float speed = 12f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3f;
    public GameObject cameraParent;
    private void Start () {
        cameraParent.SetActive (photonView.IsMine);
    }
    void Update () {
        if (!photonView.IsMine) { return; }

        isGrounded = Physics.CheckSphere (groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move (move * speed * Time.deltaTime);

        if (Input.GetButtonDown ("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt (jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move (velocity * Time.deltaTime);

    }
}