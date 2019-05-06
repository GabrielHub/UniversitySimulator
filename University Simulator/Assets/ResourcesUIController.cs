using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcesUIController : MonoBehaviour {
    private Resources resources {
        get {
            return GameManagerScript.instance.resources;
        }
    }

    private TextMeshProUGUI studentsText;
    private TextMeshProUGUI wealthText;
    private TextMeshProUGUI buildingsText;
    private TextMeshProUGUI alumniText;
    private TextMeshProUGUI facultyText;

    // Start is called before the first frame update
    void Start() {
        this.wealthText = this.transform.Find("Wealth").GetComponentInChildren<TextMeshProUGUI>();
        this.studentsText = this.transform.Find("Students").GetComponentInChildren<TextMeshProUGUI>();
        this.buildingsText = this.transform.Find("Buildings").GetComponentInChildren<TextMeshProUGUI>();
        this.alumniText = this.transform.Find("Alumni").GetComponentInChildren<TextMeshProUGUI>();
        this.facultyText =this.transform.Find("Faculty").GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        this.wealthText.text = this.resources.wealth.ToString();
        this.studentsText.text = this.resources.students.ToString();
        this.buildingsText.text = this.resources.buildingCount.ToString();
        this.alumniText.text = this.resources.alumni.ToString();
        this.facultyText.text = this.resources.faculty.ToString();
    }
}
