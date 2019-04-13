using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{

    protected EventManager() { }

    public delegate void KinectUserAction();
    public static event KinectUserAction OnKinectUserFound;
    public static event KinectUserAction OnKinectUserLost;
    public static event KinectUserAction OnNextScreenEvent;
    public static event KinectUserAction OnPreviousScreenEvent;


    public void KinectUserFound()
    {
        if (OnKinectUserFound != null)
            OnKinectUserFound();
    }

    public void KinectUserLost()
    {
        if (OnKinectUserLost != null)
            OnKinectUserLost();
    }

    public void NextScreenEvent()
    {
        if (OnNextScreenEvent != null)
            OnNextScreenEvent();
    }

    public void PreviousScreenEvent()
    {
        if (OnPreviousScreenEvent != null)
            OnPreviousScreenEvent();
    }


}