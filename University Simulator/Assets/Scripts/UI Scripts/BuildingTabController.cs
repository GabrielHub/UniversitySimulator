using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTabController : MonoBehaviour
{
	public Text description;
	public Text enabledText;
	public GameObject container;

    // Start is called before the first frame update
    void Start()
    {
        container.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.instance.state == GameState.State.MidGame) {
        	enabledText.text = "";
        	container.SetActive(true);
        }
    }
}
