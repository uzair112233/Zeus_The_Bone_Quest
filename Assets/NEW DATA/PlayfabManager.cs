using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour {


	[Header("Windows")]
	public GameObject nameWindow;
	public GameObject leaderboardWindow;

	[Header("Display name window")]
	public GameObject nameError;
	public InputField nameInput;
	
	[Header("Leaderboard")]
	public GameObject rowPrefab;
	public Transform rowsParent;
	
	public Text DisplayNameText;
	
	
	//	public static int CounterID=1;

	// 🎥 Playfab episode #1 - Login with custom ID
	void Start() {
		//CounterID = Random.Range(20,22);//999999999
		Login();
	}

 void Login() {
 	
	 var request = new LoginWithCustomIDRequest 
	 {
		 CustomId = GetCustID.CustID.text  ,
		 CreateAccount = true,
		 InfoRequestParameters = new GetPlayerCombinedInfoRequestParams 
		 {
			GetPlayerProfile = true
         }        
     };

	 

	 PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
	
 }
	void OnLoginSuccess(LoginResult result) {
		Debug.Log("New Account create! " + GetCustID.CustID);
		string name = null;
		if (result.InfoResultPayload.PlayerProfile != null)
			name = result.InfoResultPayload.PlayerProfile.DisplayName;

		if (name == null)
		{
			nameWindow.SetActive(true);
		}
			
		else
		{
			nameWindow.SetActive(false);
		}
			
	}
	
	public void SubmitNameButton() {
		var request = new UpdateUserTitleDisplayNameRequest {
			DisplayName = nameInput.text,
        };
		PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
	}

	void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result) {
		Debug.Log("Updated display name!");
		nameWindow.SetActive(false);
	}





	// 🎥 Playfab episode #2 - Leaderboard (+ UI video)
	public void SendLeaderboard(int score) {
		var request = new UpdatePlayerStatisticsRequest {
			Statistics = new List<StatisticUpdate> {
			new StatisticUpdate {
			StatisticName = "Zeus - The Bone Quest", // <- ✏️ CHANGE YOUR LEADERBOARD NAME HERE!
			Value = score
			//Value = Random.Range(10,100) <- ⭐️ Use this to test out random send data
                }
            }
        };
		PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
	}
	void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result) {
		Debug.Log("Successfull leaderboard sent!");
	}

	public void GetLeaderboard() {
		var request = new GetLeaderboardRequest {
			StatisticName = "Zeus - The Bone Quest", // <- ✏️ CHANGE YOUR LEADERBOARD NAME HERE!
			StartPosition = 0,
			MaxResultsCount = 10
        };
		PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
	}
	void OnLeaderboardGet(GetLeaderboardResult result) {

		foreach (Transform item in rowsParent) {
			Destroy(item.gameObject);
		}

		foreach (var item in result.Leaderboard) {

			GameObject newGo = Instantiate(rowPrefab, rowsParent);
			Text[] texts = newGo.GetComponentsInChildren<Text>();
			texts[0].text = (item.Position + 1).ToString();
			texts[1].text = item.DisplayName;
			texts[2].text = item.StatValue.ToString();

			Debug.Log(string.Format("PLACE: {0} | ID: {1} | VALUE: {2}",
				item.Position, item.PlayFabId, item.StatValue));
		}
	}


	// Error for all Playfab calls
	void OnError(PlayFabError error) {
		DisplayNameText.text = "Name Already Taken";
		Debug.Log("Error while executing Playfab call!");
		Debug.Log(error.GenerateErrorReport());
	}
	
	public void BackButton()
	{
		leaderboardWindow.SetActive(false);
	}
	public void ShowLeaderboardScreen()
	{
		leaderboardWindow.SetActive(true);
	}
	

}
