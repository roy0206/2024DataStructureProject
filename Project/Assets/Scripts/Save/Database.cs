using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




[Serializable]
public class Database
{
    public SerializableDIctionary<string, bool> stage_isCleardPair;








    public Database()
    {
        stage_isCleardPair = new SerializableDIctionary<string, bool>();
    }
}
