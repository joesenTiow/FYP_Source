using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PathwayManager : MonoBehaviour
{
	public Button[] topicButtons;
	public Button[] quizButtons;

	void OnEnable()
	{
		PlayerPrefs.SetInt("QUIZ NUMBER", -1);

		// Only the first four topic buttons is enabled by default
		for (int i = 4; i < topicButtons.Length; i++)
		{
			topicButtons[i].interactable = false;
		}
		for (int i = 0; i < quizButtons.Length; i++)
        {
			quizButtons[i].interactable = false;
        }
	}

	// Update is called once per frame
	void Update()
	{
		// If user is logged in and completes the fourth lesson, the next lesson will be unlocked.
		if (PlayerPrefs.GetString("ISLOGGEDIN") == "True")
        {
			for (int i = 0; i <= PlayerPrefs.GetInt("UNLOCKED TOPICS"); i++)
			{
				topicButtons[i].interactable = true;
			}
			for (int i = 0; i < PlayerPrefs.GetInt("UNLOCKED QUIZZES"); i++)
            {
				quizButtons[i].interactable = true;
            }
		}
	}

	public void OnTopicButtonClicked(Button button)
	{
		for (int i = 0; i < topicButtons.Length; i++)
		{
			if (button == topicButtons[i])
			{
				PlayerPrefs.SetInt("TOPIC NUMBER", i);
			}
		}
		SceneManager.LoadScene("Learn Scene");
	}

	public void OnQuizButtonClicked(Button button)
    {
		for (int i = 0; i < quizButtons.Length; i++)
        {
			if (button == quizButtons[i])
            {
				PlayerPrefs.SetInt("QUIZ NUMBER", i);
            }
        }
		SceneManager.LoadScene("Quiz Scene");
    }
}
