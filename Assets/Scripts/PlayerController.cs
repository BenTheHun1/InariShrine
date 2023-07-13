using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public CharacterController controller;
	public StairsController stairsController;
	public float speed;
	public Vector3 velocity;
	private float gravity = 0.3f * (-9.81f * 6);
	public GameObject curStep;
	public GameObject encourageText;

	public RaycastHit ray;
	public bool pausedSteps;
	public AudioSource steps;
	public PauseMenu pause;

	// Start is called before the first frame update
	void Start()
    {
		encourageText.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
		controller = GetComponent<CharacterController>();   
		stairsController = FindObjectOfType<StairsController>();
    }

    // Update is called once per frame
    void Update()
    {
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 move = transform.right * x + transform.forward * z;
		//Debug.Log((move == Vector3.zero) + " " + (!pausedSteps));
		if (curStep)
		{
			//Debug.Log(move + " " + curStep.transform.TransformDirection(Vector3.back) + " " + (move - curStep.transform.TransformDirection(Vector3.back)).magnitude);
			if ((move - curStep.transform.TransformDirection(Vector3.back)).magnitude >= 1)
			{
				encourageText.SetActive(false);
				controller.Move(move * speed * Time.deltaTime);
				pausedSteps = false;
				steps.UnPause();
			}
			else
			{
				encourageText.SetActive(true);
				pausedSteps = true;
				steps.Pause();

			}

		}
		else
		{
			controller.Move(move * speed * Time.deltaTime);
			pausedSteps = true;
			steps.Pause();
		}


		if (move == Vector3.zero && !pausedSteps)
		{
			pausedSteps = true;
			steps.Pause();
		}

		if (velocity.y < 0)
		{
			velocity.y = -20f; //Stops y velocity from infinitely decreasing
		}

		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);

		/*if (Input.GetKeyDown(KeyCode.F))
		{
			Debug.Log(transform.position);
			//stairsController.Teleport();
			//curStep = null;
		}*/
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			pause.Pause();
		}
	}

	private void OnTriggerEnter(Collider collision)
	{

		//Debug.Log("Collision");
		if (collision.TryGetComponent(out Stairs steps))
		{
			curStep = steps.gameObject;
			if (steps.hasGenNextStep == false)
			{
				steps.hasGenNextStep = true;
				stairsController.SpawnStep();
				stairsController.stairsClimbed++;
			}
		}
	}

}
