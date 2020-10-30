using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    [Header("Требуется IMob (MobController)")]

    /// <summary>
    /// если true, моб будет смотреть в сторону указанной цели, по умолчанию - первый найденый gameobgect с PlayerController
    /// для изменения объекта использовать LookAtTarget(gameobject)
    /// </summary>
    [SerializeField] protected bool IsLookingAtTarget = false;
    [SerializeField] protected bool AutoAttack = true;

    /// <summary>
    /// Если моб не сможет интерактировать с другими объектами (поднимать монеты, открывать двери), должно быть false.
    /// Не влияет на атаку
    /// </summary>
    [SerializeField] protected bool CanInteract = false;
    [SerializeField] protected bool AutoInteract;

    /// <summary>
    /// Gameobject, на который в данный момент сосредоточена реакция (смотрит в его сторону, объект для атаки и тд)
    /// </summary>
    [SerializeField] protected GameObject Target = null;

    /// <summary>
    /// Список объектов, с которыми можно в данный момент интерактировать (кнопки, диалоги, предметы и тд).
    ///  если пуст -  объектов нет рядом. Заполняется через OnTriggerEnter2D и OnTriggerExit2D 
    /// </summary>
    [SerializeField] protected List<GameObject> InteractableGameObjects = new List<GameObject>();

    /// <summary>
    /// Радиус обзора мобом
    /// </summary>
    [SerializeField] protected float LookRadius = 15f;
    [Range(0.05f, 2f)][SerializeField] protected float LookUpdate = 0.5f;
    [SerializeField] protected bool CanSearchTargetByLook = false;

    /// <summary>
    /// Объекты, вошедшие в коллайдер (OnTriggerEnter2D), которые содержут IMob и MobStatsController с вражеской фракцией. (мобы которые можно атаковать)
    ///  добавляются и удаляются через OnTriggerEnter2D и OnTriggerExit2D 
    /// </summary>
    [SerializeField] protected List<GameObject> EnemyMobsInCollider = new List<GameObject>();

    protected IMob IMob;
    protected MobStatsController MobStatsController;

    private float _nextAttackTime = 0;
    
    protected virtual void Awake()
    {
        IMob = GetComponent<IMob>();
        MobStatsController = GetComponent<MobStatsController>();

        // Компонент остановится, когда IMob оповестит о том, что моб мертв
        IMob.Dead += () => { this.enabled = false; };

        var mobColliderDetector = GetComponentInChildren<MobDetectorColliderController>();

        if (mobColliderDetector != null)
        {
            mobColliderDetector.OnTriggerEntered += OnChildTriggerEntered;
            mobColliderDetector.OnTriggerExited += OnChildTriggerExited;
        }
    }

    protected virtual void Start()
    {
        if (CanSearchTargetByLook)
            StartCoroutine(CheckLookCollider());

        StartCoroutine(CheckInColliderListObjectsForNull());
    }

    protected virtual void Update()
    {
        //слежка за gameobject
        if (IsLookingAtTarget && Target != null && IMob != null)
        {
            //Transform цели должен быть РАВЕН или ЭКВИВАЛЕНТЕН this.gameobject.transform
            if (Target.transform.position.x < transform.position.x)
                IMob.SetDirection(Direction.Left);
            else if (Target.transform.position.x > transform.position.x)
                IMob.SetDirection(Direction.Right);
        }

        //атака врага
        if (AutoAttack && EnemyMobsInCollider.Count > 0)
            CloseAttack();
    }

    public void CloseAttack()
    {
        if (Time.time < _nextAttackTime) 
            return;

        _nextAttackTime = Time.time + IMob.GetMobStatsController().GetAttackTime();
        
        IMob.Attack();
    }

    public virtual void SetTarget(GameObject targetObject) => Target = targetObject;

    //События из MobDetectorColliderController
    protected virtual void OnChildTriggerEntered(Collider2D collider)
    {
        var colliderIMob = collider.gameObject.GetComponent<IMob>();

        if (collider.gameObject.layer == LayerMask.NameToLayer("Mobs"))
        {
            //проверка на врага
            if (colliderIMob != null)
            {
                var colliderFraction = colliderIMob.GetMobStatsController().GetFraction;

                if (MobStatsController.GetFraction.IsEnemy(colliderFraction))
                    EnemyMobsInCollider.Add(collider.gameObject);
            }
        }

        //todo : доделать
            //проверка на интерактивный объект
            if (CanInteract)
            {
                var colliderIInteract = collider.gameObject.GetComponent<IIteracactable>();

                if (colliderIInteract != null)
                    InteractableGameObjects.Add(collider.gameObject);
            }
    }

    protected virtual IEnumerator CheckInColliderListObjectsForNull()
    {
        while (true)
        {
            if (EnemyMobsInCollider.Count > 0)
            {
                for (int i = 0; i < EnemyMobsInCollider.Count; i++)
                    if (EnemyMobsInCollider[i] == null) 
                        EnemyMobsInCollider.RemoveAt(i);
            }

            if (InteractableGameObjects.Count > 0)
            {
                for (int i = 0; i < InteractableGameObjects.Count; i++)
                    if (InteractableGameObjects[i] == null) 
                        InteractableGameObjects.RemoveAt(i);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    protected virtual void OnChildTriggerExited(Collider2D collider)
    {   
        //если вышел объект из EnemyMobsInCollider[], то он удаляется из этого массива
        if (EnemyMobsInCollider.Count > 0)
        {
            for (int i = 0; i < EnemyMobsInCollider.Count; i++)
            {
                if (EnemyMobsInCollider[i].GetInstanceID() == collider.gameObject.GetInstanceID())
                {
                    EnemyMobsInCollider.RemoveAt(i);
                    break;
                }
            }
        }
        
        //Проверка на выход Интерактивного объекта
        if (InteractableGameObjects.Count > 0)
        {
            for (int i = 0; i < InteractableGameObjects.Count; i++)
            {
                if (InteractableGameObjects[i].GetInstanceID() == collider.gameObject.GetInstanceID())
                {
                    InteractableGameObjects.RemoveAt(i);
                    break;
                };
            }
        }
    }

    protected virtual void TargetIsDead() => Target = null;

    //todo : оптимизировать, убрать леса
    /// <summary>
    /// Проверка вражеских мобов в поле зрения. Задает Target (ближайший) 
    /// </summary>
    protected virtual IEnumerator CheckLookCollider()
    {
        //Обновляется всегда
        while (true)
        {
            yield return new WaitForSeconds(LookUpdate);

            var objects = Physics2D.CircleCastAll(transform.position, LookRadius, Vector2.zero, 0,
                LayerMask.GetMask("Mobs"));

            if (objects != null)
            {
                for (int i = 0; i < objects.Length; i++)
                {
                    //Проверка на себя
                    if (objects[i].collider.gameObject.GetInstanceID() == gameObject.GetInstanceID()) continue;

                    var objectIMob = objects[i].collider.gameObject.GetComponent<IMob>();

                    if (objectIMob != null)
                    { 
                        //Проверка фрации у моба
                        var objectFraction = objectIMob.GetMobStatsController().GetFraction;

                        //Если враг с хп больше 0 - задается цель
                        if (IMob.GetMobStatsController().GetFraction.IsEnemy(objectFraction))
                            //&& objectIMob.GetMobStatsController().GetStats.Health > 0)
                        {
                         //Алгоритм спорный, целью становиться ближайший GameObject, Если цели нет
                            if (Target == null)
                            {
                                Target = objects[i].collider.gameObject;
                                objectIMob.Dead += TargetIsDead;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}