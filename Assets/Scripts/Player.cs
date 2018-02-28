using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //射线碰撞
    private RaycastHit2D[] raycast;
    //碰撞的物体个数
    private int hitNumbers = 0;
    //是否能移动
    public bool allReached = false;
    void Start () {

        raycast = new RaycastHit2D[4];
	}


    void Update () {
        //没有胜利，继续移动
        if(allReached == false)
            Move();

	}
    //移动
    private void Move()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            ClearRayCast();
            RayHit("Up");
            if(CanMove("Up"))
                transform.Translate(new Vector3(0, 1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ClearRayCast();
            RayHit("Down");
            if (CanMove("Down"))
                transform.Translate(new Vector3(0, -1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ClearRayCast();
            RayHit("Left");
            if (CanMove("Left"))
                transform.Translate(new Vector3(-1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ClearRayCast();
            RayHit("Right");
            if (CanMove("Right"))
                transform.Translate(new Vector3(1, 0, 0));
        }
    }
    //射线检测
    private void RayHit(string dir)
    {
        Vector2 rayRightPos = new Vector2(transform.position.x + 0.6f, transform.position.y);
        Vector2 rayLeftPos = new Vector2(transform.position.x - 0.6f, transform.position.y);
        Vector2 rayUpPos = new Vector2(transform.position.x , transform.position.y + 0.6f);
        Vector2 rayDownPos = new Vector2(transform.position.x, transform.position.y - 0.6f);
        hitNumbers = 0;
        //获取左边3个单位的碰撞
        if(dir == "Right")
        {
            hitNumbers = Physics2D.RaycastNonAlloc(rayRightPos, new Vector2(1, 0), raycast, 0.5f);
        }
        else if (dir == "Left")
        {
            hitNumbers = Physics2D.RaycastNonAlloc(rayLeftPos, new Vector2(-1, 0), raycast, 0.5f);
        }
        else if (dir == "Up")
        {
            hitNumbers = Physics2D.RaycastNonAlloc(rayUpPos, new Vector2(0, 1), raycast, 0.5f);
        }
        else if (dir == "Down")
        {
            hitNumbers = Physics2D.RaycastNonAlloc(rayDownPos, new Vector2(0, -1), raycast, 0.5f );
        }

        
    }
    //是否可以行走
    private bool CanMove(string dir)
    {

        if (hitNumbers == 1)
        {
            //检测到是墙，不移动
            if (raycast[0].transform.gameObject.tag == "Wall")
            {
                return false;
            }
            else if (raycast[0].transform.gameObject.tag == "Box")
            {
                //调用Box的射线判断,箱子能移动就返回True
                if (raycast[0].transform.GetComponent<Box>().Move(dir))
                    return true;
                else
                    return false;
            }
        }
        else if (hitNumbers == 0)
        {
            return true;
        }

        return true;
        
    }
    //清除Raycast信息
    private void ClearRayCast()
    {
        
        raycast = new RaycastHit2D[4];
    }

}
