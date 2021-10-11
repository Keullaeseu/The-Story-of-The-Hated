using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
	private float _fpsMeasurePeriod;
	private float _fpsNextPeriod;
	private int _fpsAccumulator;
	private int _currentFps;

	private TMP_Text m_Text;

    private void Start()
    {
        m_Text = GetComponent<TMP_Text>();
		_fpsNextPeriod = Time.realtimeSinceStartup + _fpsMeasurePeriod;
	}

	private void OnEnable()
	{
		_fpsMeasurePeriod = 0.5f;
		_fpsNextPeriod = 0f;
		_fpsAccumulator = 0;
		_currentFps = 0;
	}

	private void Update()
	{
		if (!m_Text.enabled)
		{
			return;
		}

		// Measure average frames per second
		_fpsAccumulator++;
		if (Time.realtimeSinceStartup > _fpsNextPeriod)
		{
			_currentFps = (int)(_fpsAccumulator / _fpsMeasurePeriod);
			_fpsAccumulator = 0;
			_fpsNextPeriod += _fpsMeasurePeriod;
			m_Text.text = _currentFps.ToString();
		}
	}
}
