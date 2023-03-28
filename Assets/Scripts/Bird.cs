using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Spine;
using Spine.Unity;

public class Bird : MonoBehaviour
{
    public BirdType birdType;

    private SkeletonAnimation skeletonAnimation;
    // [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)] public string eventName;
    private Spine.EventData eventData;
    private string idleAnimation, flyAnimation, groundingAnimation, touchingAnimation;

    private MeshRenderer meshRenderer;
    
    [SerializeField] private bool isSelected = false;
    private Vector3 flyAwayPosition = new Vector3(0, 6.9f, 0);
    private float birdSpeed = 1.5f;
    private Vector3 direction;
    // [SerializeField] public Transform flyAwayPoint;
    // [SerializeField] private AudioSource flyAwaySound;

    void Awake(){
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        idleAnimation = skeletonAnimation.AnimationName = "idle";
        flyAnimation = skeletonAnimation.AnimationName = "fly";
        groundingAnimation = skeletonAnimation.AnimationName = "grounding";
        touchingAnimation = skeletonAnimation.AnimationName = "touching";
        
        skeletonAnimation.AnimationState.SetAnimation(0, idleAnimation, true);
        
        // eventData = skeletonAnimation.Skeleton.Data.FindEvent(eventName);
        skeletonAnimation.AnimationState.Event += HandleAnimationStateEvent;
    }
    void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e){
        if (eventData == e.Data) Debug.Log("test");
    }
    public void SetBirdPos(float x, float y, int i){
        Vector3 newDirection = new Vector3(x, y, 0);
        transform.localPosition = newDirection;
        // if (!value) transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        skeletonAnimation.Skeleton.ScaleX = -skeletonAnimation.Skeleton.ScaleX;
        meshRenderer.sortingOrder = i;
    }
    public void SelectBird(){
        isSelected = !isSelected;
        if (isSelected) skeletonAnimation.AnimationState.SetAnimation(1, touchingAnimation, true);
        else skeletonAnimation.AnimationState.AddEmptyAnimation(1, 0, 0);
    }
    public void SortOrder(int i){
        meshRenderer.sortingOrder = i;
    }
    public void SetMovingDirection(float x, float y){
        meshRenderer.sortingOrder = 10;
        direction = new Vector3(x, y, 0);
        transform.DOLocalMove(direction, birdSpeed).SetEase(Ease.InOutQuint);
        skeletonAnimation.AnimationState.SetAnimation(0, flyAnimation, true);
        skeletonAnimation.AnimationState.AddAnimation(0, idleAnimation, true, birdSpeed);
        // StartCoroutine(FlipTheBird());
    }
    public IEnumerator FlipTheBird(){
        yield return new WaitForSeconds(birdSpeed);
        skeletonAnimation.Skeleton.ScaleX = -skeletonAnimation.Skeleton.ScaleX;
        skeletonAnimation.AnimationState.SetAnimation(0, groundingAnimation, false);
        skeletonAnimation.AnimationState.AddAnimation(0, idleAnimation, true, 0);
        // if (value) transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public IEnumerator FlyAway(){
        yield return new WaitForSeconds(birdSpeed);
        transform.DOMove(flyAwayPosition, birdSpeed);
        skeletonAnimation.AnimationState.SetAnimation(0, flyAnimation, false);
        transform.SetParent(null);
        // flyAwaySound.Play();
    }
    void OnDestroy(){
        // destroy/disable DOTween here
    }
}

public enum BirdType{
    bird1, bird2, bird3, bird4, bird5, bird6, bird7, bird8, bird9, bird10, bird11, bird12
}