using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private CharacterController2D controller;
	[SerializeField] private float runSpeed = 30.0f;

	private float horizontalMove = 0.0f;
	private bool jump = false;

	public static PlayerController Instance { get; private set; }

	// private void LogCollisions() {

	void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		if (Input.GetButtonDown("Jump"))
			jump = true;
		else if (Input.GetButtonUp("Jump"))
			jump = false;
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

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(gameObject);
	}
}
