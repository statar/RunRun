using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleObjectsComponent : MonoBehaviour
{
    private float _oldPlayerPosition;
    private readonly List<GameObject> _activeObstacleObjectsList = new List<GameObject>();
    
    public List<GameObject> passiveObstacleObjectsList = new List<GameObject>();
    public GameObject playerGameObject;

    private void Start()
    {
        GetPlayerPos();
    }

    private void Update()
    {
        if ((_oldPlayerPosition + 10f < playerGameObject.transform.position.z) && _activeObstacleObjectsList.Count < 10)
        {
            GetPlayerPos();
            ObstacleObjectSetActive();
        }
        else if (_activeObstacleObjectsList.Count > 1)
        {
            var objPos = _activeObstacleObjectsList[0].transform.position;
            if (_oldPlayerPosition - 10f > objPos.z || objPos.y < -5f)
            {
                ObstacleObjectSetPassive();
            }
            else if (_oldPlayerPosition > playerGameObject.transform.position.z)
            {
                GetPlayerPos();
                while (_activeObstacleObjectsList.Count > 0)
                    ObstacleObjectSetPassive();
            }
        }
    }

    private void GetPlayerPos()
    {
        _oldPlayerPosition = playerGameObject.transform.position.z;
    }

    private void ObstacleObjectSetPassive()
    {
        var obstacleObject = _activeObstacleObjectsList[0];
        obstacleObject.SetActive(false);
        _activeObstacleObjectsList.RemoveAt(0);
        passiveObstacleObjectsList.Add(obstacleObject);
    }
    private void  ObstacleObjectSetActive()
    {
        var obstacleObject = passiveObstacleObjectsList[0];
        passiveObstacleObjectsList.RemoveAt(0);
        _activeObstacleObjectsList.Add(obstacleObject);
        obstacleObject.SetActive(true);
        obstacleObject.tag = "ActiveObstacleObject";
        
        obstacleObject.transform.position = RandomObstaclePosition();
        obstacleObject.transform.rotation = RandomObstacleRotation();
    }

    private Vector3 RandomObstaclePosition()
    {
        var extraX = Random.Range(-3f, 4f);
        var extraZ =Random.Range(30f, 50f);
        var pPos = playerGameObject.transform.position;
        var obstaclePosition =new Vector3(pPos.x +extraX, 15f, pPos.z +extraZ);
        return obstaclePosition;
    }

    private Quaternion RandomObstacleRotation()
    {
        float rotationExtraPos = Random.Range(-50, 50);
        var obstacleRotation = Quaternion.Euler(0f, 180f + rotationExtraPos, 0f);
        return obstacleRotation;
    }
}
