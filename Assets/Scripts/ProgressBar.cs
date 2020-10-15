using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ProgressBar : MonoBehaviour
{
    private UnityEngine.UI.Image slider = null;

    [SerializeField]
    private float fillRate = .1f;
    private float fillNext = -1f;
    private float fillAmount = .02f;
    private float targetProgress = 1.0f;

    [SerializeField]
    private bool decrementFill = false;

    [SerializeField]
    private RectTransform particleSystemContainer = null;

    private void Awake()
    {
        slider = GetComponent<UnityEngine.UI.Image>();
        if(slider == null)
        {
            Debug.LogError("slider is equal to NULL");
        }

        if(particleSystemContainer == null)
        {
            Debug.LogError("Particle System Container is equal to NULL");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > fillNext)
        {
            if (decrementFill)
            {
                targetProgress = 0.0f;
                DecrementProgress();
            }
            else
            {
                targetProgress = 1.0f;
                IncrementProgress();
            }
        }
    }

    private void DecrementProgress()
    {
        fillNext = Time.time + fillRate;
        if (slider.fillAmount > targetProgress)
        {
            slider.fillAmount -= fillAmount;
            Mathf.Clamp(slider.fillAmount, 0.0f, 1.0f);
            particleSystemContainer.transform.Translate(new Vector3(-.65f, 0, 0));
        }
    }

    private void IncrementProgress()
    {
        fillNext = Time.time + fillRate;

        if (slider.fillAmount < targetProgress)
        {
            slider.fillAmount += fillAmount;
            Mathf.Clamp(slider.fillAmount, 0.0f, 1.0f);
            particleSystemContainer.transform.Translate(new Vector3(.65f, 0, 0));
        }
    }

    public void DecrementProgressBar(bool value)
    {
        decrementFill = value;
    }

    public float SliderValue()
    {
        return slider.fillAmount;
    }
}
