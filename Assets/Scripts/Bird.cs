using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bird : MonoBehaviour
{
    public BirdType birdType;
    private Outline outline;
    // [SerializeField] private Vector3 birdPos;
    [SerializeField] Animator animator;
    [SerializeField] private bool isSelected = false;
    [SerializeField] public Transform selectIndicator, flyAwayPoint;
    [SerializeField] private AudioSource flyAwaySound;
    public float birdSpeed = 1f;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        // birdPos = transform.localPosition;
        selectIndicator.GetComponent<SpriteRenderer>().enabled = isSelected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetBirdPos(float x, float y, bool value){
        Vector3 newDirection = new Vector3(x, y, 0);
        transform.localPosition = newDirection;
        GetComponent<SpriteRenderer>().flipX = value;
        if (!value) transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void SelectBird(bool value){
        isSelected = value;
        // selectIndicator.gameObject.SetActive(isSelected);
        selectIndicator.GetComponent<SpriteRenderer>().enabled = isSelected;
    }
    public void SetMovingDirection(float x, float y){
        direction = new Vector3(x, y, 0);
        transform.DOLocalMove(direction, birdSpeed);
    }
    public Vector3 GetMovingDirection(){
        return direction;
    }
    public IEnumerator FlipTheBird(bool value, float speed){
        yield return new WaitForSeconds(speed);
        GetComponent<SpriteRenderer>().flipX = value;
        // if (!value) transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public IEnumerator FlyAway(){
        yield return new WaitForSeconds(birdSpeed);
        GetComponent<SpriteRenderer>().flipX = true;
        transform.DOMove(flyAwayPoint.position, birdSpeed * 3f);
        flyAwaySound.Play();
    }
}

public enum BirdType{
    red, green, pink, grey, blue, yellow, purple
}