using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {


    //射线碰撞
    private RaycastHit2D[] raycast;
    //碰撞的物体个数
    private int hitNumbers = 0;

    //箱子移动
    public bool Move(string dir)
    {
        if(dir == "Up")
        {
            ClearRayCast();
            RayHit("Up");
            if(CanMove())
            {
                transform.Translate(new Vector2(0, 1));
                return true;
            }
            
        }
        else if(dir == "Down")
        {
            ClearRayCast();
            RayHit("Down");
            if (CanMove())
            {
                transform.Translate(new Vector2(0, -1));
                return true;
            }
        }
        else if(dir == "Left")
        {
            ClearRayCast();
            RayHit("Left");
            if (CanMove())
            {
                transform.Translate(new Vector2(-1, 0));
                return true;
            }
        }
        else if(dir == "Right")
        {
            ClearRayCast();
            RayHit("Right");
            if (CanMove())
            {
                transform.Translate(new Vector2(1, 0));
                return true;
            }
        }
        
        return false;
    }

    //射线检测
    private void RayHit(string dir)
    {
        Vector2 rayRightPos = new Vector2(transform.position.x + 0.6f, transform.position.y);
        Vector2 rayLeftPos = new Vector2(transform.position.x - 0.6f, transform.position.y);
        Vector2 rayUpPos = new Vector2(transform.position.x, transform.position.y + 0.6f);
        Vector2 rayDownPos = new Vector2(transform.position.x, transform.position.y - 0.6f);
        hitNumbers = 0;
        //获取左边3个单位的碰撞
        if (dir == "Right")
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
            hitNumbers = Physics2D.RaycastNonAlloc(rayDownPos, new Vector2(0, -1), raycast, 0.5f);
        }


    }
    //是否可以行走
    private bool CanMove()
    {

        //只要没物体，就可以行走
        if (hitNumbers == 0)
        {
            return true;
        }
       else if(hitNumbers == 1)
        {
            if (raycast[0].transform.gameObject.tag == "End")
                return true;
            else if (raycast[0].transform.gameObject.tag == "Box")
                return false;
            else if (raycast[0].transform.gameObject.tag == "Wall")
                return false;
        }
        return true;

    }
    //清除Raycast信息
    private void ClearRayCast()
    {
        
        raycast = new RaycastHit2D[4];
    }
}
