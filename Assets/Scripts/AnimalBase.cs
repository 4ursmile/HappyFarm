using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AnimalBase : MonoBehaviour, Iselectable
{

    public AnimalType type;
    [SerializeField] public NavMeshAgent agent;
    public string animalName;
    public string animalDescription;
    public AnimalAge AniAge;
    public List<FoodType> foodEatable;
    [SerializeField] float baseSpeed = 3;
    [SerializeField] float foodAbSpeed = 2;
    [SerializeField] public int MaxHealth;
    [SerializeField] float TimeToEat;
    [SerializeField] float stopDistances;
    [SerializeField] Camera mainCamera;
    [SerializeField] GUIPerPet guionThispet;
    Animator animator;
    AnimaStatus status;
    bool isSelect;
    public int CurrentHeath;
    public delegate void ActionHealthBar();
    public ActionHealthBar HealbarAction;
    public void Awake()
    {
        animator = GetComponent<Animator>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stopDistances;
        
    }
    private void OnEnable()
    {
        int Age = 0;
        status = AnimaStatus.MoveAround;
        transform.DOScale(new Vector3(SizeEachAge[Age], SizeEachAge[Age], SizeEachAge[Age]),1);
        CurrentHeath = MaxHealth;
        StartCoroutine(HealthReduceOverTime());
        StartCoroutine(TimeToDev());
    }
    public float GetCurrentHealt()
    {
        return (float)CurrentHeath / (float)MaxHealth;
    }
    bool isMoving = false;
    private void Update()
    {
        if (isMoving)
            if (agent.hasPath)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    HandleStopMoving();
                    isMoving = false;
                }
            }
        if (!isSelect && status ==  AnimaStatus.MoveAround)
        {
            if (!isMoving)
            {
                MoveTo(CreateMovePoint.Instance.GetPoint());
                isMoving = true;
            }
        }
    }
    void HandleStopMoving()
    {
        animator.SetBool("IsMove", false);
        agent.isStopped = true;
        status = AnimaStatus.Idle;
        StartCoroutine(ChangeStatus());
    }
    public void Select()
    {
        PlayerController.Instance.selectbleAction += HandleSelected;
        DownArrowUIHandle.Instance.SetUIArrow(transform);
        isSelect = true;
        guionThispet.SelectPetAction();
        HandleStopMoving();
    }
    public void DeSelect()
    {
        PlayerController.Instance.selectbleAction -= HandleSelected;
        isSelect = false;
        guionThispet.DeSelectPetAction();
        status = AnimaStatus.MoveAround;
    }
    protected virtual void HandleSelected()
    {
        if (Input.GetButtonDown("Fire2"))
            CastAction();
    }
    public bool CanEat(FoodType foodType)
    {
        if (foodEatable.Contains(foodType))
            return true;
        return false;
    }
    void CastAction()
    {
        RaycastHit hit;
        Ray inputRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(inputRay, out hit, Mathf.Infinity))
        {
            MoveTo(hit.point);
        }
    }
    public void MoveTo(Vector3 targetPosition)
    {
        agent.isStopped = false;

        isMoving = true;    
        animator.SetBool("IsMove", true);
        animator.speed = 1;
        agent.speed = baseSpeed;
        agent.destination = targetPosition;
    }
    public void MoveToEat(Vector3 targetPosition)
    {
        agent.isStopped = false;

        isMoving = true;
        animator.SetBool("IsMove", true);
        animator.speed = foodAbSpeed;

        status = AnimaStatus.MoveToFood;
        agent.speed = baseSpeed*foodAbSpeed;
        agent.destination = targetPosition;
    }
    public void EatFood(int foodValue)
    {
        CurrentHeath = Mathf.Clamp(CurrentHeath+foodValue,0,foodValue);
    }
    
    IEnumerator HealthReduceOverTime()
    {
        while (CurrentHeath > 0)
        {
            yield return new WaitForSeconds(1);
            CurrentHeath--;
            HealbarAction?.Invoke();
            
        }
        DeathAction();
        yield return null;
    }
    [SerializeField]
    List<float> TimeToDevEachAge;
    IEnumerator TimeToDev()
    {
        while (AniAge != AnimalAge.adult)
        {
            yield return new WaitForSeconds(TimeToDevEachAge[((int)AniAge)]);
            Devolop((int)AniAge +1);
        }
        yield return null;
    }
    [SerializeField]
    List<float> SizeEachAge;
    void Devolop(int Age)
    {
        transform.DOScale(new Vector3(SizeEachAge[Age], SizeEachAge[Age], SizeEachAge[Age]),1);
        AniAge++;

    }
    IEnumerator ChangeStatus()
    {
        yield return new WaitForSeconds(Random.Range(2, 5f));
        if (status == AnimaStatus.Idle)
        {
            status = AnimaStatus.MoveAround;
        }
    }
    IEnumerator EatCountDown()
    {
        yield return new WaitForSeconds(TimeToEat);
        if (status == AnimaStatus.Eat)
            status = AnimaStatus.Idle;
        StartCoroutine(ChangeStatus());
    }
   public void GetName()
    {
        Debug.Log(animalName);
    }
    void DeathAction()
    {
        animator.SetTrigger("Death");
        this.DeSelect();
        PlayerController.Instance.iselectables.Remove(this);
        Destroy(gameObject,1);
        Destroy(guionThispet.gameObject, 1);



    }
}
public enum AnimalType
{
    Cat, Sheep, Peguin
}
public enum AnimaStatus
{
    Idle,  MoveAround, Eat, MoveToFood
}
public enum AnimalAge
{
    child,  young, adult 
}
public interface Iselectable
{
    public void GetName();
    public void Select();
    public void DeSelect();
}