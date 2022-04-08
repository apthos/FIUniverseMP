using System;
using System.Threading.Tasks;
using SharedLibrary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts
{
    public class HttpManager : MonoBehaviour
    {
        public GameObject TwoDtextInput;
        public GameObject VRTextInput;
        public string username;
        public int[] inventory = new int[8];
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public async void LoginButton(string username)
        {
            User user;
            try
            {
                user = await HttpClient.Post<User>(username);
                Debug.Log(user);
            }
            catch (Exception e) // fix way to check exception 
            {
                user = await HttpClient.Get<User>(username);
                Debug.Log(user);
            }

            this.username = user.username;
            SceneManager.LoadScene("Dorm");
        }

        public void LoginAction()
        {
            Debug.Log("button clicked");
            string text = TwoDtextInput.GetComponent<TMP_InputField>().text;

            LoginButton(text);
            Debug.Log(text);
        }

        public void VRLoginAction()
        {
            string text = VRTextInput.GetComponent<TMP_InputField>().text;
            //LoginButton(text);
            Debug.Log(text);
        }
    }
}