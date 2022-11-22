using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPass : MonoBehaviour
{
    private static EnemyPass Instance;
    Enemy _selectedEnemy;
    bool hasSelectedBeenDefeated = false;
    public bool HasSelectedBeenDefeated { get { return hasSelectedBeenDefeated; } private set { hasSelectedBeenDefeated = value; } }
    Dictionary<Enemy, bool> defeatedEnemies = new Dictionary<Enemy, bool>();
    public Dictionary<Enemy, bool> DefeatedEnemies { get { return defeatedEnemies; } }
    [SerializeField] EnemyShuffle shuffle;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DictionaryInit();
    }
    void DictionaryInit()
    {
        foreach (Enemy e in shuffle.AvailableEnemies)
        {
            defeatedEnemies.Add(e, false);
        }
    }
    // Start is called before the first frame update
    public void SetSelectedEnemy(Enemy enemy)
    {
        _selectedEnemy = enemy;
    }

    public Enemy GetSelectedEnemy()
    {
        return _selectedEnemy;
    }

    public void EnemyHasBeenWon()
    {
        defeatedEnemies[_selectedEnemy] = true;
    }


}
