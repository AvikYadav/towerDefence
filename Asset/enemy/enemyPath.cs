using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(enemy))]
public class enemyPath : MonoBehaviour
{
    List<Node> path = new List<Node>();
    [SerializeField] [Range(0f,5f)]float speed = 1f;
    // Start is called before the first frame update
    enemy enemy;
    GridManager gridManager;
    pathFinding pathFinder;
    void OnEnable ()
    {
        RecalculatePath();
        StartingPosition();
        StartCoroutine(followPath());
    }

    void Awake()
    {
        enemy = GetComponent<enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<pathFinding>();
    }
    void StartingPosition()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    void RecalculatePath()
    {
        path.Clear();

        path = pathFinder.getNewPath();
    }

    void finsihPath()
    {
        enemy.GetReward();
        gameObject.SetActive(false);
    }

    IEnumerator followPath()
    {
        for(int i = 0; i< path.Count;i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            transform.LookAt(endPos);
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        finsihPath();

    }
}
