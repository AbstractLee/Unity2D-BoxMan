using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    /// <summary>
    /// 游戏管理
    /// </summary>
    //玩家
    private GameObject player;
    //箱子
    private GameObject[] boxs;
    //终点
    private GameObject[] ends;
    //存储Box位置
    public List<Vector2> boxList;
    //存储End位置
    public List<Vector2> endList;
    private bool isContain = false;
    //下一关按钮
    public GameObject nextBtn;
    
    void Start () {
        //获取玩家
        player = GameObject.FindGameObjectWithTag("Player");
        //获取箱子
        boxs = GameObject.FindGameObjectsWithTag("Box");
        //获取终点
        ends = GameObject.FindGameObjectsWithTag("End");

        //获取下一关按钮
        nextBtn = GameObject.Find("Canvas/ControlPanel/NextBtn");
        nextBtn.GetComponent<Button>().enabled = false;

        //获取所有终点的位置
        for (int i = 0; i < ends.Length; i++)
            endList.Add(ends[i].transform.position);
	}
	
	// Update is called once per frame
	void Update () {

        boxList.Clear();
        if(!isContain)
            GetBoxPos();
        //判断所有箱子是否全部到达ends
        Contains();
        if (isContain)
        {
            //完成
            if(player != null)
                player.transform.GetComponent<Player>().allReached = true;
            if (nextBtn.activeSelf == false)
                nextBtn.SetActive(true);
            if (nextBtn.GetComponent<Button>().enabled == false)
                nextBtn.GetComponent<Button>().enabled = true;

        }
	}
    //获取所有箱子的位置
    public void GetBoxPos()
    {
        for (int i = 0; i < boxs.Length; i++)
            boxList.Add(boxs[i].transform.position);
    }
    //判断是否所有箱子都在终点上
    private void Contains()
    {
        int i = 0;
        foreach(Vector2 item in endList)
        {
            if (boxList.Contains(item))
                i++;
        }
        if (i == endList.Count)
            isContain = true;
        else
            isContain = false;
    }
}
