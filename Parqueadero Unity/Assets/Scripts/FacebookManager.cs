using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public class FacebookManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake () {
		
		if(!FB.IsInitialized){
			FB.Init();
		
	}else{

		FB.ActivateApp();
		
		}
	}

   public void LogIn () {

	   FB.LogInWithReadPermissions(callback:OnLogIn);
		
	}


	private void OnLogIn(ILoginResult result) {

		if(FB.IsLoggedIn){

			AccessToken token = AccessToken.CurrentAccessToken;
			print("usuario"+ token.UserId);
		
	}else{

		print("Canceled");
		
		}

	   
		
	}


}
