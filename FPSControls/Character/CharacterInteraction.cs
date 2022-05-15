using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInteraction : MonoBehaviour
{

	public Transform PlayerCamera;
	[Tooltip("How far the player can interact from")]
	public float InteractDistance = 2;
	public Image Cursor;
	public float CursorUpdateTime = 0.01f;
	
	private RaycastHit _hit;

	private void Awake()
	{
		PlayerCamera = Camera.main.transform;
	}
	
	/*public void Interact()
	{
		if (Physics.Raycast(PlayerCamera.position, PlayerCamera.forward, out _hit, InteractDistance))
		{
			var interactable = _hit.collider.GetComponent<IInteractable>();

			if (interactable != null)
			{
				interactable.Use(gameObject);
			}
		}
	}*/

}
