using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StairsController : MonoBehaviour
{
	public GameObject Stairs;
	public List<GameObject> allSteps;
	public int stepCount;
	public int adjustCount;
	public int stairsClimbed;
	public int startingStairs;
	public Stairs lastStep;
	public GameObject newStep;

	public int turnCountdown;

	public Vector3 currentTurn;
	public Vector3 mountainFix;
	public float mountainFixStep;

	public int turnLength;
	public float turnChance;
	public int deleteThreshold;

	public GameObject plane;
	public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
		//stepCount -= startingStairs;
		for (int i = 0; i < startingStairs; i++)
		{
			SpawnStep();
		}

		//StartCoroutine(Teleport());
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Q))
		{
			SpawnStep();
		}
		if (Input.GetKeyDown(KeyCode.V))
		{
			foreach (GameObject step in allSteps)
			{
				Destroy(step);
			}
			allSteps.Clear();
			stepCount = 0;
			lastStep = null;
			mountainFix = Vector3.zero;
			//countLeftTurn = 0;
			for (int i = 0; i < startingStairs; i++)
			{
				SpawnStep();
			}
		}*/
			
	}

	public void Teleport()
	{
		//yield return new WaitForSeconds(1f);
		if (PlayerPrefs.HasKey("climbed"))
		{
			stairsClimbed = PlayerPrefs.GetInt("climbed");
			if (stairsClimbed >= startingStairs / 2)
			{
				for (int i = 0; i < startingStairs / 2; i++)
				{
					SpawnStep();
				}
				FindObjectOfType<CharacterController>().enabled = false;
				player.transform.position = allSteps[startingStairs / 2].transform.position;
				FindObjectOfType<CharacterController>().enabled = true;
				Debug.Log(allSteps[startingStairs / 2].transform.position + " " + player.transform.position);
				plane.SetActive(false);
			}
		}
	}

	public void SpawnStep()
	{
		stepCount++;
		newStep = Instantiate(Stairs, Vector3.zero, Quaternion.identity);
		allSteps.Add(newStep);
		
		if (allSteps.Count > deleteThreshold)
		{
			Destroy(allSteps[0]);
			allSteps.RemoveAt(0);
		}

		if (stepCount % 3 == 0 || stepCount % 3 == 1)
		{
			newStep.GetComponent<Stairs>().gate.SetActive(false);
		}

		//Debug.Log("Spawn Step");
	}

	public void AdjustStep(Stairs step)
	{
		adjustCount++;
		if (lastStep)
		{
			float newHeight = Random.Range(0f, .5f);
			if (newHeight < 0.2f)
			{
				newHeight = 0.01f;
			}
			float newDepth = Random.Range(1f, 5f);

			if (turnCountdown == 0)
			{
				float doTurn = Random.Range(-1f, 1f);
				if (doTurn <= turnChance && doTurn > 0f)
				{
					turnCountdown = turnLength;
					currentTurn = new Vector3(0f, currentTurn.y + 5f, 0f);
					mountainFix = new Vector3(0f, 0f, -mountainFixStep);
				}
				else if (doTurn >= -turnChance && doTurn < 0f)
				{

					turnCountdown = turnLength;
					currentTurn = new Vector3(0f, currentTurn.y - 5f, 0f);
					mountainFix = new Vector3(0f, 0f, mountainFixStep);
				}
			}
			if (turnCountdown > 0)
			{
				turnCountdown--;
				if (turnCountdown == 0f)
				{
					currentTurn = Vector3.zero;
				}
				if (currentTurn.y == 5f)
				{
					mountainFix += new Vector3(0f, 0f, mountainFixStep); ;
				}
				if (currentTurn.y == -5f) 
				{
					mountainFix += new Vector3(0f, 0f, -mountainFixStep); ;
				}
			}

			step.transform.eulerAngles = lastStep.transform.eulerAngles + currentTurn;


			step.transform.localPosition = lastStep.transform.localPosition + new Vector3(0, newHeight, 0) + step.transform.TransformPoint(Vector3.forward);
			//step.vertices[4] = new Vector3(lastStep.vertices[0].x, lastStep.vertices[0].y - newHeight * 2f, lastStep.vertices[0].z - 1f);
			//step.vertices[5] = new Vector3(lastStep.vertices[1].x, lastStep.vertices[1].y - newHeight * 2f, lastStep.vertices[1].z - 1f);
			step.vertices[4] = step.transform.InverseTransformPoint(lastStep.transform.TransformPoint(lastStep.vertices[0]));
			step.vertices[5] = step.transform.InverseTransformPoint(lastStep.transform.TransformPoint(lastStep.vertices[1]));

			step.vertices[7] = step.transform.InverseTransformPoint(lastStep.transform.TransformPoint(lastStep.vertices[6]));
			step.vertices[9] = step.transform.InverseTransformPoint(lastStep.transform.TransformPoint(lastStep.vertices[8]));

			step.vertices[11] = step.transform.InverseTransformPoint(lastStep.transform.TransformPoint(lastStep.vertices[10]));
			step.vertices[13] = step.transform.InverseTransformPoint(lastStep.transform.TransformPoint(lastStep.vertices[12]));

			//step.vertices[6] = step.vertices[7] + new Vector3(0, newHeight, 0) * 1.5f + mountainFix;
			//step.vertices[8] = step.vertices[9] + new Vector3(0, newHeight, 0) * 1.5f + mountainFix;

			//step.vertices[10] = new Vector3(step.vertices[10].x, step.vertices[10].y, step.vertices[10].z - currentTurn.y);
			//step.vertices[12] = new Vector3(step.vertices[12].x, step.vertices[12].y, step.vertices[12].z + currentTurn.y);

			step.vertices[2] = new Vector3(step.vertices[4].x, step.vertices[0].y, step.vertices[4].z);
			step.vertices[3] = new Vector3(step.vertices[5].x, step.vertices[1].y, step.vertices[5].z);

			//step.vertices[0] = new Vector3(step.vertices[0].x, step.vertices[0].y, step.vertices[0].z + 5f);
			//step.vertices[1] = new Vector3(step.vertices[1].x, step.vertices[1].y, step.vertices[1].z + 5f);

			step.Rebuild();
		}
		
		lastStep = step;
		
		if (adjustCount == startingStairs)
		{
			Teleport();
		}
	}

	private void OnApplicationQuit()
	{
		PlayerPrefs.SetInt("climbed", stairsClimbed);
	}
}
