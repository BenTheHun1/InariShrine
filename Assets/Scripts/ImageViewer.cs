using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageViewer : MonoBehaviour
{
	public List<Sprite> photos;
	private int curPhoto = 0;
	public Image display;
    // Start is called before the first frame update
    void Start()
    {
        display.sprite = photos[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			PhotoPrev();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			PhotoNext();
		}
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			Exit();
		}
    }

	public void PhotoNext()
	{
		curPhoto++;
		if (curPhoto > photos.Count - 1) 
		{
			curPhoto = 0;
		}
		display.sprite = photos[curPhoto];
	}

	public void PhotoPrev()
	{
		curPhoto--;
		if (curPhoto < 0)
		{
			curPhoto = photos.Count - 1;
		}
		display.sprite = photos[curPhoto];
	}

	public void Exit()
	{
		SceneManager.LoadScene(0);
	}
}
