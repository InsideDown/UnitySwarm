using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using System;
using UnityEngine.SceneManagement;

public class KinectGestureSpeechListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{

    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        
        keywordActions.Add("next", NextScreen);
        keywordActions.Add("back", PreviousScreen);

        if (keywordRecognizer == null)
        {
            keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
            keywordRecognizer.Start();
        }
    }

    private void Start()
    {
        if (KinectManager.Instance != null)
        {
            if (KinectManager.Instance.IsUserDetected())
            {
                EventManager.Instance.KinectUserFound();
            }
        }
    }

    private void OnEnable()
    {
        EventManager.OnNextScreenEvent += EventManager_OnNextScreenEvent;
        EventManager.OnPreviousScreenEvent += EventManager_OnPreviousScreenEvent;
    }
    
    private void OnDisable()
    {
        Debug.Log("disable");
        EventManager.OnNextScreenEvent -= EventManager_OnNextScreenEvent;
        EventManager.OnPreviousScreenEvent -= EventManager_OnPreviousScreenEvent;
    }

    private void OnDestroy()
    {
        if (keywordRecognizer != null)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }

    void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    private void NextScreen()
    {
        string curSceneName = Application.loadedLevelName;
        string newSceneName = "";
        int totalScenes = GlobalVars.Instance.SceneList.Length;
        if (totalScenes > 1)
        {
            for (int i = 0; i < totalScenes; i++)
            {
                string sceneName = GlobalVars.Instance.SceneList[i];
                if (curSceneName == sceneName)
                {
                   
                    //if we're at the end of the list, jump back to one
                    if (i == totalScenes - 1)
                        newSceneName = GlobalVars.Instance.SceneList[0];
                    else
                        newSceneName = GlobalVars.Instance.SceneList[i + 1];
                    break;
                }
            }
            if(!string.IsNullOrEmpty(newSceneName))
                SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("you only have one scene in your GlobalVars list, we cannot change");
        }
    }
    
    private void PreviousScreen()
    {
        string curSceneName = Application.loadedLevelName;
        string newSceneName = "";
        int totalScenes = GlobalVars.Instance.SceneList.Length;
        if (totalScenes > 1)
        {
            for (int i = 0; i < totalScenes; i++)
            {
                string sceneName = GlobalVars.Instance.SceneList[i];
                if (curSceneName == sceneName)
                {

                    //if we're at the end of the list, jump back to one
                    if (i == 0)
                        newSceneName = GlobalVars.Instance.SceneList[totalScenes - 1];
                    else
                        newSceneName = GlobalVars.Instance.SceneList[i - 1];
                    break;
                }
            }
            if (!string.IsNullOrEmpty(newSceneName))
                SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("you only have one scene in your GlobalVars list, we cannot change");
        }
    }

    private void EventManager_OnNextScreenEvent()
    {

    }

    private void EventManager_OnPreviousScreenEvent()
    {
        
    }


    /// <summary>
    /// Invoked when a new user is detected. Here you can start gesture tracking by invoking KinectManager.DetectGesture()-function.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="userIndex">User index</param>
    public void UserDetected(long userId, int userIndex)
    {
        EventManager.Instance.KinectUserFound();
    }

    /// <summary>
    /// Invoked when a user gets lost. All tracked gestures for this user are cleared automatically.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="userIndex">User index</param>
    public void UserLost(long userId, int userIndex)
    {
        EventManager.Instance.KinectUserLost();
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
        //return;
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
/*        if (gesture == KinectGestures.Gestures.SwipeLeft)
        {
            Debug.Log("swipe left gesture complete");
            NextScreen();
        }
        else if (gesture == KinectGestures.Gestures.SwipeRight)
        {
            Debug.Log("swipe right gesture completed");
            PreviousScreen();
        }
*/
        return true;
    }
}
