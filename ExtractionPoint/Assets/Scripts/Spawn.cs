﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    #region Variables
    float spawnPointX;
    float spawnPointY;
    public Transform boundary;

    //Environment Spawning
    public GameObject Tree;

    public int numberOfTrees;

    Vector3 treeSpawnPosition;

    //Enemy Spawning
    public GameObject Enemy;

    Vector3 enemySpawnPosition;

    public float spawnWait;
    public int startWait;
    public int startingEnemies;
    public int spawnEnemies; 

    //Drop Spawning
    public GameObject Drop;
    public GameObject HandGun;
    public GameObject Ammo;
    public GameObject HealthPack;

    Vector3 dropSpawnPosition;

    public float dropWait;
    public float startDropWait;

    public int numberOfDrops = 5;
    int dropType;
    #endregion

    #region Methods
    void Start ()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(spawnDrop());
        Drop = HandGun;
    }
	
	
	void Update ()
    {
        
    }

    void spawnEnvironment()
    {
        for(int i = 0; i < numberOfTrees; i++)
        {
            spawnPointX = Random.Range(-125, 125);
            spawnPointY = Random.Range(-125, 125);
            treeSpawnPosition = new Vector3(spawnPointX, spawnPointY, -3);

            Instantiate(Tree, treeSpawnPosition, gameObject.transform.rotation);
        }
    }

    IEnumerator SpawnEnemy()
    {
        for(int i = 0; i < startingEnemies; i++)
        {
            spawnPointX = Random.Range(-125, 125);
            spawnPointY = Random.Range(-125, 125);
            enemySpawnPosition = new Vector3(spawnPointX, spawnPointY, -2);

            Instantiate(Enemy, enemySpawnPosition, gameObject.transform.rotation);
        }

        yield return new WaitForSeconds(startWait);

        for (int i = 0; i < startingEnemies; i++)
        {
            boundary = GameObject.FindGameObjectWithTag("Boundary").transform;
            spawnPointX = Random.Range(boundary.position.x + boundary.localScale.x, boundary.position.x - boundary.localScale.x);
            spawnPointY = Random.Range(boundary.position.y + boundary.localScale.y, boundary.position.y - boundary.localScale.y);
            enemySpawnPosition = new Vector3(spawnPointX, spawnPointY, -2);

            Instantiate(Enemy, enemySpawnPosition, gameObject.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }

    IEnumerator spawnDrop()
    {
        yield return new WaitForSeconds(startDropWait);

        for(int i = 0; i < numberOfDrops; i++)
        {
            boundary = GameObject.FindGameObjectWithTag("Boundary").transform;
            spawnPointX = Random.Range(boundary.position.x + boundary.localScale.x, boundary.position.x - boundary.localScale.x);
            spawnPointY = Random.Range(boundary.position.y + boundary.localScale.y, boundary.position.y - boundary.localScale.y);
            dropSpawnPosition = new Vector3(spawnPointX, spawnPointY, -1);
            Instantiate(Drop, dropSpawnPosition, gameObject.transform.rotation);

            dropType = Random.Range(1, 3);

            switch(dropType)
            { 
                case 1:
                    Drop = Ammo;
                    break;
                case 2:
                    Drop = HealthPack;
                    break;
                case 3:
                    Drop = HandGun;
                    break;
            }
           
            yield return new WaitForSeconds(dropWait);
        }
    }
    #endregion
}
