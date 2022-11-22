using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum EnemyStage { First, Second, Third };
public class EnemyShuffle : MonoBehaviour
{
    [SerializeField] Enemy[] availableEnemies;
    public Enemy[] AvailableEnemies { get { return availableEnemies; } }
    [SerializeField] private Transform gfxSpawnPoint;
    [SerializeField] private TextMeshProUGUI enemyText;

    private Enemy _selectedEnemy;
    EnemyPass enemyPass;
    EnemyStage enemyStage;

    private void Awake()
    {
        enemyPass = GameObject.FindGameObjectWithTag("EnemyPass").GetComponent<EnemyPass>();
    }
    private void Start()
    {
        SetSelectedEnemy();
    }

    void SetSelectedEnemy()
    {
        foreach (Enemy e in availableEnemies)
        {
            if (enemyPass.DefeatedEnemies[e] == false)
            {
                _selectedEnemy = e;
                GameObject selectedEnemyGFX = Instantiate(_selectedEnemy.enemyGFX, gfxSpawnPoint.position, Quaternion.identity);
                enemyText.text = _selectedEnemy.enemyName;
                return;
            }
        }
    }

    public void EnterGame()
    {
        enemyPass.SetSelectedEnemy(_selectedEnemy);
        SceneManager.LoadScene((int)SceneIndex.GameScene);
    }

}
