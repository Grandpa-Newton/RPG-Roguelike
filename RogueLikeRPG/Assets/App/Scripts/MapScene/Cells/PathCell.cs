using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.MapScene.Cells
{
    public abstract class PathCell : BaseCell
    {
        public List<Path> Paths = new List<Path>(); // пути, по которым можно попасть к данной клетке
        // (первый элемент - клетка, из которой идёт путь, потом идут точки "поворота" пути)

        [Serializable]
        public class Path // отдельный класс для того, чтобы сделать список списков
        {
            public List<GameObject> WayPoints = new List<GameObject>();
        }
    }
}
