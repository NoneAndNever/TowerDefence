using System;
using System.Collections.Generic;


public delegate void CallBack();
public delegate void CallBack<in T>(T t);

public class EventCenter: BaseManager<EventCenter>
{
    private Dictionary<EventType, Delegate> m_EventTable = new Dictionary<EventType, Delegate>();
    /// <summary>
    /// 添加一个0参数函数监听一个类型的事件，当该事件触发时，会自动调用添加的函数
    /// </summary>
    /// <param name="eventType">希望监听的事件类型</param>
    /// <param name="callBack">用于监听的函数</param>
    public EventCenter AddListener(EventType eventType, CallBack callBack)
    {
        if (!m_EventTable.ContainsKey(eventType))
        {
            m_EventTable.Add(eventType, null);
        }
        Delegate d = m_EventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType())
        {
            throw new Exception($"尝试事件为{eventType}添加不同类型委托，当前委托类型为{d.GetType()}，要添加的委托类型为{callBack.GetType()}");
        }
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] + callBack;
        return this;
    }
    
    public EventCenter AddListener<T>(EventType eventType, CallBack<T> callBack)
    {
        if (!m_EventTable.ContainsKey(eventType))
        {
            m_EventTable.Add(eventType, null);
        }
        Delegate d = m_EventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType())
        {
            throw new Exception($"尝试事件为{eventType}添加不同类型委托，当前委托类型为{d.GetType()}，要添加的委托类型为{callBack.GetType()}");
        } 
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] + callBack;
        return this;
    }
}
