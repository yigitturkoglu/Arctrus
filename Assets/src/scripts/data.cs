using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class data : MonoBehaviour
{
    public RawImage test;
    bool update_start = false;

    private float time = 0.0f;
    public float interpolationPeriod = 2f;

    string labels;
    string table_data;

    int[] values;
    int[] label_list;

    int clock = 0;
    int clock2 = 0;

    void Start()
    {
        var label_number = new List<int>();
        var values_number = new List<int>();

        StartCoroutine(WaitUntilGet());
        values = new int[6];
        label_list = new int[6];
        for(int i = 0; i < label_list.Length; i++)
        {
            label_list[i] = 0;
        }
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            test.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }

    IEnumerator WaitUntilGet()
    {
        yield return new WaitForSeconds(3);
        update_start = true;
    }

    void Update()
    {
        if (update_start)
        {
            time += Time.deltaTime;

            if (time >= interpolationPeriod)
            {
                time = 0.0f;
                int tempvaluelabel = label_list[5];
                List<int> labelValueTemp = label_list.ToList();
                labelValueTemp.Add(tempvaluelabel + 2);
                labelValueTemp.RemoveAt(0);
                label_list = labelValueTemp.ToArray();

                values[0] = values[1];
                values[1] = values[2];
                values[2] = values[3];
                values[3] = values[4];
                values[4] = values[5];
                values[5] = int.Parse(get_data.data_value); 

                labels = $"['{label_list[0].ToString()}','{label_list[1].ToString()}', '{label_list[2].ToString()}', '{label_list[3].ToString()}', '{label_list[4].ToString()}', '{label_list[5].ToString()}']";
                table_data = $"[{values[0].ToString()},{values[1].ToString()},{values[2].ToString()},{values[3].ToString()},{values[4].ToString()},{values[5].ToString()}]";
                string chartUrl = "https://quickchart.io/chart?c=" + "{type:'line',data:{labels:" + labels + ", datasets:[{label:'Value', data: " + table_data + ", fill:false,borderColor:'blue'}]}}";

                StartCoroutine(DownloadImage(chartUrl));
            }
        }
    }
}
