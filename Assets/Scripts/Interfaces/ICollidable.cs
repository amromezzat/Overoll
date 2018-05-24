using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICollidable
{
    void ReactToCollision(int collidedHealth);
    int Gethealth();
}
