using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Vector2 offset;
	[SerializeField] private float maxSpeed;
	[SerializeField] private float distanceForMaxSpeed;

	// Update is called once per frame
	void Update()
	{
		Vector2 currentPlayerPosition = new Vector2
		(
			PlayerController.Instance.transform.localPosition.x,
			PlayerController.Instance.transform.localPosition.y
		);

		Vector2 actualOffset = new Vector2
		(
			PlayerController.Instance.FacingRight ? offset.x : -offset.x,
			offset.y
		);

		Vector2 goalPosition = currentPlayerPosition + actualOffset;

		Vector2 currentCameraPosition = new Vector2
		(
			transform.localPosition.x,
			transform.localPosition.y
		);

		Vector2 directionVector = goalPosition - currentCameraPosition;
		float distance = directionVector.magnitude;
		directionVector /= distance;

		float speed = Mathf.Min(1.0f, distance/distanceForMaxSpeed) * maxSpeed;

		Vector2 displacement = directionVector * (speed * Time.deltaTime);
		Vector2 finalPosition = currentCameraPosition + displacement;
		transform.localPosition = new Vector3
		(
			finalPosition.x,
			finalPosition.y,
			transform.localPosition.z
		);
	}
}
