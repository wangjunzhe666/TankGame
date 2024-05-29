﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    //移动速度
    public float moveSpeed = 50;
    //谁发射的我
    public TankBaseObj fatherObj;

    //特效对象
    public GameObject effObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    //和别人碰撞触发时
    private void OnTriggerEnter(Collider other)
    {
        //子弹 设计到立方体 会爆炸 
        //同样 子弹设计到 不同阵营的对象也应该爆炸
        if( other.CompareTag("Cube") ||
            other.CompareTag("Player") && fatherObj.CompareTag("Monster") ||
            other.CompareTag("Monster") && fatherObj.CompareTag("Player"))
        {
            //判断是否受伤
            //得到碰撞到的对象身上 是否有坦克相关脚本 我们用里氏替换原则
            //通过父类去获取
            TankBaseObj obj = other.GetComponent<TankBaseObj>();
            //判断 对象上 是否挂载了 坦克相关脚本 
            if (obj != null)
                obj.Wound(fatherObj);

            //当子弹销毁时 可以创建一个 爆炸特效
            if(effObj != null)
            {
                //创建爆炸特效
                GameObject eff = Instantiate(effObj, this.transform.position, this.transform.rotation);
                //改音效的音量和开启状态
                AudioSource audioS = eff.GetComponent<AudioSource>();
                //设置大小
                audioS.volume = GameDataMgr.Instance.musicData.soundValue;
                //设置是否开启
                audioS.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            }

            Destroy(this.gameObject);
        }
    }

    //设置拥有者
    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }
}
