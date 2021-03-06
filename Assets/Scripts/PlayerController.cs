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

	private Transform faceTransform;

	public static PlayerController Instance { get; private set; }
	public bool FacingRight {
		get { return facingRight; }
		private set
		{
			facingRight = value;

			faceTransform.localPosition = new Vector3
			(
				Mathf.Abs(faceTransform.localPosition.x) * (facingRight ? 1.0f : -1.0f),
				faceTransform.localPosition.y,
				faceTransform.localPosition.z
			);
		}
	}

	// private void LogCollisions() {

	private void PutGlassesOn(Sprite glassesSprite, string message)
	{
		SpriteRenderer glassesSpriteRenderer = transform.Find("Scaler/Face/Glasses").GetComponent<SpriteRenderer>();
		glassesSpriteRenderer.sprite = glassesSprite;

		TMPro.TextMeshPro text = transform.Find("Scaler/Text").GetComponent<TMPro.TextMeshPro>();
		text.text = message;

		GetComponent<Animator>().SetTrigger(PUTGLASSES_TRIGGER);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Item")
		{
			GlassesItemController itemController = other.gameObject.GetComponent<GlassesItemController>();

			Sprite glassesSprite = itemController.GetSprite();
			string glassesMessage = itemController.GetMessage();
			PutGlassesOn(glassesSprite, glassesMessage);

			itemController.Pick();
		}
		else if(other.gameObject.tag == "Door")
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
		}
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
		{
			FacingRight = true;
		}
		else if(horizontalAxis < 0.0f)
			FacingRight = false;
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
		if (Instance != null)
			Destroy(Instance.gameObject);

		{
			Instance = this;
			audioSource = GetComponent<AudioSource>();
			faceTransform = transform.Find("Scaler/Face");
		}
	}
}
