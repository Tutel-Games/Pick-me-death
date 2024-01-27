using UnityEngine;
public class EnemiesSpawner : MonoBehaviour
{
    [field: SerializeField] public float SpawnTimeInterval { get; private set; }
    [field: SerializeField] public int ActiveEnemiesCount { get; private set; }
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private SpawnPositionData[] _spawnPositions;
    private int _lastDrawnPositionIndex;
    private float _timer;

    private void Start()
    {
        _timer = SpawnTimeInterval;
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
        Enemy newEnemy = Instantiate(_enemyPrefab, drawnSpawnPos, Quaternion.identity);
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
