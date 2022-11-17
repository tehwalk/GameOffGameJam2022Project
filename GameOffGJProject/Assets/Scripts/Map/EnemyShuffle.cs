using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyShuffle : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private Transform gfxSpawnPoint;
    [SerializeField] private TextMeshProUGUI enemyText;
    private Enemy _selectedEnemy;
    EnemyPass enemyPass;
    private void Awake()
    {
        enemyPass = GameObject.FindGameObjectWithTag("EnemyPass").GetComponent<EnemyPass>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _selectedEnemy = enemies[Random.Range(0, enemies.Length)];
        GameObject selectedEnemyGFX = Instantiate(_selectedEnemy.enemyGFX, gfxSpawnPoint.position, Quaternion.identity);
        enemyText.text = _selectedEnemy.enemyName;
    }

    public void EnterGame()
    {
        enemyPass.SetSelectedEnemy(_selectedEnemy);
        SceneManager.LoadScene((int)SceneIndex.GameScene);
    }

}
