using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    private static Camera mainCamera;
    
    [Header("sprite切换相关")]
    [SerializeField] protected SpriteRenderer spriteRenderer;   //sprite组件
    [SerializeField] protected Sprite spriteNormal;             //正常时的sprite
    [SerializeField] protected Sprite spriteHighlight;          //高亮时的sprite
    [SerializeField] protected Sprite spriteDisable;            //禁用时的sprite
    [SerializeField] protected bool isEnabled;                  //是否启用
    
    [Header("点击相关")]
    [SerializeField] protected InGameManager inGameManager;
    [SerializeField] protected UnityEvent mouseEvent;
    [SerializeField] private bool isInside;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject obj;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        inGameManager = InGameManager.GetInstance();
        spriteRenderer.sprite = spriteNormal;
        mainCamera= mainCamera ? mainCamera : Camera.main;
        isEnabled = true;
        obj = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&obj &&!isInside)
        {
            ObjectPool.GetInstance().PushObject(obj);
            obj = null;
        }
    }
    /// <summary>
    /// 设置物体是否启用
    /// </summary>
    /// <param name="isEnable">是否启用，是则true，否则false</param>
    public void SetEnable(bool isEnable)
    {
        isEnabled = isEnable;
        spriteRenderer.sprite = isEnabled ? spriteNormal : spriteDisable;
    }
    
    private void OnMouseEnter()
    {
        //如果启用则可以切换到高亮
        if(isEnabled) spriteRenderer.sprite = spriteHighlight;
        isInside = true;
    }

    private void OnMouseOver()
    {
        //如果启用则可以切换到高亮
        if(isEnabled) spriteRenderer.sprite = spriteHighlight;
    }

    private void OnMouseExit()
    {
        //如果启用则可以切换到默认
        if(isEnabled) spriteRenderer.sprite = spriteNormal;
        isInside = false;
    }

    protected virtual void OnMouseDown()
    {
        if (isEnabled) mouseEvent?.Invoke();
        InGameManager.GetInstance();
    }

    public void PopUpObject(GameObject prefab)
    {
        if (obj) return;
        obj = ObjectPool.GetInstance().GetObject(prefab);
        obj.transform.SetParent(transform,false);
        obj.transform.position = transform.position;

    }
    
    public void SellTower()
    { 
        var parent = transform.parent.parent;
        ObjectPool.GetInstance().PushObject(parent.gameObject);
        parent.parent.GetComponent<InteractableObject>().SetEnable(true);
    }
}
