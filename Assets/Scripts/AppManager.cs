using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AppManager : MonoBehaviour {

    //开始面板
    private GameObject startPanel;
    //控制面板
    private GameObject controlPanel;
    //游戏关卡
    private int levelCount;
    private GameObject levelObj;
    //关卡预制品
    private GameObject levelPrefab;
    //当前关卡
    private GameObject currentLevel;
    public Text levelTxt;
    public Button nextBtn;
    public Button restBtn;
    public GameObject nextBtnObj;

    void Start()
    {
        //初始化关卡
        levelCount = 0;

        //获取LevelPanel
        levelObj = GameObject.Find("LevelPanel").gameObject;

        //获取StartPanel
        startPanel = GameObject.Find("StartPanel");
        //获取ControlPanel
        controlPanel = GameObject.Find("ControlPanel");

        nextBtnObj = GameObject.Find("Canvas/ControlPanel/NextBtn");

        if (startPanel.activeSelf == false)
            startPanel.SetActive(true);
        if (controlPanel.activeSelf == true)
            controlPanel.SetActive(false);

        //下一关按钮添加监听事件
        nextBtn.onClick.AddListener(delegate () {

            this.LoadLevel();
        });

        //重玩按钮添加监听事件
        restBtn.onClick.AddListener(delegate () {
            levelCount--;
            this.LoadLevel();
        });
    }
	
    //开始游戏
    public void StartGame()
    {
        levelCount++;
        if (startPanel.activeSelf == true)
            startPanel.SetActive(false);
        if (controlPanel.activeSelf == false)
            controlPanel.SetActive(true);
        //加载关卡
        LoadLevel();
        
    }
    //游戏退出
    public void Quit()
    {
        Application.Quit();
    }
    //加载关卡
    public void LoadLevel()
    {
        if (levelCount >= 2)
            levelCount = 2;

        levelTxt.text = "关 卡" + " " + levelCount.ToString();

        //销毁当前关卡
        if (currentLevel != null)
            DestroyImmediate(currentLevel);
        string path = "Prefabs/Level" + levelCount.ToString();
        //生成下一个关卡
        levelPrefab = Resources.Load(path) as GameObject;
        currentLevel = Instantiate(levelPrefab, new Vector2(0, -0.5f), Quaternion.identity, levelObj.transform);
        //下一关按钮不可用
        if (nextBtnObj.activeSelf == true)
            nextBtnObj.SetActive(false);
        levelCount++;
    }

}
