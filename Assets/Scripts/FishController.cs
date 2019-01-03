using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FishFlock
{
    public class FishController : MonoBehaviour
    {
        public float MinNeighborDist = 0.4f;
        public float MaxNeighborDist = 1.6f;
        public float MinTimerInt = 0.5f;
        public float MaxTimerInt = 4.0f;

        private FishFlockControllerGPU FishFlockController;

        private void Awake()
        {
            FishFlockController = this.gameObject.GetComponent<FishFlockControllerGPU>();
            if (FishFlockController == null)
                throw new System.Exception("A FishFlockControllerGPU must be defined in FishController");
        }

        // Start is called before the first frame update
        void Start()
        {
            InitFishList();
        }

        private void InitFishList()
        {
            StartCoroutine(DelayFish());
        }

        private IEnumerator DelayFish()
        {
            float ranInt = UnityEngine.Random.Range(MinTimerInt, MaxTimerInt);
            yield return new WaitForSeconds(ranInt);
            RandomizeFish();
        }

        private void RandomizeFish()
        {
            
            if(UnityEngine.Random.Range(0f, 1f) > 0.5f)
            {
                //get a new neighbor distance
                float ranNeighborDistance = UnityEngine.Random.Range(MinNeighborDist, MaxNeighborDist);
                FishFlockController.neighbourDistance = ranNeighborDistance;
            }
            
            InitFishList();
        }
    }
}
