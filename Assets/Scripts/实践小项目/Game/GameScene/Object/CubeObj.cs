﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObj : MonoBehaviour
{
    //奖励物品预设体 关联
    public GameObject[] rewardObjects;

    //死亡特效 预设体  关联
    public GameObject deadEff;

    private void OnTriggerEnter(Collider other)
    {
        //1.打到自己的子弹 应该销毁
        //第一步 不用再这里写 只需要把箱子的tag  改成Cube 之前子弹逻辑当中就已经处理过 打中 cube销毁自己的逻辑

        //2.打到自己 应该处理 随机创建奖励的逻辑

        //随机一个数 来获取奖励
        int rangeInt = Random.Range(0, 100);
        //50%的几率 创建一个奖励 
        if( rangeInt < 50 )
        {
            //随机创建一个 奖励预设体 在当前位置
            rangeInt = Random.Range(0, rewardObjects.Length);
            //放到当前箱子所在的位置 即可
            Instantiate(rewardObjects[rangeInt], this.transform.position, this.transform.rotation);
        }

        //创建特效预设体
        GameObject effObj = Instantiate(deadEff, this.transform.position, this.transform.rotation);
        //控制音效
        AudioSource audioS = effObj.GetComponent<AudioSource>();
        audioS.volume = GameDataMgr.Instance.musicData.soundValue;
        audioS.mute = !GameDataMgr.Instance.musicData.isOpenSound;

        Destroy(this.gameObject);
    }
}
