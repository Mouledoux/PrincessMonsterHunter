using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class TimeDisplay : MonoBehaviour
{
    TMPro.TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = System.DateTime.Now.Hour.ToString("00") + ":" + System.DateTime.Now.Minute.ToString("00");
    }
}
