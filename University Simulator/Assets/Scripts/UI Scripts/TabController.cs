using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
	public ToggleGroup tg;
	public Image img;

    // Update is called once per frame
    void Update()
    {
        if (tg.AnyTogglesOn() == false) {
        	img.enabled = false;
        }
        else {
        	img.enabled = true;
        }
    }
}
