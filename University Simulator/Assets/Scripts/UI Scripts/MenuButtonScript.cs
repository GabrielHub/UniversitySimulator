using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuButtonScript : MonoBehaviour
{
	public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(HandleClick);
    }

    void HandleClick() {
    	SceneManager.LoadScene(0);
    }
}
