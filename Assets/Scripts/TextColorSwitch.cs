using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorSwitch : MonoBehaviour
{
    private Text ammoText = null;
    private int randomColor = 0;

    private void Start()
    {
        ammoText = GetComponent<Text>();
        if(ammoText == null)
        {
            Debug.LogError("ammoText is equal to NULL");
        }
    }
    // Update is called once per frame
    void Update()
    {
        randomColor = UnityEngine.Random.Range(0, 4);

        switch (randomColor)
        {
            case 0:
                {
                    ammoText.color = Color.red;
                    break;
                }
            case 1:
                {
                    ammoText.color = Color.green;
                    break;
                }
            case 2:
                {
                    ammoText.color = Color.blue;
                    break;
                }
            case 3:
                {
                    ammoText.color = Color.yellow;
                    break;
                }
            default:
                {
                    ammoText.color = Color.red;
                    break;
                }
        }
    }
}
