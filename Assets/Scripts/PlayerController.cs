using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private CharacterController2D controller;
	[SerializeField] private float runSpeed = 30.0f;

	private float horizontalMove = 0.0f;
	private bool jump = false;

	// private void LogCollisions() {

	void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		jump = Input.GetButtonDown("Jump");
	}

	void FixedUpdate()
	{
		controller.Move
		(
			horizontalMove * Time.fixedDeltaTime,
			false,
			jump
		);
	}
}
