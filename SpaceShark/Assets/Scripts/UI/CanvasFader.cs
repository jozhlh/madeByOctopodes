using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFader : MonoBehaviour {

	[SerializeField]
	private bool m_startsVisible = false;
	[SerializeField]
	private bool m_fadeOnAwake = false;
	[SerializeField]
	private bool m_continuous = false;
	[SerializeField]
	private float m_fadeSpeed = 1.0f;
	[SerializeField]
	private float m_minAlpha = 0;
	[SerializeField]
	private float m_maxAlpha = 1.0f;


	// ********************************************************************
	// Private Data Members 
	// ********************************************************************
	private CanvasRenderer m_canvas = null;


	// ********************************************************************
	// Properties 
	// ********************************************************************
	public bool isVisible {
		get { 
			if (m_canvas.GetAlpha() == m_maxAlpha)
				return true;
			else return false;
		}
	}
	public bool isHidden {
		get { 
			if (m_canvas.GetAlpha() == m_minAlpha)
				return true;
			else return false;
		}
	}
	public float fadeSpeed {
		get { return m_fadeSpeed; }
		set { m_fadeSpeed = value; }
	}
	public float minAlpha {
		get { return m_minAlpha; }
		set { m_minAlpha = value; }
	}
	public float maxAlpha {
		get { return m_maxAlpha; }
		set { m_maxAlpha = value; }
	}
	public bool continuous {
		get { return m_continuous; }
		set { m_continuous = value; }
	}

	void Start () {
		m_canvas = GetComponent<CanvasRenderer>();
		if (m_canvas == null)
		{
			Debug.LogError("FadeSprite: No CanvasRenderer found!");
			return;
		}
		
		
		if (m_startsVisible)
		{
			//float canvasAlpha = m_canvas.GetAlpha();
			//canvasAlpha = m_maxAlpha;
			m_canvas.SetAlpha(m_maxAlpha);
			if (m_fadeOnAwake)
				StartCoroutine(FadeOut());
		}
		else
		{
			//Color spriteColor = m_sprite.color;
			//spriteColor.a = m_minAlpha;
			//m_sprite.color = spriteColor;
			m_canvas.SetAlpha(m_minAlpha);
			if (m_fadeOnAwake)
				StartCoroutine(FadeIn());
		}
	}

	public IEnumerator FadeIn()
	{
		//Color spriteColor = m_sprite.color;
		float canvasAlpha = m_canvas.GetAlpha();

		while (canvasAlpha < m_maxAlpha)
		{
			yield return null;
			canvasAlpha += m_fadeSpeed * Time.deltaTime;
			m_canvas.SetAlpha(canvasAlpha);
		}

		//spriteColor.a = m_maxAlpha;
		//m_sprite.color = spriteColor;
		m_canvas.SetAlpha(m_maxAlpha);

		if (m_continuous)
			StartCoroutine(FadeOut());
	}

	public IEnumerator FadeOut()
	{
		//Color spriteColor = m_sprite.color;
		float canvasAlpha = m_canvas.GetAlpha();

		while (canvasAlpha > m_minAlpha)
		{
			yield return null;
			canvasAlpha -= m_fadeSpeed * Time.deltaTime;
			m_canvas.SetAlpha(canvasAlpha);
		}
		//spriteColor.a = m_minAlpha;
		//m_sprite.color = spriteColor;
		m_canvas.SetAlpha(m_minAlpha);
		m_canvas.gameObject.SetActive(false);
		
		if (m_continuous)
			StartCoroutine(FadeIn());
	}
}
