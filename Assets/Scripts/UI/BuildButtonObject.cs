using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildButtonObject : InteractableObject
{
    [Header("建造及花费相关")]
    [SerializeField] private int cost;
    [SerializeField] private GameObject towerObject;
    //[SerializeField] private 
    // Start is called before the first frame update
    


    private void OnEnable()
    {
        isEnabled = inGameManager.GetGold() > cost;
        spriteRenderer.sprite = isEnabled ? spriteNormal : spriteDisable;
    }

    void Update()
    {
        if (isEnabled!=(inGameManager.GetGold() > cost))
            //如果金币
        {
            isEnabled = !isEnabled;
            spriteRenderer.sprite = isEnabled ? spriteNormal : spriteDisable;
        }
        
    }

    public void BuildTower(GameObject prefab)
    {
        towerObject = ObjectPool.GetInstance().GetObject(prefab);
        var parent = transform.parent.parent;
        towerObject.transform.SetParent(parent);
        towerObject.transform.localPosition=Vector3.zero;
        towerObject.SetActive(true);
        parent.GetComponent<InteractableObject>().SetEnable(false);
    }

    
}
