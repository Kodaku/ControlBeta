using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraSpeedSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;
    [SerializeField] private float maxCameraSpeed;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(delegate { HandleSliderValueChange(); });
    }

    private void HandleSliderValueChange()
    {
        // float newCameraSpeed = (slider.value) * 100.0f / maxCameraSpeed + 70.0f;
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = slider.value;
        // print(cinemachineFreeLook.m_XAxis.m_MaxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
