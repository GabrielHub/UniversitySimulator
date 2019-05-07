using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class CustomNumString {
    public static string ToAbbreviatedString(this int value, int decimals = 1) {
        if (value < 1_000) return value.ToString();
        return ((float) value).ToAbbreviatedString(decimals);
    }

    public static string ToAbbreviatedString(this float value, int decimals = 1) {
        string unit = "";
        float denom = 1;
        if (value >= 1_000_000_000) {
            unit = "b";
            denom = 1_000_000_000;
        } else if (value >= 1_000_000) {
            unit = "m";
            denom = 1_000_000;
        } else if (value >= 1_000) {
            unit = "k";
            denom = 1_000;
        } else {
            return value.ToString();
        }
        float precision = Mathf.Pow(10f, decimals);
        return Mathf.Round(value / denom * precision) / precision  + unit;
    }
}

public class StatTextController : MonoBehaviour {
    public StatController stat;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start() {
        this.text = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        this.text.text = this.stat.value.ToAbbreviatedString(decimals: 1);
    }
}
