using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathCell : BaseCell
{
    public List<Path> Paths = new List<Path>(); // ����, �� ������� ����� ������� � ������ ������
                                                // (������ ������� - ������, �� ������� ��� ����, ����� ���� ����� "��������" ����)

    [Serializable]
    public class Path // ��������� ����� ��� ����, ����� ������� ������ �������
    {
        public List<GameObject> WayPoints = new List<GameObject>();
    }
}
