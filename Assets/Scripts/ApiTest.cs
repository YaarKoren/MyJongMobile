using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using TMPro;  // TextMeshProUGUI

public class ApiTest : MonoBehaviour {
    public TextMeshProUGUI resultText; 

    private string baseUrl = "http://192.168.1.37:8000";

    // This function will be called by the button's OnClick event
    public void OnPingButtonPressed() {
        StartCoroutine(SayHi());
    }

    IEnumerator SayHi() {
        resultText.text = "Loading...";

        string jsonBody = "{\"message\":\"hi from unity!\"}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest request = new UnityWebRequest(baseUrl + "/hi", "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success) {
            resultText.text = "Response: " + request.downloadHandler.text;
        } else {
            resultText.text = "Error: " + request.error;
        }
    }
}