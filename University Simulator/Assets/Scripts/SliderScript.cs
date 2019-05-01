using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
	public Text valueText;
	private Slider slider;

    // Start is called before the first frame update
    void Start()
    {   
    	slider = GetComponent<Slider> ();
    }

    void FixedUpdate() {
    	valueText.text = ((int)slider.value).ToString();
    }
}
