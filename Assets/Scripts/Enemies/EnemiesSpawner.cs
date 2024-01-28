using UnityEngine;
public class EnemiesSpawner : MonoBehaviour
{
    [field: SerializeField] public float SpawnTimeInterval { get; private set; }
    [field: SerializeField] public int ActiveEnemiesCount { get; private set; }
    [SerializeField] private Enemy[] _enemyPrefab;
    [SerializeField] private SpawnPositionData[] _spawnPositions;
    private int _lastDrawnPositionIndex;
    private float _timer;
    private float _spawnIntervalDefault;

    private void OnEnable()
    {
        ComboCounter.Instance.StreakIncrease += On_StreakIncrease;
        ComboCounter.Instance.StreakReset += On_StreakReset;
    }


    private void OnDisable()
    {
        ComboCounter.Instance.StreakIncrease -= On_StreakIncrease;
        ComboCounter.Instance.StreakReset -= On_StreakReset;
    }

    private void On_StreakReset()
    {
        SpawnTimeInterval = _spawnIntervalDefault;
        foreach (var enemy in _enemyPrefab)
        {
            enemy.SetMoveSpeed(enemy.MoveSpeed);
        }
    }
    
    private void On_StreakIncrease(int currentStreak)
    {
        if (currentStreak == 25)
        {
            SpawnTimeInterval *= 0.95f;
            foreach (var enemy in _enemyPrefab)
            {
                enemy.SetMoveSpeed(enemy.MoveSpeed * 1.15f);
            }
        }
        else if (currentStreak == 50)
        {
            SpawnTimeInterval *= 0.95f;
            foreach (var enemy in _enemyPrefab)
            {
                enemy.SetMoveSpeed(enemy.MoveSpeed * 1.125f);
            }
        }
        else if (currentStreak == 75)
        {
            SpawnTimeInterval *= 0.95f;
            foreach (var enemy in _enemyPrefab)
            {
                enemy.SetMoveSpeed(enemy.MoveSpeed * 1.1f);
            }
        }
    }
    
    private void Start()
    {
        _timer = SpawnTimeInterval;
        _spawnIntervalDefault = SpawnTimeInterval;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            SpawnEnemy();
            _timer = SpawnTimeInterval;
        }
    }

    private void SpawnEnemy()
    {
        int drawnPosIndex;
        do
        {
            drawnPosIndex = Random.Range(0, _spawnPositions.Length);
        } while (drawnPosIndex == _lastDrawnPositionIndex);

        SpawnPositionData drawnPosition = _spawnPositions[drawnPosIndex];
        Vector2 drawnSpawnPos = drawnPosition.Position;
        _lastDrawnPositionIndex = drawnPosIndex;
        Enemy newEnemy = Instantiate(_enemyPrefab[Random.Range(0, _enemyPrefab.Length)], drawnSpawnPos, Quaternion.identity);
        ActiveEnemiesCount++;
        newEnemy.Death += On_Death;
        newEnemy.MoveRight = drawnPosition.MoveRight;
    }

    private void On_Death(Enemy enemy)
    {
        ActiveEnemiesCount--;
        enemy.Death -= On_Death;
    }
}
