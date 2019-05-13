using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
