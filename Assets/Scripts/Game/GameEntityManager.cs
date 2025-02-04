using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameEntityManager : MonoBehaviour{
    public GameEntityList EntityList;

    public GameEntity FindEntityByGUID(int guid){
        return EntityList.Entities.FirstOrDefault(x => x.GUID == guid);
    }
}