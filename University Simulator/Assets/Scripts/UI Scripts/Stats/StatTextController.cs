using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatTextController : MonoBehaviour {
    public StatController stat;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        this.text = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        string unit = "";
        float denom = 1;
        if (this.stat.value >= 1_000_000_000) {
            unit = "b";
            denom = 1_000_000_000;
        } else if (this.stat.value >= 1_000_000) {
            unit = "m";
            denom = 1_000_000;
        } else if (this.stat.value >= 1_000) {
            unit = "k";
            denom = 1_000;
        } else {
            this.text.text = this.stat.value.ToString();
            return;
        }
        int decimals = 1;
        float precision = Mathf.Pow(10f, decimals);
        this.text.text = Mathf.Round(this.stat.value / denom * precision) / precision  + unit;
    }
}
