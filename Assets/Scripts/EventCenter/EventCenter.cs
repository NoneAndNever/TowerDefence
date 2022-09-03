using System;
using System.Collections.Generic;


public delegate void CallBack();
public delegate void CallBack<in T>(T t);

public class EventCenter: BaseManager<EventCenter>
{
    private Dictionary<EventType, Delegate> eventTable = new Dictionary<EventType, Delegate>();
    /// <summary>
    /// 添加一个0参数函数监听一个类型的事件，当该事件触发时，会自动调用添加的函数
    /// </summary>
    /// <param name="eventType">希望监听的事件类型</param>
    /// <param name="callBack">用于监听的函数</param>
    public EventCenter AddListener(EventType eventType, CallBack callBack)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }
        Delegate d = eventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType())
        {
            throw new Exception($"尝试事件为{eventType}添加不同类型委托，当前委托类型为{d.GetType()}，要添加的委托类型为{callBack.GetType()}");
        }
        eventTable[eventType] = (CallBack)eventTable[eventType] + callBack;
        return this;
    }
    
    public EventCenter AddListener<T>(EventType eventType, CallBack<T> callBack)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }
        Delegate d = eventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType())
        {
            throw new Exception($"尝试事件为{eventType}添加不同类型委托，当前委托类型为{d.GetType()}，要添加的委托类型为{callBack.GetType()}");
        } 
        eventTable[eventType] = (CallBack<T>)eventTable[eventType] + callBack;
        return this;
    }
}
