using UnityEngine;

public class OnionState : MonsterState
{
    [SerializeField] private float itemDetectionRange = 5.0f;
    [SerializeField] private float fleeDistance = 5.0f;
    private Collider2D myCollider;
    private CharacterStatHandler stathandler;
    private float moveTimer = 0f;
    private float moveDuration = 2f;
    private Vector2 currentDirection;
    private bool isFleeing = false;

    private void Start()
    {
        base.Start();
        myCollider = GetComponent<Collider2D>();
        stathandler = GetComponent<CharacterStatHandler>();
        healthSystem.OnDeath += HandleMonsterDeath;
        healthSystem.OnDamage += FleeFromPlayer;

        if (stathandler == null)
        {
            Debug.LogError("스탯 핸들러가 없습니다.");
        }
        else
        {
            stathandler.UpdateCharacterStats();
        }
        ChooseNewDirection();
    }

    protected override void Update()
    {
        base.Update();

        if (currentState == State.Move && isFleeing)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > fleeDistance)
            {
                isFleeing = false;
                currentState = State.Idle;
            }
        }

        switch (currentState)
        {
            case State.Idle:
                IdleBehavior();
                break;
            case State.Move:
                MoveBehavior();
                break;
        }
    }

    protected override void IdleBehavior()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveDuration)
        {
            ChooseNewDirection();
            currentState = State.Move;
            moveTimer = 0f;
        }
    }

    protected override void MoveBehavior()
    {
        transform.position += (Vector3)currentDirection * stathandler.CurrentMonsterStats.speed * Time.deltaTime;
        moveTimer += Time.deltaTime;
        if (!isFleeing && moveTimer >= moveDuration)
        {
            currentState = State.Idle;
            moveTimer = 0f;
        }
    }

    private void ChooseNewDirection()
    {
        currentDirection = Random.insideUnitCircle.normalized;
    }

    private void FleeFromPlayer()
    {
        Vector2 fleeDirection = (transform.position - player.transform.position).normalized;
        currentDirection = fleeDirection;
        isFleeing = true;
        currentState = State.Move;
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        HandleMonsterDeath();
        Destroy(gameObject);
    }

    private void HandleMonsterDeath()
    {
        Vector3 originalPosition = transform.position;
        if (ItemManager.instance != null && ItemManager.instance.itemPool != null)
        {
            int RandomNum = Random.Range(0, 8);
            switch (RandomNum)
            {
                case 0:
                    ItemManager.instance.itemPool.ItemSpawn(1752, originalPosition);
                        break;
                case 1:
                    ItemManager.instance.itemPool.ItemSpawn(1753, originalPosition);
                        break;
                case 2:
                    ItemManager.instance.itemPool.ItemSpawn(1754, originalPosition);
                        break;
                case 3:
                    ItemManager.instance.itemPool.ItemSpawn(1755, originalPosition);
                        break;
                case 4:
                    ItemManager.instance.itemPool.ItemSpawn(1712, originalPosition);
                        break;
                case 5:
                    ItemManager.instance.itemPool.ItemSpawn(1713, originalPosition);
                        break;
                case 6:
                    ItemManager.instance.itemPool.ItemSpawn(1714, originalPosition);
                        break;
                case 7:
                    ItemManager.instance.itemPool.ItemSpawn(1715, originalPosition);
                        break;
            }            
        }
        else
        {
            Debug.LogWarning("ItemManager 또는 itemPool이 null입니다. 아이템을 드랍할 수 없습니다.");
        }
    }

    private void OnDestroy()
    {
        healthSystem.OnDeath -= HandleMonsterDeath;
        healthSystem.OnDamage -= FleeFromPlayer;
    }
}