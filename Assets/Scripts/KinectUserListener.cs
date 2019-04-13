using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectUserListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{

    public List<FollowObj> FollowObjList;

    private bool _IsFollowing = false;

    private void Awake()
    {
        if(FollowObjList != null)
        {
            for(int i=0;i<FollowObjList.Count; i++)
            {
                Vector3 curChildPos = FollowObjList[i].ChildObj.transform.position;
                FollowObjList[i].ChildObjStartingPos = curChildPos;
            }
        }
    }

    private void Update()
    {
        if (_IsFollowing)
        {
            EnableFollow();
        }
    }

    /// <summary>
    /// Allow our objects to follow each other
    /// </summary>
    private void EnableFollow()
    {
        if (FollowObjList != null)
        {
            if (FollowObjList.Count > 0)
            {
                for (int i = 0; i < FollowObjList.Count; i++)
                {
                    GameObject curObjToFollow = FollowObjList[i].ObjToFollow;
                    GameObject curChildObj = FollowObjList[i].ChildObj;
                    curChildObj.transform.position = curObjToFollow.transform.position;
                }
            }
        }
    }

    /// <summary>
    /// loop through our objects to reset them to their starting positions
    /// </summary>
    private void DisableFollow()
    {
        if (FollowObjList != null)
        {
            if (FollowObjList.Count > 0)
            {
                for (int i = 0; i < FollowObjList.Count; i++)
                {
                    GameObject curChildObj = FollowObjList[i].ChildObj;
                    curChildObj.transform.position = FollowObjList[i].ChildObjStartingPos;
                }
            }
        }
    }


    /// <summary>
    /// Invoked when a new user is detected. Here you can start gesture tracking by invoking KinectManager.DetectGesture()-function.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="userIndex">User index</param>
    public void UserDetected(long userId, int userIndex)
    {
        _IsFollowing = true;
    }

    /// <summary>
    /// Invoked when a user gets lost. All tracked gestures for this user are cleared automatically.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="userIndex">User index</param>
    public void UserLost(long userId, int userIndex)
    {
        //think we need to check if we're at zero users
        _IsFollowing = false;
        DisableFollow();
    }


    /// <summary>
    /// Invoked when a gesture is in progress.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="userIndex">User index</param>
    /// <param name="gesture">Gesture type</param>
    /// <param name="progress">Gesture progress [0..1]</param>
    /// <param name="joint">Joint type</param>
    /// <param name="screenPos">Normalized viewport position</param>
    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture,
                                float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        return;
    }

    /// <summary>
    /// Invoked if a gesture is cancelled.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <param name="userId">User ID</param>
    /// <param name="userIndex">User index</param>
    /// <param name="gesture">Gesture type</param>
    /// <param name="joint">Joint type</param>
    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint)
    {
        return true;
    }

    /// <summary>
    /// Invoked if a gesture is completed.
    /// </summary>
    /// <returns>true</returns>
    /// <c>false</c>
    /// <param name="userId">User ID</param>
    /// <param name="userIndex">User index</param>
    /// <param name="gesture">Gesture type</param>
    /// <param name="joint">Joint type</param>
    /// <param name="screenPos">Normalized viewport position</param>
    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint, Vector3 screenPos)
    {

        return true;
    }
}
