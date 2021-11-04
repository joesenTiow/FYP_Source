using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void LearnScene()
	{
		SceneManager.LoadScene("Learn Scene");
	}

	public void MenuScene()
	{
		SceneManager.LoadScene("Menu Scene");
	}

	public void PathwayScene()
	{
		SceneManager.LoadScene("Pathway Scene");
	}
		
	public void QuizScene()
	{
		SceneManager.LoadScene("Quiz Scene");
	}

	public void SolarSystemScene()
    {
		SceneManager.LoadScene("Solar System Scene");
    }
}
