using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class QuizManager : MonoBehaviour
{
	private int question, score, quizNumber, previousQuizScore;

	public int arrayIndex;

	public Button falseButton;
	public Button nextButton;
	public Button trueButton;

	public Button backToPathwayButton;
	public Button tryAgainButton;

	public Button[] choiceButtons;

	public GameObject successPanel;
	public GameObject earth, sun, moon, sunEarthOrbit, mercury, venus, mars, jupiter, saturn, uranus, neptune;

	public Text panelText, questionText, quizTitleText, scoreText, messageText;

	public AudioSource correctSound, incorrectSound, successSound;

	private int[] questionNumbers = new int[5];


	private string[] successMessages = { "That's right!", "Nice one!", "That is correct!", "Yep, you've got it!" };

	// Start is called before the first frame update
	void Start()
	{
		arrayIndex = 0;
		score = 0;

		quizNumber = PlayerPrefs.GetInt("QUIZ NUMBER");

		// Questions are set at random according to the quiz.
		switch (quizNumber)
		{
			case 0:
				GenerateUniqueRandomNumbers(questionNumbers, 1, 6);
				previousQuizScore = PlayerPrefs.GetInt("QUIZ 1 SCORE");
				break;
			case 1:
				GenerateUniqueRandomNumbers(questionNumbers, 6, 11);
				previousQuizScore = PlayerPrefs.GetInt("QUIZ 2 SCORE");
				break;
			case 2:
				GenerateUniqueRandomNumbers(questionNumbers, 11, 16);
				previousQuizScore = PlayerPrefs.GetInt("QUIZ 3 SCORE");
				break;
		}

		question = questionNumbers[arrayIndex];

		switch (question)
		{
			case 3:
			case 5:
			case 9:
			case 13:
			case 15:
				SetChoiceButtons(); // for multiple choice questions.
				break;
		}
	}

	// Update is called once per frame
	void Update()
	{
		switch (question)
		{
			case 1:
				// Question1(); // Compare: Which object is bigger? The Sun or Earth?
				break;
			case 2:
				Question2(); // TrueFalse: The Sun revolves around the Earth.
				break;
			case 3:
				Question3(); // MCQ: The Moon orbits the Earth about every ___ days.
				break;
			case 4:
				Question4(); // Compare: Which object is brighter? The Sun or Moon?
				break;
			case 5:
				Question5(); // MCQ: The Earth is around ___ billion years old.
				break;
			case 6:
				Question6(); // Compare: Which planet is hotter? Mercury or Venus?
				break;
			case 7:
				Question7(); // Compare: Which planet is bigger? Earth or Mars?
				break;
			case 8:
				Question8(); // TrueFalse: Mars is the outermmost terrestrial planet. 
				break;
			case 9:
				Question9(); // MCQ: Mercury orbits the Sun about every ___ days.
				break;
			case 10:
				Question10(); // Compare: Which planet is closer to the Sun? Venus or Earth?
				break;
			case 11:
				Question11(); // Compare: Which planet is a gas giant? Jupiter or Mercury?
				break;
			case 12:
				Question12(); // Compare: Which planet is an ice giant? Venus or Uranus?
				break;
			case 13:
				Question13(); // MCQ: How many confirmed moons does Saturn have?
				break;
			case 14:
				Question14(); // TrueFalse: Neptune's color is caused by the presence of methane.
				break;
			case 15:
				Question15(); // MCQ: A day on Jupiter lasts for about ___ hours.
				break;
			case 16:
				if (score == 0)
				{
					panelText.text = "Too bad.";
				}
				else if (score == 1 || score == 2)
				{
					panelText.text = "Nice try.";
				}
				else
				{
					panelText.text = "Great job!";
				}

				panelText.text += "\nYou got " + score + " out of 5 correct";
				successPanel.SetActive(true);
				break;
		}
		quizTitleText.text = "Quiz " + (quizNumber + 1);
	}

	public void NextQuestion()
	{
		// If final question is completed, finish quiz.
		if (question == 16)
		{
			if (quizNumber == 2)
			{
				SceneManager.LoadScene("Pathway Scene");
			}
			else
			{
				SceneManager.LoadScene("Learn Scene");
			}
		}
		if (question == questionNumbers[4])
		{
			question = 16;
		}
		// If user has not reached final question, progress to next question.
		else
		{
			if (question != 16)
            {
				arrayIndex++;
			}
			question = questionNumbers[arrayIndex];
		}

		successPanel.SetActive(false);

		earth.SetActive(false);
		sun.SetActive(false);
		sunEarthOrbit.SetActive(false);
		moon.SetActive(false);
		mercury.SetActive(false);
		venus.SetActive(false);
		mars.SetActive(false);
		jupiter.SetActive(false);
		saturn.SetActive(false);
		uranus.SetActive(false);
		neptune.SetActive(false);

		trueButton.gameObject.SetActive(false);
		falseButton.gameObject.SetActive(false);

		for (int i = 0; i < choiceButtons.Length; i++)
		{
			choiceButtons[i].gameObject.SetActive(false);
		}

		switch (question)
		{
			case 3:
			case 5:
			case 9:
			case 13:
			case 15:
				SetChoiceButtons();
				break;
			case 16:
				OnFinishQuiz();
				break;
		}
	}

	#region Questions
	void Question1()
	{
		earth.SetActive(true);
		sun.SetActive(true);

		earth.transform.position = new Vector3(-0.055f, 0, 0.45f);
		sun.transform.position = new Vector3(0.055f, 0, 0.45f);

		questionText.text = "Which object is bigger?";

		SetChoiceBetweenTwoPlanets(sun, earth);
	}// Compare: Which object is bigger? The Sun or Earth?

	void Question2()
	{
		sunEarthOrbit.SetActive(true);
		sunEarthOrbit.transform.position = new Vector3(0, 0, 1);
		trueButton.gameObject.SetActive(true);
		falseButton.gameObject.SetActive(true);

		questionText.text = "True or False: The Sun revolves around the Earth.";
	}// TrueFalse: The Sun revolves around the Earth.

	void Question3()
	{
		moon.SetActive(true);
		moon.transform.position = new Vector3(0, 0, 0.45f);
		questionText.text = "The Moon orbits the Earth about every _____ days.";
	}// MCQ: The Moon orbits the Earth about every ___ days.

	void Question4()
	{
		sun.SetActive(true);
		moon.SetActive(true);

		sun.transform.position = new Vector3(-0.055f, 0, 0.45f);
		moon.transform.position = new Vector3(0.055f, 0, 0.45f);

		questionText.text = "Which object is brighter?";

		SetChoiceBetweenTwoPlanets(sun, moon);
	}// Compare: Which object is brighter? The Sun or Moon?

	void Question5()
	{
		earth.SetActive(true);
		earth.transform.position = new Vector3(0, 0, 0.45f);
		questionText.text = "The Earth is around ___ billion years old.";
	}// MCQ: The Earth is around ___ billion years old.

	void Question6()
	{
		mercury.SetActive(true);
		venus.SetActive(true);

		mercury.transform.position = new Vector3(-0.055f, 0, 0.45f);
		venus.transform.position = new Vector3(0.055f, 0, 0.45f);

		questionText.text = "Which planet is hotter?";

		SetChoiceBetweenTwoPlanets(venus, mercury);
	}// Compare: Which planet is hotter? Mercury or Venus?

	void Question7()
	{
		mars.SetActive(true);
		earth.SetActive(true);

		mars.transform.position = new Vector3(-0.055f, 0, 0.45f);
		earth.transform.position = new Vector3(0.055f, 0, 0.45f);

		questionText.text = "Which planet is bigger?";

		SetChoiceBetweenTwoPlanets(earth, mars);
	}// Compare: Which planet is bigger? Earth or Mars?

	void Question8()
	{
		mars.SetActive(true);
		mars.transform.position = new Vector3(0, 0, 0.45f);
		trueButton.gameObject.SetActive(true);
		falseButton.gameObject.SetActive(true);

		questionText.text = "True or False: Mars is the outermmost terrestrial planet.";
	}// TrueFalse: Mars is the outermmost terrestrial planet. 

	void Question9()
    {
		mercury.SetActive(true);
		mercury.transform.position = new Vector3(0, 0, 0.45f);
		questionText.text = "Mercury orbits the Sun about every ___ days.";
	}// MCQ: Mercury orbits the Sun about every ___ days.

	void Question10()
    {
		earth.SetActive(true);
		venus.SetActive(true);

		earth.transform.position = new Vector3(-0.055f, 0, 0.45f);
		venus.transform.position = new Vector3(0.055f, 0, 0.45f);

		questionText.text = "Which planet is closer to the Sun?";

		SetChoiceBetweenTwoPlanets(venus, earth);
	}// Compare: Which planet is closer to the Sun? Venus or Earth?

	void Question11()
	{		
		jupiter.SetActive(true);
		mercury.SetActive(true);

		jupiter.transform.position = new Vector3(-0.055f, 0, 0.45f);
		mercury.transform.position = new Vector3(0.055f, 0, 0.45f);

		questionText.text = "Which planet is a gas giant?";

		SetChoiceBetweenTwoPlanets(jupiter, mercury);
	}// Compare: Which planet is a gas giant? Jupiter or Mercury?

	void Question12()
	{
		venus.SetActive(true);
		uranus.SetActive(true);

		venus.transform.position = new Vector3(-0.055f, 0, 0.45f);
		uranus.transform.position = new Vector3(0.055f, 0, 0.45f);

		questionText.text = "Which planet is an ice giant?";

		SetChoiceBetweenTwoPlanets(uranus, venus);
	}// Compare: Which planet is an ice giant? Venus or Uranus?

	void Question13()
	{		
		saturn.SetActive(true);
		saturn.transform.position = new Vector3(0, 0, 0.45f);

		questionText.text = "How many confirmed moons does Saturn have?";
	}// MCQ: How many confirmed moons does Saturn have?

	void Question14()
    {
		neptune.SetActive(true);
		neptune.transform.position = new Vector3(0, 0, 0.45f);
		trueButton.gameObject.SetActive(true);
		falseButton.gameObject.SetActive(true);

		questionText.text = "True or False: Neptune's color is caused by the presence of methane.";
	}// TrueFalse: Neptune's color is caused by the presence of methane.

	void Question15()
    {
		jupiter.SetActive(true);
		jupiter.transform.position = new Vector3(0, 0, 0.45f);
		questionText.text = "A day on Jupiter lasts for about ___ hours.";
	}// MCQ: A day on Jupiter lasts for about ___ hours.

	#endregion Questions

	#region TrueFalse
	public void OnClickTrueButton()
	{
		if (question == 2)
		{
			AnswerIsIncorrect();
		}
		else if (question == 8 || question == 14)
        {
			AnswerIsCorrect();
		}
		successPanel.SetActive(true);

		trueButton.interactable = false;
		falseButton.interactable = false;
	}

	public void OnClickFalseButton()
	{
		if (question == 2)
		{
			AnswerIsCorrect();
		}
		else if (question == 8 || question == 14)
        {
			AnswerIsIncorrect();
		}
		successPanel.SetActive(true);

		trueButton.interactable = false;
		falseButton.interactable = false;
	}
	#endregion TrueFalse

	#region MultipleChoice
	public void OnClickChoiceButton(Button button)
	{
		if ((question == 3 && button.GetComponentInChildren<Text>().text == "27") 
			|| (question == 5 && button.GetComponentInChildren<Text>().text == "4.5")
			|| (question == 9 && button.GetComponentInChildren<Text>().text == "88")
			|| (question == 13 && button.GetComponentInChildren<Text>().text == "82")
			|| (question == 15 && button.GetComponentInChildren<Text>().text == "10"))
        {
			AnswerIsCorrect();
		}
		else
        {
			AnswerIsIncorrect();
		}
		successPanel.SetActive(true);

		for (int i = 0; i < choiceButtons.Length; i++)
		{
			choiceButtons[i].interactable = false;
		}
	}

	void SetChoiceButtons()
	{
		int[] randomNumbers = new int[choiceButtons.Length];

		// Generate random numbers.
		switch (question)
        {
			case 3:
				GenerateUniqueRandomNumbers(randomNumbers, 25, 29);
				break;
			case 5:
				GenerateUniqueRandomNumbers(randomNumbers, 3, 7);
				break;
			case 9:
				GenerateUniqueRandomNumbers(randomNumbers, 87, 91);
				break;
			case 13:
				GenerateUniqueRandomNumbers(randomNumbers, 79, 83);
				break;
			case 15:
				GenerateUniqueRandomNumbers(randomNumbers, 9, 13);
				break;
		}
		
		// Assign the random numbers to the buttons.
		for (int i = 0; i < choiceButtons.Length; i++)
		{
			choiceButtons[i].GetComponentInChildren<Text>().text = randomNumbers[i].ToString();
			if (question == 5)
            {
				choiceButtons[i].GetComponentInChildren<Text>().text += ".5";
			}
			choiceButtons[i].gameObject.SetActive(true);
			choiceButtons[i].interactable = true;
		}
	}
	#endregion MultipleChoice

	void UpdateScore() // When player answers correct, increase score.
	{
		score += 1;
		scoreText.text = "Your Score: " + score;
	}

	void OnFinishQuiz()
    {
		if (previousQuizScore != -1 && score > previousQuizScore)
		{
			messageText.gameObject.SetActive(true);
			messageText.text = "You scored " + (score - previousQuizScore) + " more points than the last time!";
		}

		if (score >= 3)
        {
			successSound.Play();
		}
		
		switch (quizNumber)
		{
			case 0:
				PlayerPrefs.SetInt("QUIZ 1 SCORE", score);
				PlayfabController.controller.SetQuiz1Score(score);
				break;
			case 1:
				PlayerPrefs.SetInt("QUIZ 2 SCORE", score);
				PlayfabController.controller.SetQuiz2Score(score);
				break;
			case 2:
				PlayerPrefs.SetInt("QUIZ 3 SCORE", score);
				PlayfabController.controller.SetQuiz3Score(score);
				break;
		}

		// If user doesn't score more than 3, let user try again.
		if (score < 3)
		{
			nextButton.gameObject.SetActive(false);
			tryAgainButton.gameObject.SetActive(true);
		}
		else
		{
			// If user finishes quiz, progress to next topic... 
			nextButton.GetComponentInChildren<Text>().text = "Continue";

			if (quizNumber == 0)
			{
				PlayerPrefs.SetInt("TOPIC NUMBER", 4);
			}
			else if (quizNumber == 1)
			{
				PlayerPrefs.SetInt("TOPIC NUMBER", 7);
			}
			// If user has not unlocked next topic, unlock next topic
			if (PlayerPrefs.GetInt("TOPIC NUMBER") > PlayerPrefs.GetInt("UNLOCKED TOPICS"))
			{
				PlayerPrefs.SetInt("UNLOCKED TOPICS", PlayerPrefs.GetInt("TOPIC NUMBER"));
			}
			// ...and update player data.
			PlayfabController.controller.SetPlayerData(PlayerPrefs.GetInt("UNLOCKED TOPICS"),
				PlayerPrefs.GetInt("LESSONS COMPLETED"), PlayerPrefs.GetInt("UNLOCKED QUIZZES"));
		}
		backToPathwayButton.gameObject.SetActive(true);
	}

	void SetChoiceBetweenTwoPlanets(GameObject correctAnswer, GameObject wrongAnswer)
    {
		correctAnswer.GetComponent<SphereCollider>().enabled = true;
		wrongAnswer.GetComponent<SphereCollider>().enabled = true;

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				// If user selects correct answer, display correct message.
				if (hit.transform.name == correctAnswer.name)
				{
					AnswerIsCorrect();
				}
				else if (hit.transform.name == wrongAnswer.name)
				{
					AnswerIsIncorrect();
				}

				successPanel.SetActive(true);

				// Once user makes selection, disable all click events of objects.
				correctAnswer.GetComponent<SphereCollider>().enabled = false;
				wrongAnswer.GetComponent<SphereCollider>().enabled = false;
			}
		}
	}

	void GenerateUniqueRandomNumbers(int[] array, int minRange, int maxRange)
    {
		int randomNumber;

		// Generate an array of unique random numbers within a range.
		for (int i = 0; i < array.Length; i++)
		{
			randomNumber = UnityEngine.Random.Range(minRange, maxRange);

			if (i != 0)
			{
				// If a number already exists in the array, randomize the number until it is unique.
				do
				{
					randomNumber = UnityEngine.Random.Range(minRange, maxRange);
				}
				while (Array.Exists(array, element => element == randomNumber));
			}
			array[i] = randomNumber;
		}
	}

	void AnswerIsCorrect()
    {
		correctSound.Play();
		int randomNumber = UnityEngine.Random.Range(0, successMessages.Length);

		panelText.text = successMessages[randomNumber];
		UpdateScore();
    }

	void AnswerIsIncorrect()
    {
		incorrectSound.Play();
		panelText.text = "Oops, that is incorrect!";
	}

	public void OnTryAgainButtonClicked()
    {
		SceneManager.LoadScene("Quiz Scene");
    }
}
