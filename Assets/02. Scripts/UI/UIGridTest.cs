//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// All children added to the game object with this script will be repositioned to be on a grid of specified dimensions.
/// If you want the cells to automatically set their scale based on the dimensions of their content, take a look at UITable.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/GridTest")]
public class UIGridTest : MonoBehaviour
{
	public enum Arrangement
	{
		Horizontal,
		Vertical,
	}

	public Arrangement arrangement = Arrangement.Horizontal;
	public int maxPerLine = 0; // 한 줄 당 최대 아이템 수 
	public float cellWidth = 200f; // 각 셀 너비
	public float cellHeight = 200f; // 각 셀 높이 
	public bool repositionNow = false; 
	public bool sorted = false;
	public bool hideInactive = true; // 비활성화된 오브젝트 숨김 여부 (true!)

	bool mStarted = false;

	void Start ()
	{
		mStarted = true;
		Reposition(); // 초기 위치 지정
	}

	void Update ()
	{
		if (repositionNow)
		{
			repositionNow = false;
			Reposition();
		}
	}

	static public int SortByName (Transform a, Transform b) { return string.Compare(a.name, b.name); }

	/// <summary>
	/// Recalculate the position of all elements within the grid, sorting them alphabetically if necessary.
	/// </summary>
	// 그리드 내의 모든 요소 위치를 재계산
	public void Reposition ()
	{
		if (!mStarted)
		{
			repositionNow = true;
			return;
		}

		Transform myTrans = transform;

		int x = 0;
		int y = 0;

		// 정렬이 필요한 경우
		if (sorted)
		{
			List<Transform> list = new List<Transform>();

			// 활성화된 자식 오브젝트만 리스트에 추가 
			for (int i = 0; i < myTrans.childCount; ++i)
			{
				Transform t = myTrans.GetChild(i);
				if (t && (!hideInactive || NGUITools.GetActive(t.gameObject))) list.Add(t);
			}
			list.Sort(SortByName); // 알파벳 순으로 정렬

			// 정렬된 리스트의 각 요소를 그리드에 배치 
			for (int i = 0, imax = list.Count; i < imax; ++i)
			{
				Transform t = list[i];

				if (!NGUITools.GetActive(t.gameObject) && hideInactive) continue;

				float depth = t.localPosition.z;
				t.localPosition = (arrangement == Arrangement.Horizontal) ?
					new Vector3(cellWidth * x, -cellHeight * y, depth) :
					new Vector3(cellWidth * y, -cellHeight * x, depth);

				if (++x >= maxPerLine && maxPerLine > 0)
				{
					x = 0;
					++y;
				}
			}
		}
		// 정렬이 필요없는 경우
		else
		{
			// 모든 자식 오브젝트를 순서대로 그리드에 배치
			for (int i = 0; i < myTrans.childCount; ++i)
			{
				// 자식 오브젝트 위치 가져오기 
				Transform t = myTrans.GetChild(i);

				// 비활성화 오브젝트 처리 (hideInactive - true일 때)
				if (!NGUITools.GetActive(t.gameObject) && hideInactive) continue;

				float depth = t.localPosition.z; // z값 저장
				// 위치 설정 
				t.localPosition = (arrangement == Arrangement.Horizontal) ?
					// Horizontal : (cellWidth * x, -cellHeight * y)
					new Vector3(cellWidth * x, -cellHeight * y, depth) :
					// Vertical : (cellWidth * y, -cellHeight * x) 
					new Vector3(cellWidth * y, -cellHeight * x, depth);

				if (++x >= maxPerLine && maxPerLine > 0)
				{
					x = 0;
					++y;
				}
			}
		}

		UIDraggablePanel drag = NGUITools.FindInParents<UIDraggablePanel>(gameObject);
		if (drag != null) drag.UpdateScrollbars(true);
	}
}