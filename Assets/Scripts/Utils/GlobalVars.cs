﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : Singleton<GlobalVars>
{

    protected GlobalVars() { }

    public string[] SceneList = new string[] {"RegularLogo", "FlockLogo", "Logo3D" };

}