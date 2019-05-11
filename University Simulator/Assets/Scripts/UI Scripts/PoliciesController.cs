using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PoliciesController : MonoBehaviour
{
	public GameObject sliderContainer;
	public TMP_Text descriptionText;

    // Start is called before the first frame update
    void Start()
    {
        descriptionText.text = "Keep growing the University to unlock Policies";
        sliderContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.instance.state == GameManagerScript.GameState.EarlyGame4) {
        	descriptionText.text = "Each slider can increase a resource, at the expense of Happiness or Renown";
        	sliderContainer.SetActive(true);
        }
    }
}
