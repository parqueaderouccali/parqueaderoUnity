using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class FirebaseScript : MonoBehaviour {

	Firebase.Auth.FirebaseAuth auth;
	Firebase.Auth.FirebaseUser user;

	public InputField EmailAddress, Password, PasswordValidate, Names , SecondNames;

	string displayName;
	string emailAddress;



	// Use this for initialization
	void Start () {
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

	}
	
	// Update is called once per frame
	void Update () {
		
	}



 public	void InitializeFirebase() {
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		auth.StateChanged += AuthStateChanged;
		AuthStateChanged(this, null);
	}

	void AuthStateChanged(object sender, System.EventArgs eventArgs) {
		if (auth.CurrentUser != user) {
			bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
			if (!signedIn && user != null) {
				print("Signed out " + user.UserId);
			}
			user = auth.CurrentUser;
			if (signedIn) {
				print("Signed in " + user.UserId + user.Email + user.DisplayName);
				displayName = user.DisplayName ?? "";
				emailAddress = user.Email ?? "";
			}
		}
	}

//Ingresar info del usuario
	public void ingresarInfolUser(){
		
		user = auth.CurrentUser;
			if (user != null) {
			Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile {
				DisplayName = "Jane Q. User"
				
			};
			user.UpdateUserProfileAsync(profile).ContinueWith(task => {
				if (task.IsCanceled) {
				Debug.LogError("UpdateUserProfileAsync was canceled.");
				return;
				}
				if (task.IsFaulted) {
				Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
				return;
				}
			

   			});
		}

	}

	public void LoginButtonPressed()
	{

		//FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(EmailAddress.text, Password.text).
		//ContinueWith((obj) =>
		//{
		//	SceneManager.LoadSceneAsync("LoggedInScene");
		//});


		auth.SignInWithEmailAndPasswordAsync(EmailAddress.text, Password.text).ContinueWith(task => {
			if (task.IsCanceled) {
				Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
				return;
			}

			Firebase.Auth.FirebaseUser newUser = task.Result;
			Debug.LogFormat("User signed in successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);
				SceneManager.LoadSceneAsync("LoggedInScene");
		});

	}




//Crear usuario
//-Correo electronico para activar cuenta
	public void CreateNewUserButtonPressed()
	{
		if(String.Equals (Password.text,PasswordValidate.text)){
		auth.CreateUserWithEmailAndPasswordAsync(EmailAddress.text, Password.text).ContinueWith(task => {
			if (task.IsCanceled) {
				Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
				return;
			}
			
			if(!task.IsCanceled){
				
			   ClearInputsFields();
				
			}
			// Firebase user has been created.
			Firebase.Auth.FirebaseUser newUser = task.Result;
			Debug.LogFormat("Firebase user created successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);
 
			
			
		});

		} else {
			
			print("Las contraseñas no son iguales" + Password.text+ "," + PasswordValidate.text);

		}

	}







 //Limpiar los inputs despues registrar
	public void ClearInputsFields(){
			
		EmailAddress.text=null;
		Password.text=null;
		PasswordValidate.text=null;
		Names.text=null;
		SecondNames.text=null;

	}

 //Restablecer contraseña
	public void SendPasswordResetEmail() {

		string emailAddress = "warhammerjj28@gmail.com";


		auth.SendPasswordResetEmailAsync(emailAddress).ContinueWith((obj) => {
				if (obj.IsCanceled) {
					Debug.LogError("SendPasswordResetEmailAsync was canceled.");
					return;
				}
				if (obj.IsFaulted) {
					Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + obj.Exception);
					return;
				}

				Debug.Log("Password reset email sent successfully.");
			});
		}



		public void ChangeSceneToMainMenu() {

				SceneManager.LoadScene("LoginScene");

		}	
		
		
		public void ChangeSceneToRegisterMenu() {

				SceneManager.LoadScene("RegisterUser");

		}

		public void ChangeSceneToResetPassMenu() {

				SceneManager.LoadScene("ResetPass");

		}


	}
	
