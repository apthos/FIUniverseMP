using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace _Scripts {
    public static class HttpClient
    {

        private static string endpoint = "https://fiuvrserver.herokuapp.com/user/";
        
        public static async Task<T> Get<T>(string username) {
            var getRequest = CreateRequest(endpoint + username);
            getRequest.SendWebRequest();
            
            while (!getRequest.isDone) await Task.Delay(10);
            return JsonUtility.FromJson<T>(getRequest.downloadHandler.text);
        } 
        
        public static async Task<T> Post<T>(string username) {
            var postRequest = CreateRequest($"{endpoint}{username}", RequestType.POST);
            postRequest.SendWebRequest();
            
            while (!postRequest.isDone) await Task.Delay(10);
            return JsonUtility.FromJson<T>(postRequest.downloadHandler.text);
        }

        public static async Task<T> UpdateInventory<T>(string username, int item, int quantity)
        {
            var putRequest = CreateRequest($"{endpoint}inventory/{username}?item={item}&quantity={quantity}", RequestType.PUT);
            putRequest.SendWebRequest();
            
            while (!putRequest.isDone) await Task.Delay(10);
            return JsonUtility.FromJson<T>(putRequest.downloadHandler.text);
        }

        public static async Task<T> UpdatePreferences<T>(string username, int head, int skin, int face, int hair,
            int color)
        {
            var putRequest =
                CreateRequest(
                    $"{endpoint}preferences/{username}?head={head}&skin={skin}&face={face}&hair={hair}&color={color}", RequestType.PUT); 
            putRequest.SendWebRequest();

            while (!putRequest.isDone) await Task.Delay(10);
            return JsonUtility.FromJson<T>(putRequest.downloadHandler.text);
        }

        private static UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null) {
            var request = new UnityWebRequest(path, type.ToString());

            if (data != null) {
                var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            return request;
        }
    }

    public enum RequestType {
        GET = 0,
        POST = 1,
        PUT = 2
    }
}