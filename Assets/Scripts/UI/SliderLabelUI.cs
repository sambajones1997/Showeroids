using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SliderLabelUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider mSlider;
    TMPro.TextMeshProUGUI mText;
    [SerializeField] string mSuffix = "%";
    [SerializeField] bool bValueAsPercentage = true;
    void Start()
    {
        mText = GetComponent<TMPro.TextMeshProUGUI>();
        if (mText)
        {
            if (mSlider)
            {
                mSlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
            }
            OnSliderValueChanged(); // Get initial value
        }
    }

    private void OnDestroy()
    {
        if (mSlider)
        {
            mSlider.onValueChanged.RemoveListener(delegate { OnSliderValueChanged(); });
        }
    }

    public void OnSliderValueChanged()
    {
        if (mSlider && mText)
        {
            int Value = (int)(bValueAsPercentage ? ((mSlider.value / mSlider.maxValue) * 100.0f) : mSlider.value);
            mText.text = Value.ToString() + mSuffix;
        }
    }
}
