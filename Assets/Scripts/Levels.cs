﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
	[SerializeField]
	private GameObject m_Canvas;
	[SerializeField]
	private GameObject m_Prefab;
	[SerializeField]
	private Vector2 m_Start;
	[SerializeField]
	private Vector2 m_End;
	[SerializeField]
	private Vector2Int m_Grid;
	private int m_Levels = (int)Scenes.LevelLast - (int)Scenes.Level1 + 1;

    private void Start()
    {
		int lvl = 1;
		for (uint y = 0; y < m_Grid.y; y++)
			for (uint x = 0; x < m_Grid.x; x++, lvl++)
            {
				if (lvl > m_Levels)
					return;
				GameObject obj = Instantiate(m_Prefab, m_Canvas.transform);
				obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Lerp(m_Start.x, m_End.x, (float)x / m_Grid.x), Mathf.Lerp(m_Start.y, m_End.y, (float)y / m_Grid.y));
				obj.transform.Find("LevelButton").Find("Number").GetComponent<Text>().text = lvl.ToString();
				obj.transform.Find("Time").GetComponent<Text>().text = GetTime(lvl).ToString("n2");
				AddListener(obj.transform.Find("LevelButton").gameObject, lvl);
			}
	}

	private float GetTime(int lvl) =>
		PlayerPrefs.GetFloat($"Level{lvl}.Time", float.NaN);

	private void AddListener(GameObject obj, int lvl) =>
		obj.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.GotoLevel(lvl - 1));
}