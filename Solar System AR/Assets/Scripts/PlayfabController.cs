using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayfabController : MonoBehaviour
{
	public static PlayfabController controller;

	private string userID;
	private string userPassword;
	private string username;
	public GameObject loginPanel;
	public GameObject registerPanel;

	// Dialog box (for the settings panel)
	public GameObject dialogBox;
	public Text dialogBoxTitleText;
	public Text dialogBoxContentText;
	public Button confirmLogoutButton;
	public Button confirmResetButton;
	public Button cancelButton;
	public Button closeButton;

	// Profile panel
	public GameObject profilePanel;
	public Text usernameText;
	public Text lessonsCompletedText;
	public Text topicsUnlockedText;
	public Text userIDText;

	// Settings panel
	public GameObject settingsPanel;
	// public Lean.Gui.LeanToggle soundToggle;
	// public Lean.Gui.LeanToggle musicToggle;
	public Button SoundOnButton;
	public Button SoundOffButton;
	public Button MusicOnButton;
	public Button MusicOffButton;

	// Login Error Message panel
	// Displays error message when username or password is incorrect etc.
	public GameObject loginErrorMessagePanel;
	public Text errorMessage;

	private void OnEnable() // Make sure there's only one PlayfabController for the entire app
	{
		if (controller == null)
		{
			controller = this;
		}
		else if (controller != this)
		{
			Destroy(controller.gameObject);
			controller = this;
		}
		DontDestroyOnLoad(gameObject);
	}

	public void Start()
	{
		//Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
		if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
		{
			PlayFabSettings.TitleId = "77E23"; // Please change this value to your own titleId from PlayFab Game Manager
		}

		// If user has not logged out after the previous session, the current session will keep them logged in.
		if (PlayerPrefs.HasKey("USERNAME"))
		{
			username = PlayerPrefs.GetString("USERNAME");
			userPassword = PlayerPrefs.GetString("PASSWORD");

			var request = new LoginWithPlayFabRequest
			{
				Username = username,
				Password = userPassword
			};
			PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnUserNotLoggedIn);
		}

		
	}

    private void Update()
    {
		if (MusicOnButton != null)
        {
			// Restore toggle settings for music.
			if (PlayerPrefs.GetString("ISMUSICON") == "True")
			{
				// musicToggle.TurnOn();
				MusicOnButton.gameObject.SetActive(true);
				MusicOffButton.gameObject.SetActive(false);
			}
			else
			{
				// musicToggle.TurnOff();
				MusicOnButton.gameObject.SetActive(false);
				MusicOffButton.gameObject.SetActive(true);
			}

			if (PlayerPrefs.GetString("ISSOUNDON") == "True")
			{
				// soundToggle.TurnOn();
				SoundOnButton.gameObject.SetActive(true);
				SoundOffButton.gameObject.SetActive(false);
			}
			else
			{
				// soundToggle.TurnOff();
				SoundOnButton.gameObject.SetActive(false);
				SoundOffButton.gameObject.SetActive(true);
			}
		}
	}

    #region Login
    public void OnClickLogin()
	{
		// Log the user in
		var request = new LoginWithPlayFabRequest
		{
			Username = username,
			Password = userPassword
		};
		PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
	}

	private void OnLoginSuccess(LoginResult result)
	{
		if (loginPanel != null)
		{
			loginPanel.SetActive(false);
		}
		userID = result.PlayFabId;

		GetPlayerData();
		SetPlayerProfile();
	}

	private void OnLoginFailure(PlayFabError error)
	{
		Debug.LogWarning(error.GenerateErrorReport());

		errorMessage.text = error.GenerateErrorReport();

		// Checks for different kinds of errors.
		if (errorMessage.text.Contains("The Username field is required.") 
			&& errorMessage.text.Contains("The Password field is required."))
        {
			errorMessage.text = "Please enter your username and password.";
		}
		else if (errorMessage.text.Contains("The Username field is required."))
        {
			errorMessage.text = "Please enter your username.";
        }
		else if (errorMessage.text.Contains("The Password field is required."))
		{
			errorMessage.text = "Please enter your password.";
		}
		else if (errorMessage.text.Contains("Password must be between 6 and 100 characters")
				 || errorMessage.text.Contains("Invalid username or password")) {
			errorMessage.text = "Username or password incorrect.";
		}
		else if (errorMessage.text.Contains("User not found"))
        {
			errorMessage.text = "User does not exist.";
		}
		

		loginErrorMessagePanel.SetActive(true);
	}

	public void OnChooseRegister()
	{
		loginPanel.SetActive(false);
		registerPanel.SetActive(true);
	}

	public void OnUserNotLoggedIn(PlayFabError error)
    {
		// This method is used on start so that the error message panel won't appear every time the scene starts.
		Debug.Log("User is not logged in");
    }
	#endregion Login

	#region Register
	public void onClickRegister()
	{
		// Register the user
		var registerRequest = new RegisterPlayFabUserRequest
		{
			Password = userPassword,
			Username = username,
			RequireBothUsernameAndEmail = false
		};
		PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
	}

	private void OnRegisterSuccess(RegisterPlayFabUserResult result)
	{
		Debug.Log("User is registered!");
		registerPanel.SetActive(false);
		userID = result.PlayFabId;
		username = result.Username;

		PlayerPrefs.SetInt("UNLOCKED TOPICS", 3);
		PlayerPrefs.SetInt("LESSONS COMPLETED", 0);

		SetPlayerData(3, 0, 0);

		SetQuiz1Score(-1);
		SetQuiz2Score(-1);
		SetQuiz3Score(-1);

		GetPlayerData();

		SetPlayerProfile();
	}

	private void OnRegisterFailure(PlayFabError error)
    {
		Debug.LogWarning(error.GenerateErrorReport());

		errorMessage.text = error.GenerateErrorReport();

		// Checks for differend kinds of errors.
		if (username == "" && userPassword == "")
        {
			errorMessage.text = "Please enter a username and password.";
		}
		else if (username == "")
		{
			errorMessage.text = "Please enter a username.";
		}
		else if (errorMessage.text.Contains("Username must be between 3 and 20 characters"))
		{
			errorMessage.text = "Username must be at least 3 characters.";
		}
		else if (errorMessage.text.Contains("Password must be between 6 and 100 characters"))
        {
			errorMessage.text = "Password must be at least 6 characters.";
		}
		else if (errorMessage.text.Contains("User name already exists"))
        {
			errorMessage.text = "Username already exists.";
        }
		
		loginErrorMessagePanel.SetActive(true);
	}

	public void OnChooseLogin()
	{
		registerPanel.SetActive(false);
		loginPanel.SetActive(true);
	}
	#endregion Register

	public void OnClose() // close login and register panels
	{
		loginPanel.SetActive(false);
		registerPanel.SetActive(false);
	}

	public void OnOK()
    {
		loginErrorMessagePanel.SetActive(false);
    }

	#region InputValues

	public void GetUserPassword(string passwordIn)
	{
		userPassword = passwordIn;
	}

	public void GetUsername(string usernameIn)
	{
		username = usernameIn;
	}

	#endregion InputValues

	#region MenuButtons

	public void OnClickLearnButton()
	{
		SceneManager.LoadScene("Pathway Scene");
	}

	public void OnClickProfileButton()
	{
		if (PlayerPrefs.GetString("ISLOGGEDIN") == "False")
		{
			if (loginPanel.activeSelf)
            {
				loginPanel.SetActive(false);
            }
			else
            {
				loginPanel.SetActive(true);
			}
			
		}
		else
		{
			settingsPanel.SetActive(false);
			if (profilePanel.activeSelf)
            {
				profilePanel.SetActive(false);
            }
			else
            {
				profilePanel.SetActive(true);
            }
		}
	}

	public void OnClickHomeButton()
	{
		profilePanel.SetActive(false);
		settingsPanel.SetActive(false);
	}

	#endregion MenuButtons

	#region Settings

	public void OnClickSettingsButton()
	{
		profilePanel.SetActive(false);
		if (settingsPanel.activeSelf)
		{
			settingsPanel.SetActive(false);
		}
		else
		{
			settingsPanel.SetActive(true);
		}
	}

	public void OnClickResetProgressButton()
	{
		confirmLogoutButton.gameObject.SetActive(false);
		closeButton.gameObject.SetActive(false);
		dialogBoxTitleText.text = "Reset Progress";

		if (PlayerPrefs.GetString("ISLOGGEDIN") == "False")
        {
			dialogBoxContentText.text = "User is not logged in.";
			closeButton.gameObject.SetActive(true);
		}
		else
        {
			dialogBoxContentText.text = "Are you sure you want to reset your progress? This cannot be undone.";
			confirmResetButton.gameObject.SetActive(true);
			cancelButton.gameObject.SetActive(true);
		}

		dialogBox.SetActive(true);
	}

	public void OnConfirmResetProgress()
    {
		confirmResetButton.gameObject.SetActive(false);
		cancelButton.gameObject.SetActive(false);
		
		Invoke("OnReset", 1.5f);

		dialogBoxContentText.text = "Resetting your progress...";
		
		SetPlayerData(3, 0, 0);
		SetQuiz1Score(-1);
		SetQuiz2Score(-1);
		SetQuiz3Score(-1);

		PlayerPrefs.SetInt("QUIZ NUMBER", -1);
		
		GetPlayerData();
	}

	void OnReset()
    {
		dialogBoxContentText.text = "Your progress has been reset.";
		settingsPanel.SetActive(false);
		closeButton.gameObject.SetActive(true);
	}

	public void OnClickLogoutButton()
	{
		confirmResetButton.gameObject.SetActive(false);
		closeButton.gameObject.SetActive(false);
		dialogBoxTitleText.text = "Logout";

		if (PlayerPrefs.GetString("ISLOGGEDIN") == "False")
        {
			dialogBoxContentText.text = "User is already logged out.";
			closeButton.gameObject.SetActive(true);
		}
		else
        {
			dialogBoxContentText.text = "Are you sure you want to log out?";
			confirmLogoutButton.gameObject.SetActive(true);
			cancelButton.gameObject.SetActive(true);
		}

		dialogBox.SetActive(true);
	}

	public void OnConfirmLogout()
    {
		SetPlayerData(PlayerPrefs.GetInt("UNLOCKED TOPICS"), 
			PlayerPrefs.GetInt("LESSONS COMPLETED"), 
			PlayerPrefs.GetInt("UNLOCKED QUIZZES"));

		PlayFabClientAPI.ForgetAllCredentials();
		Debug.Log("Successfully logged out!");
		PlayerPrefs.SetString("USERNAME", null);
		PlayerPrefs.SetString("PASSWORD", null);
		PlayerPrefs.SetString("ISLOGGEDIN", "False");
		profilePanel.SetActive(false);

		SceneManager.LoadScene("Menu Scene");
	}

	public void OnCancel()
    {
		dialogBox.SetActive(false);
    }

	public void OnCloseDialogBox()
    {
		if (PlayerPrefs.GetString("ISLOGGEDIN") == "False")
        {
			dialogBox.SetActive(false);
        }
		else
        {
			SceneManager.LoadScene("Pathway Scene");
		}
	}

	#endregion Settings

	#region PlayerData

	public void GetPlayerData()
	{
		PlayFabClientAPI.GetUserData(new GetUserDataRequest()
		{
			PlayFabId = userID,
			Keys = null,
		}, UserDataSuccess, OnError);
	}

	void UserDataSuccess(GetUserDataResult result)
	{
		if (result.Data.ContainsKey("Topics Unlocked")) 
		{
			PlayerPrefs.SetInt("UNLOCKED TOPICS", int.Parse(result.Data["Topics Unlocked"].Value));
		}
		
		if (result.Data.ContainsKey("Lessons Completed"))
		{
			PlayerPrefs.SetInt("LESSONS COMPLETED", int.Parse(result.Data["Lessons Completed"].Value));
		}

		if (result.Data.ContainsKey("Quizzes Unlocked"))
        {
			PlayerPrefs.SetInt("UNLOCKED QUIZZES", int.Parse(result.Data["Quizzes Unlocked"].Value));
        }

		if (result.Data.ContainsKey("Quiz 1 Score"))
		{
			PlayerPrefs.SetInt("QUIZ 1 SCORE", int.Parse(result.Data["Quiz 1 Score"].Value));
		}

		if (result.Data.ContainsKey("Quiz 2 Score"))
		{
			PlayerPrefs.SetInt("QUIZ 2 SCORE", int.Parse(result.Data["Quiz 2 Score"].Value));
		}

		if (result.Data.ContainsKey("Quiz 3 Score"))
		{
			PlayerPrefs.SetInt("QUIZ 3 SCORE", int.Parse(result.Data["Quiz 3 Score"].Value));
		}
	}

	public void SetPlayerData(int topicsUnlocked, int lessonsCompleted, int quizzesUnlocked)
	{
		// Update user data in PlayFab
		PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
		{
			Data = new System.Collections.Generic.Dictionary<string, string>()
			{
				{"Topics Unlocked", topicsUnlocked.ToString() },
				{"Lessons Completed", lessonsCompleted.ToString() },
				{"Quizzes Unlocked", quizzesUnlocked.ToString() }
			}
		}, SetDataSuccess, OnError);
	}

	
	public void SetQuiz1Score(int score)
    {
		PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
		{
			Data = new System.Collections.Generic.Dictionary<string, string>()
			{
				{"Quiz 1 Score", score.ToString() }
			}
		}, SetDataSuccess, OnError);
	}

	public void SetQuiz2Score(int score)
	{
		PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
		{
			Data = new System.Collections.Generic.Dictionary<string, string>()
			{
				{"Quiz 2 Score", score.ToString() }
			}
		}, SetDataSuccess, OnError);
	}

	public void SetQuiz3Score(int score)
	{
		PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
		{
			Data = new System.Collections.Generic.Dictionary<string, string>()
			{
				{"Quiz 3 Score", score.ToString() }
			}
		}, SetDataSuccess, OnError);
	}
	

	void SetDataSuccess(UpdateUserDataResult result)
	{
		Debug.Log("Player data updated!");
	}
	#endregion PlayerData

	public void SetPlayerProfile()
	{
		PlayerPrefs.SetString("USERNAME", username);
		PlayerPrefs.SetString("PASSWORD", userPassword);
		PlayerPrefs.SetString("ISLOGGEDIN", "True");

		userIDText.text = "User ID: " + userID;
		usernameText.text = username;
		lessonsCompletedText.text = PlayerPrefs.GetInt("LESSONS COMPLETED").ToString();
		topicsUnlockedText.text = (PlayerPrefs.GetInt("UNLOCKED TOPICS") + 1) + "/11";
	}

	void OnError(PlayFabError error)
	{
		Debug.LogError(error.GenerateErrorReport());
	}
}
