using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

namespace FishFlock
{
    public class FishController : MonoBehaviour
    {
        public float MinNeighborDist = 0.4f;
        public float MaxNeighborDist = 1.6f;
        public float MinTimerInt = 0.5f;
        public float MaxTimerInt = 4.0f;
        public Transform FollowObj;
        

        private FishFlockControllerGPU FishFlockController;
        private Vector3 _StartingPos;
        private bool _IsAnimating = false;
        private float _MinSpeed;
        private float _MaxSpeed;
        private RandomMotion _RandomMotion;

        private void Awake()
        {
            FishFlockController = this.gameObject.GetComponent<FishFlockControllerGPU>();
            if (FishFlockController == null)
                throw new System.Exception("A FishFlockControllerGPU must be defined in FishController");

            _MinSpeed = FishFlockController.minSpeed;
            _MaxSpeed = FishFlockController.maxSpeed;

            if(FollowObj != null)
            {
                _StartingPos = FollowObj.position;
                _RandomMotion = FollowObj.gameObject.GetComponent<RandomMotion>();
            }
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

            if (FollowObj != null)
            {
                if (UnityEngine.Random.Range(0f, 1f) > 0.9f)
                {
                    _IsAnimating = true;
                    AnimOut();
                }
            }

            InitFishList();
        }

        private void AnimOut()
        {
            //get our random motion script and turn it off
            _RandomMotion.enabled = false;
            FishFlockController.minSpeed = _MinSpeed * 20;
            FishFlockController.maxSpeed = _MaxSpeed * 20;
            Vector3 newPos = new Vector3(_StartingPos.x, _StartingPos.y, _StartingPos.z - 20);
            FollowObj.DOMove(newPos, 2.0f).OnComplete(AnimIn);
        }

        private void AnimIn()
        {
            FollowObj.DOMove(_StartingPos, 2.0f).OnComplete(AnimComplete).SetDelay(1.5f);
        }

        private void AnimComplete()
        {
            FishFlockController.minSpeed = _MinSpeed;
            FishFlockController.maxSpeed = _MaxSpeed;
            _RandomMotion.enabled = true;
            _IsAnimating = false;
        }
    }
}
