using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPass : MonoBehaviour
{
    private static EnemyPass Instance;
    Enemy _selectedEnemy;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
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
}
