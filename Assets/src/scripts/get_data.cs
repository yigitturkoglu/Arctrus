using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class get_data : MonoBehaviour
{

    public string url = "token";

    string data_value_loc;
    public static string data_value;

    private float time = 0.0f;
    public float interpolationPeriod = 0.5f;

    IEnumerator GetData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError) // Error
            {
                Debug.Log(request.error);
            }
            else // Success
            {
                data_value_loc = request.downloadHandler.text;
                data_value = data_value_loc.Replace('[', ' ').Replace(']', ' ').Replace('"', ' ').Trim();
            }
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time >= interpolationPeriod)
        {
            time = 0.0f;
            StartCoroutine(GetData());
        }
    }
}
