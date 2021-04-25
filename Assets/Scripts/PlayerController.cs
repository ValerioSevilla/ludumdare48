using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private CharacterController2D controller;
	[SerializeField] private float runSpeed = 30.0f;

	private float horizontalMove = 0.0f;
	private bool jump = false;
	private bool facingRight = true;

	public static PlayerController Instance { get; private set; }
	public bool FacingRight { get { return facingRight; } }

	// private void LogCollisions() {
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag != "Item")
			return;

		GlassesItemController itemController = other.gameObject.GetComponent<GlassesItemController>();

		Sprite glassesSprite = itemController.GetSprite();
		itemController.Pick();
	}
	
	void Update()
	{
		float horizontalAxis = Input.GetAxisRaw("Horizontal");
		horizontalMove = horizontalAxis * runSpeed;
		if (Input.GetButtonDown("Jump"))
			jump = true;
		else if (Input.GetButtonUp("Jump"))
			jump = false;
		
		if (horizontalAxis > 0.0f)
			facingRight = true;
		else if(horizontalAxis < 0.0f)
			facingRight = false;
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
