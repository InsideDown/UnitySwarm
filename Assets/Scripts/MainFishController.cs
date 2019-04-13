using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishFlock
{
    public class MainFishController : MonoBehaviour
    {
        [System.Serializable]
        public struct FishItem
        {
            public FishFlockControllerGPU FishControllerGPU;
        }

        public List<FishItem> FishItemList;

        public int TotalFish = 1000;

        private void Awake()
        {
            foreach(FishItem curFishItem in FishItemList)
            {
                curFishItem.FishControllerGPU.fishesCount = TotalFish;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("k"))
            {
                ToggleKinectSettingsOn(true);
            }
            if (Input.GetKeyDown("l"))
            {
                ToggleKinectSettingsOn(false);
            }
        }

        void ToggleKinectSettingsOn(bool isKinectSettingsOn = true)
        {
            if (KinectManager.Instance != null)
            {
                KinectManager.Instance.displayUserMap = isKinectSettingsOn;
                KinectManager.Instance.displayColorMap = isKinectSettingsOn;
                KinectManager.Instance.displaySkeletonLines = isKinectSettingsOn;
            }

        }
    }
}
