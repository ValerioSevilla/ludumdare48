using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private static readonly int PUTGLASSES_TRIGGER = Animator.StringToHash("PutGlasses");

	[SerializeField] private CharacterController2D controller;
	[SerializeField] private float runSpeed = 30.0f;
	[SerializeField] private AudioClip[] jumpSounds;

	private float horizontalMove = 0.0f;
	private bool jump = false;
	private bool facingRight = true;
	private int jumpSoundIndex = 0;
	private AudioSource audioSource;

	public static PlayerController Instance { get; private set; }
	public bool FacingRight { get { return facingRight; } }

	// private void LogCollisions() {

	private void PutGlassesOn(Sprite glassesSprite)
	{
		SpriteRenderer glassesSpriteRenderer = transform.Find("Scaler/Face/Glasses").GetComponent<SpriteRenderer>();
		glassesSpriteRenderer.sprite = glassesSprite;

		GetComponent<Animator>().SetTrigger(PUTGLASSES_TRIGGER);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag != "Item")
			return;

		GlassesItemController itemController = other.gameObject.GetComponent<GlassesItemController>();

		Sprite glassesSprite = itemController.GetSprite();
		PutGlassesOn(glassesSprite);

		itemController.Pick();
	}

	void PlayJumpSound()
	{
		audioSource.PlayOneShot(jumpSounds[jumpSoundIndex]);
		jumpSoundIndex = (jumpSoundIndex + 1)%jumpSounds.Length;
	}

	public void Landed()
	{
	}
	
	void Update()
	{
		float horizontalAxis = Input.GetAxisRaw("Horizontal");
		horizontalMove = horizontalAxis * runSpeed;
		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			if (controller.IsGrounded())
				PlayJumpSound();
		}
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
			audioSource = GetComponent<AudioSource>();
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(gameObject);
	}
}
