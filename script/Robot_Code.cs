using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robot_Code : MonoBehaviour
{
    public GameObject spawnObj;
    public Transform[] Robot = new Transform[6];
    public Transform objectHolder;
    public Transform objectDetect;
    public Transform boxSensor;
    public Transform parent;
    public Transform spawnerPoint;
    public Text[] txtJoint = new Text[6];
    public Text[] txtTCP = new Text[6];
    public float rayDist;
    RaycastHit hitObject;
    RaycastHit boxHit;

    private double[,] Joint = new double[6, 1];
    private double[,] TCP = new double[6, 1];
    private double[,] initialPoint = new double[6, 1] { { 1870 }, { 2500 }, { -350 }, { 0 }, { -90 }, { 0 } };
    //private double[,] initialPoint = new double[6,1] { { 1870 }, { 2650 }, { -350 }, { 0 }, { -90 }, { 0 } };
    //private double[,] endPoint = new double[6, 1] { { 1755 }, { 2026 }, { 150 }, { 0 }, { 0 }, { 0 } };
    private double[,] endPoint = new double[6, 1] { { 1755 }, { 2750 }, { 374 }, { 0 }, { 0 }, { 0 } };
    //private double[,] path = WholePath();
    private bool flag = true;
    private int row = 0;
    private int counter = 0;
    private int objCount = 0;
    private bool objDetect;
    private bool releaseTag = false;
    private bool holdObjt = false;
    private bool homePost = true;

    static double[,] objPoint = new double[6, 1] { { 1515 }, { 1650 }, { -1150 }, { 0 }, { -90 }, { 0 } };
    static double[,] objPointa = new double[6, 1] { { 1515 }, { 2100 }, { -1150 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlObjPoint = new double[6, 1] { { 1515 }, { 2420 }, { -1150 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlMidPointa = new double[6, 1] { { 1531.946 }, { 2500 }, { -641.7428 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlMidPointb = new double[6, 1] { { 1275 }, { 2500 }, { -641.7428 }, { 0 }, { -90 }, { 0 } };
    static double[,] midPoint = new double[6, 1] { { 1870 }, { 2500 }, { -350 }, { 0 }, { -90 }, { 0 } };
    static double[,] midPointb = new double[6, 1] { { 1275 }, { 2500 }, { -350 }, { 0 }, { -90 }, { 0 } };
    double[,] grabObjPath = SplinePath(midPoint, controlMidPointa, controlObjPoint, objPoint, segmentSize: 5);
    double[,] liftObjPath = LinePath(objPoint, objPointa, segmentSize: 5);
    double[,] initialPatha = SplinePath(objPointa, controlObjPoint, controlMidPointa, midPoint, segmentSize: 5);
    double[,] initialPathb = SplinePath(objPointa, controlObjPoint, controlMidPointb, midPointb, segmentSize: 5);

    static double[,] controlPoint1a = new double[6, 1] { { 2411.423 }, { 2500 }, { 191.423 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint1b = new double[6, 1] { { 2250 }, { 1854.0162 }, { 1275 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint1 = new double[6, 1] { { 2250 }, { 1175 }, { 1275 }, { 0 }, { -90 }, { 0 } };
    static double[,] point1 = new double[6, 1] { { 2250 }, { 700 }, { 1275 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath1 = SplinePath(point1, controlPoint1b, controlPoint1a, midPoint, segmentSize: 5);

    static double[,] controlPoint2a = new double[6, 1] { { 2266.4075 }, { 2500 }, { 46.4075 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint2b = new double[6, 1] { { 2250 }, { 1749.6990 }, { 790 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint2 = new double[6, 1] { { 2250 }, { 1175 }, { 790 }, { 0 }, { -90 }, { 0 } };
    static double[,] point2 = new double[6, 1] { { 2250 }, { 700 }, { 790 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath2 = SplinePath(point2, controlPoint2b, controlPoint2a, midPoint, segmentSize: 5);

    static double[,] controlPoint3a = new double[6, 1] { { 2127.1748 }, { 2500 }, { -92.8252 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint3b = new double[6, 1] { { 2250 }, { 1668.3038 }, { 310 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint3 = new double[6, 1] { { 2250 }, { 1175 }, { 310 }, { 0 }, { -90 }, { 0 } };
    static double[,] point3 = new double[6, 1] { { 2250 }, { 700 }, { 310 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath3 = SplinePath(point3, controlPoint3b, controlPoint3a, midPoint, segmentSize: 5);

    static double[,] controlPoint4a = new double[6, 1] { { 2127.1748 }, { 2500 }, { -208.0884 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint4b = new double[6, 1] { { 2250 }, { 1621.8462 }, { -200 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint4 = new double[6, 1] { { 2250 }, { 1175 }, { -200 }, { 0 }, { -90 }, { 0 } };
    static double[,] point4 = new double[6, 1] { { 2250 }, { 700 }, { -200 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath4 = SplinePath(point4, controlPoint4b, controlPoint4a, midPoint, segmentSize: 5);

    static double[,] controlPoint5a = new double[6, 1] { { 1275 }, { 2500 }, { 191.423 }, { 0 }, { -90 }, { 0 } }; 
    static double[,] controlPoint5b = new double[6, 1] { { 1275 }, { 1854.0162 }, { 1275 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint5 = new double[6, 1] { { 1275 }, { 1175 }, { 1275 }, { 0 }, { -90 }, { 0 } };
    static double[,] point5 = new double[6, 1] { { 1275 }, { 700 }, { 1275 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath5 = SplinePath(point5, controlPoint5b, controlPoint5a, midPoint, segmentSize: 5);

    static double[,] controlPoint6a = new double[6, 1] { { 1275 }, { 2500 }, { 46.4075 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint6b = new double[6, 1] { { 1275 }, { 1749.6989 }, { 790 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint6 = new double[6, 1] { { 1275 }, { 1175 }, { 790 }, { 0 }, { -90 }, { 0 } };
    static double[,] point6 = new double[6, 1] { { 1275 }, { 700 }, { 790 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath6 = SplinePath(point6, controlPoint6b, controlPoint6a, midPoint, segmentSize: 5);

    static double[,] controlPoint7a = new double[6, 1] { { 1275 }, { 2500 }, { -92.8252 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint7b = new double[6, 1] { { 1275 }, { 1668.3038 }, { 310 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint7 = new double[6, 1] { { 1275 }, { 1175 }, { 310 }, { 0 }, { -90 }, { 0 } };
    static double[,] point7 = new double[6, 1] { { 1275 }, { 700 }, { 310 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath7 = SplinePath(point7, controlPoint7b, controlPoint7a, midPoint, segmentSize: 5);

    static double[,] controlPoint8a = new double[6, 1] { { 1275 }, { 2500 }, { -208.0884 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint8b = new double[6, 1] { { 1275 }, { 1621.8462 }, { -200 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint8 = new double[6, 1] { { 1275 }, { 1175 }, { -200 }, { 0 }, { -90 }, { 0 } };
    static double[,] point8 = new double[6, 1] { { 1275 }, { 700 }, { -200 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath8 = SplinePath(point8, controlPoint8b, controlPoint8a, midPoint, segmentSize: 5);

    // --------------------------------------------- Level Two --------------------------------------------- //

    static double[,] controlPoint1ba = new double[6, 1] { { 2411.423 }, { 2500 }, { 191.423 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint1bb = new double[6, 1] { { 2250 }, { 1691 }, { 1275 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint1b = new double[6, 1] { { 2250 }, { 1500 }, { 1275 }, { 0 }, { -90 }, { 0 } };
    static double[,] point1b = new double[6, 1] { { 2250 }, { 1150 }, { 1275 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath1b = SplinePath(point1b, controlPoint1bb, controlPoint1ba, midPoint, segmentSize: 5);

    static double[,] controlPoint2ba = new double[6, 1] { { 2266.4075 }, { 2500 }, { 46.4075 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint2bb = new double[6, 1] { { 2250 }, { 2116 }, { 790 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint2b = new double[6, 1] { { 2250 }, { 1925 }, { 790 }, { 0 }, { -90 }, { 0 } };
    static double[,] point2b = new double[6, 1] { { 2250 }, { 1150 }, { 790 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath2b = SplinePath(point2b, controlPoint2bb, controlPoint2ba, midPoint, segmentSize: 5);

    static double[,] controlPoint3ba = new double[6, 1] { { 2127.1748 }, { 2500 }, { -92.8252 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint3bb = new double[6, 1] { { 2250 }, { 2116 }, { 310 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint3b = new double[6, 1] { { 2250 }, { 1925 }, { 310 }, { 0 }, { -90 }, { 0 } };
    static double[,] point3b = new double[6, 1] { { 2250 }, { 1150 }, { 310 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath3b = SplinePath(point3b, controlPoint3bb, controlPoint3ba, midPoint, segmentSize: 5);

    static double[,] controlPoint4ba = new double[6, 1] { { 2127.1748 }, { 2500 }, { -208.0884 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint4bb = new double[6, 1] { { 2250 }, { 2116 }, { -200 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint4b = new double[6, 1] { { 2250 }, { 1925 }, { -200 }, { 0 }, { -90 }, { 0 } };
    static double[,] point4b = new double[6, 1] { { 2250 }, { 1150 }, { -200 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath4b = SplinePath(point4b, controlPoint4bb, controlPoint4ba, midPoint, segmentSize: 5);

    static double[,] controlPoint5ba = new double[6, 1] { { 1275 }, { 2500 }, { 191.423 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint5bb = new double[6, 1] { { 1275 }, { 2116 }, { 1275 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint5b = new double[6, 1] { { 1275 }, { 1925 }, { 1275 }, { 0 }, { -90 }, { 0 } };
    static double[,] point5b = new double[6, 1] { { 1275 }, { 1150 }, { 1275 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath5b = SplinePath(point5b, controlPoint5bb, controlPoint5ba, midPoint, segmentSize: 5);

    static double[,] controlPoint6ba = new double[6, 1] { { 1275 }, { 2500 }, { 46.4075 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint6bb = new double[6, 1] { { 1275 }, { 2116 }, { 790 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint6b = new double[6, 1] { { 1275 }, { 1925 }, { 790 }, { 0 }, { -90 }, { 0 } };
    static double[,] point6b = new double[6, 1] { { 1275 }, { 1150 }, { 790 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath6b = SplinePath(point6b, controlPoint6bb, controlPoint6ba, midPoint, segmentSize: 5);

    static double[,] controlPoint7ba = new double[6, 1] { { 1275 }, { 2500 }, { -92.8252 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint7bb = new double[6, 1] { { 1275 }, { 2116 }, { 310 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint7b = new double[6, 1] { { 1275 }, { 1925 }, { 310 }, { 0 }, { -90 }, { 0 } };
    static double[,] point7b = new double[6, 1] { { 1275 }, { 1150 }, { 310 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath7b = SplinePath(point7b, controlPoint7bb, controlPoint7ba, midPoint, segmentSize: 5);

    static double[,] controlPoint8ba = new double[6, 1] { { 1275 }, { 2500 }, { -208.0884 }, { 0 }, { -90 }, { 0 } };
    static double[,] controlPoint8bb = new double[6, 1] { { 1275 }, { 2116 }, { -200 }, { 0 }, { -90 }, { 0 } };
    static double[,] liftPoint8b = new double[6, 1] { { 1275 }, { 1925 }, { -200 }, { 0 }, { -90 }, { 0 } };
    static double[,] point8b = new double[6, 1] { { 1275 }, { 1150 }, { -200 }, { 0 }, { -90 }, { 0 } };
    double[,] backPath8b = SplinePath(point8b, controlPoint8bb, controlPoint8ba, midPoint, segmentSize: 5);

    static double[,] point1c = new double[6, 1] { { 2225 }, { 1600 }, { 1235 }, { 0 }, { -90 }, { 0 } };
    static double[,] point2c = new double[6, 1] { { 2225 }, { 1600 }, { 785 }, { 0 }, { -90 }, { 0 } };
    static double[,] point3c = new double[6, 1] { { 2225 }, { 1600 }, { 335 }, { 0 }, { -90 }, { 0 } };
    static double[,] point4c = new double[6, 1] { { 2225 }, { 1600 }, { -115 }, { 0 }, { -90 }, { 0 } };
    static double[,] point5c = new double[6, 1] { { 1325 }, { 1600 }, { 1235 }, { 0 }, { -90 }, { 0 } };
    static double[,] point6c = new double[6, 1] { { 1325 }, { 1600 }, { 785 }, { 0 }, { -90 }, { 0 } };
    static double[,] point7c = new double[6, 1] { { 1325 }, { 1600 }, { 335 }, { 0 }, { -90 }, { 0 } };
    static double[,] point8c = new double[6, 1] { { 1325 }, { 1600 }, { -115 }, { 0 }, { -90 }, { 0 } };

    //Start is called before the first frame update
    void Start()
    {
        SpawnObject();

        //Sets joints to initial position
        //Joint = new double[6, 1] { { 0 }, { 0 }, { 0 }, { 0 }, { -90 }, { 0 } };
        //var pointTCP = new double[6, 1] { { 1870 }, { 2500 }, { -200 }, { 0 }, { -0 }, { 0 } };
        //var pointTCP = new double[6, 1] { { 1870 }, { 2500 }, { -200 }, { 0 }, { -90 }, { 0 } };
        Joint = InverseKinematics(initialPoint);

        // Update joint angle
        UpdateJointsPosition(Robot, Joint);
        TCP = ForwardKinematics(Joint);

        // Update panel value
        for (int i = 0; i < 6; i++)
        {
            txtJoint[i].text = Joint[i, 0].ToString("0.000");
            txtTCP[i].text = TCP[i, 0].ToString("0.000");
        }
    }

    // Update is called once per frame
    void Update()
    {
        var palletPath1b = PalletPath(initialPatha, midPoint, controlPoint1ba, controlPoint1bb, liftPoint1b, point1b);
        var palletPath2b = PalletPath(initialPatha, midPoint, controlPoint2ba, controlPoint2bb, liftPoint2b, point2b);
        var palletPath3b = PalletPath(initialPatha, midPoint, controlPoint3ba, controlPoint3bb, liftPoint3b, point3b);
        var palletPath4b = PalletPath(initialPatha, midPoint, controlPoint4ba, controlPoint4bb, liftPoint4b, point4b);
        var palletPath5b = PalletPath(initialPathb, midPointb, controlPoint5ba, controlPoint5bb, liftPoint5b, point5b);
        var palletPath6b = PalletPath(initialPathb, midPointb, controlPoint6ba, controlPoint6bb, liftPoint6b, point6b);
        var palletPath7b = PalletPath(initialPathb, midPointb, controlPoint7ba, controlPoint7bb, liftPoint7b, point7b);
        var palletPath8b = PalletPath(initialPathb, midPointb, controlPoint8ba, controlPoint8bb, liftPoint8b, point8b);

        objDetect = BoxSensor();
        if (objDetect == true && holdObjt == false && homePost == true)
        {
            row = NewPath(grabObjPath, row);
            if (row == grabObjPath.GetLength(0))
            {
                row = 0;
                holdObjt = true;
                homePost = false;
            }
        }


        if (CheckPath(point1) == false && row != PalletPath1().GetLength(0) && counter == 0 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(PalletPath1(), row);
            if (row == PalletPath1().GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if (CheckPath(midPoint) == false && row != backPath1.GetLength(0) && counter == 0 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath1, row);
            if (row == backPath1.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if (CheckPath(point2) == false && row != PalletPath2().GetLength(0) && counter == 1 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(PalletPath2(), row);
            if (row == PalletPath2().GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if (CheckPath(midPoint) == false && row != backPath2.GetLength(0) && counter == 1 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath2, row);
            if (row == backPath2.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if (CheckPath(point3) == false && row != PalletPath3().GetLength(0) && counter == 2 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(PalletPath3(), row);
            if (row == PalletPath3().GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if (CheckPath(midPoint) == false && row != backPath3.GetLength(0) && counter == 2 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath3, row);
            if (row == backPath3.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if (CheckPath(point4) == false && row != PalletPath4().GetLength(0) && counter == 3 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(PalletPath4(), row);
            if (row == PalletPath4().GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if (CheckPath(midPoint) == false && row != backPath4.GetLength(0) && counter == 3 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath4, row);
            if (row == backPath4.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if (CheckPath(point5) == false && row != PalletPath5().GetLength(0) && counter == 4 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(PalletPath5(), row);
            if (row == PalletPath5().GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if (CheckPath(midPoint) == false && row != backPath5.GetLength(0) && counter == 4 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath5, row);
            if (row == backPath5.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if (CheckPath(point6) == false && row != PalletPath6().GetLength(0) && counter == 5 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(PalletPath6(), row);
            if (row == PalletPath6().GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if (CheckPath(midPoint) == false && row != backPath6.GetLength(0) && counter == 5 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath6, row);
            if (row == backPath6.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if (CheckPath(point7) == false && row != PalletPath7().GetLength(0) && counter == 6 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(PalletPath7(), row);
            if (row == PalletPath7().GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if (CheckPath(midPoint) == false && row != backPath7.GetLength(0) && counter == 6 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath7, row);
            if (row == backPath7.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if (CheckPath(point8) == false && row != PalletPath8().GetLength(0) && counter == 7 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(PalletPath8(), row);
            if (row == PalletPath8().GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if (CheckPath(midPoint) == false && row != backPath8.GetLength(0) && counter == 7 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath8, row);
            if (row == backPath8.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if (CheckPath(point1b) == false && row != palletPath1b.GetLength(0) && counter == 8 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(palletPath1b, row);
            if (row == palletPath1b.GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if (CheckPath(midPoint) == false && row != backPath1b.GetLength(0) && counter == 8 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath1b, row);
            if (row == backPath1b.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if (CheckPath(point2b) == false && row != palletPath2b.GetLength(0) && counter == 9 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(palletPath2b, row);
            if (row == palletPath2b.GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if (CheckPath(midPoint) == false && row != backPath2b.GetLength(0) && counter == 9 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath2b, row);
            if (row == backPath2b.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if (CheckPath(point3b) == false && row != palletPath3b.GetLength(0) && counter == 10 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(palletPath3b, row);
            if (row == palletPath3b.GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if (CheckPath(midPoint) == false && row != backPath3b.GetLength(0) && counter == 10 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath3b, row);
            if (row == backPath3b.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if (CheckPath(point4b) == false && row != palletPath4b.GetLength(0) && counter == 11 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(palletPath4b, row);
            if (row == palletPath4b.GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if (CheckPath(midPoint) == false && row != backPath4b.GetLength(0) && counter == 11 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath4b, row);
            if (row == backPath4b.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if(CheckPath(point5b) == false && row != palletPath7b.GetLength(0) && counter == 12 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(palletPath5b, row);
            if(row == palletPath5b.GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if(CheckPath(midPoint) == false && row != backPath5b.GetLength(0) && counter == 12 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath5b, row);
            if(row == backPath5b.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if(CheckPath(point6b) == false && row != palletPath6b.GetLength(0) && counter == 13 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(palletPath6b, row);
            if(row == palletPath6b.GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if(CheckPath(midPoint) == false && row != backPath6b.GetLength(0) && counter == 13 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath6b, row);
            if(row == backPath6b.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if(CheckPath(point7b) == false && row != palletPath7b.GetLength(0) && counter == 14 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(palletPath7b, row);
            if(row == palletPath7b.GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                SpawnObject();
            }
        }
        else if(CheckPath(midPoint) == false && row != backPath7b.GetLength(0) && counter == 14 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath7b, row);
            if(row == backPath7b.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }
        else if(CheckPath(point8b) == false && row != palletPath8b.GetLength(0) && counter == 15 && releaseTag == false && holdObjt == true && homePost == false)
        {
            row = NewPath(palletPath8b, row);
            if(row == palletPath8b.GetLength(0))
            {
                releaseTag = true;
                row = 0;
                holdObjt = false;
                //SpawnObject();
            }
        }
        else if(CheckPath(midPoint) == false && row != backPath8b.GetLength(0) && counter == 15 && releaseTag == true && holdObjt == false && homePost == false)
        {
            row = NewPath(backPath8b, row);
            if(row == backPath8b.GetLength(0))
            {
                releaseTag = false;
                row = 0;
                holdObjt = false;
                homePost = true;
                counter += 1;
            }
        }

        HoldObject(releaseTag);
    }

    static void UpdateJointsPosition(Transform[] Robot,double[,] Joints)
    {
        Robot[0].localEulerAngles = new Vector3(0, (float)Joints[0, 0], 0);
        Robot[1].localEulerAngles = new Vector3(0, 0, (float)Joints[1, 0]);
        Robot[2].localEulerAngles = new Vector3(0, 0, (float)Joints[2, 0]);
        Robot[3].localEulerAngles = new Vector3((float)Joints[3, 0], 0, 0);
        Robot[4].localEulerAngles = new Vector3(0, 0, (float)Joints[4, 0]);
        Robot[5].localEulerAngles = new Vector3((float)Joints[5, 0], 0, 0);
    }

    static double Radian(double degree)
    {
        var radian = degree / 180 * Math.PI;
        return radian;
    }

    static double Degree(double radian)
    {
        var degree = radian / Math.PI * 180;
        return degree;
    }

    static double[,] ForwardKinematics(double[,] joints)
    {
        // Physical size of the robot
        var a1y = 650;
        var a2x = 400;
        var a2y = 680;
        var a3y = 1100;
        var a4y = 230;
        var a4x = 766;
        var a5x = 345;
        var a6x = 244;

        var matrixT1 = new double[4, 4] { { Math.Cos(Radian(joints[0, 0])), 0, Math.Sin(Radian(joints[0, 0])), 0 }, { 0, 1, 0, a1y }, { -Math.Sin(Radian(joints[0, 0])), 0, Math.Cos(Radian(joints[0, 0])), 0 }, { 0, 0, 0, 1 } };
        var matrixT2 = new double[4, 4] { { Math.Cos(Radian(joints[1, 0])), -Math.Sin(Radian(joints[1, 0])), 0, a2x }, { Math.Sin(Radian(joints[1, 0])), Math.Cos(Radian(joints[1, 0])), 0, a2y }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
        var matrixT3 = new double[4, 4] { { Math.Cos(Radian(joints[2, 0])), -Math.Sin(Radian(joints[2, 0])), 0, 0 }, { Math.Sin(Radian(joints[2, 0])), Math.Cos(Radian(joints[2, 0])), 0, a3y }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
        var matrixT4 = new double[4, 4] { { 1, 0, 0, a4x }, { 0, Math.Cos(Radian(joints[3, 0])), -Math.Sin(Radian(joints[3, 0])), a4y }, { 0, Math.Sin(Radian(joints[3, 0])), Math.Cos(Radian(joints[3, 0])), 0 }, { 0, 0, 0, 1 } };
        var matrixT5 = new double[4, 4] { { Math.Cos(Radian(joints[4, 0])), -Math.Sin(Radian(joints[4, 0])), 0, a5x }, { Math.Sin(Radian(joints[4, 0])), Math.Cos(Radian(joints[4, 0])), 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
        var matrixT6 = new double[4, 4] { { 1, 0, 0, a6x }, { 0, Math.Cos(Radian(joints[5, 0])), -Math.Sin(Radian(joints[5, 0])), 0 }, { 0, Math.Sin(Radian(joints[5, 0])), Math.Cos(Radian(joints[5, 0])), 0 }, { 0, 0, 0, 1 } };

        var matrixT12 = MatrixOperation.MultiplicationOperation(matrixT1, matrixT2);
        var matrixT23 = MatrixOperation.MultiplicationOperation(matrixT12, matrixT3);
        var matrixT34 = MatrixOperation.MultiplicationOperation(matrixT23, matrixT4);
        var matrixT45 = MatrixOperation.MultiplicationOperation(matrixT34, matrixT5);
        var matrixT = MatrixOperation.MultiplicationOperation(matrixT45, matrixT6);

        var matrixR1 = new double[3, 3] { { Math.Cos(Radian(joints[0, 0])), 0, Math.Sin(Radian(joints[0, 0])) }, { 0, 1, 0 }, { -Math.Sin(Radian(joints[0, 0])), 0, Math.Cos(Radian(joints[0, 0])) } };
        var matrixR2 = new double[3, 3] { { Math.Cos(Radian(joints[1, 0])), -Math.Sin(Radian(joints[1, 0])), 0 }, { Math.Sin(Radian(joints[1, 0])), Math.Cos(Radian(joints[1, 0])), 0 }, { 0, 0, 1 } };
        var matrixR3 = new double[3, 3] { { Math.Cos(Radian(joints[2, 0])), -Math.Sin(Radian(joints[2, 0])), 0 }, { Math.Sin(Radian(joints[2, 0])), Math.Cos(Radian(joints[2, 0])), 0 }, { 0, 0, 1 } };
        var matrixR4 = new double[3, 3] { { 1, 0, 0 }, { 0, Math.Cos(Radian(joints[3, 0])), -Math.Sin(Radian(joints[3, 0])) }, { 0, Math.Sin(Radian(joints[3, 0])), Math.Cos(Radian(joints[3, 0])) } };
        var matrixR5 = new double[3, 3] { { Math.Cos(Radian(joints[4, 0])), -Math.Sin(Radian(joints[4, 0])), 0 }, { Math.Sin(Radian(joints[4, 0])), Math.Cos(Radian(joints[4, 0])), 0 }, { 0, 0, 1 } };
        var matrixR6 = new double[3, 3] { { 1, 0, 0 }, { 0, Math.Cos(Radian(joints[5, 0])), -Math.Sin(Radian(joints[5, 0])) }, { 0, Math.Sin(Radian(joints[5, 0])), Math.Cos(Radian(joints[5, 0])) } };

        var matrixR12 = MatrixOperation.MultiplicationOperation(matrixR1, matrixR2);
        var matrixR23 = MatrixOperation.MultiplicationOperation(matrixR12, matrixR3);
        var matrixR34 = MatrixOperation.MultiplicationOperation(matrixR23, matrixR4);
        var matrixR45 = MatrixOperation.MultiplicationOperation(matrixR34, matrixR5);
        var bigR = MatrixOperation.MultiplicationOperation(matrixR45, matrixR6);

        var zero = new double[4, 1] { { 0 }, { 0 }, { 0 }, { 1 } };
        var TCPxyz = MatrixOperation.MultiplicationOperation(matrixT, zero);
        var X = Math.Round(TCPxyz[0, 0],4);
        var Y = Math.Round(TCPxyz[1, 0],4);
        var Z = Math.Round(TCPxyz[2, 0],4);
        var A = Math.Round(Degree(Math.Atan2(bigR[1, 2], bigR[1, 1])),4);
        var B = Math.Round(Degree(Math.Asin(bigR[1, 0])),4);
        var C = Math.Round(Degree(Math.Atan2(-bigR[2, 0], bigR[0, 0])),4);

        var TCP = new double[6, 1] { { X }, { Y }, { Z }, { A }, { B }, { C } };
        return TCP;
    }

    static double[,] InverseKinematics(double[,] TCP)
    {
        // Physical size of the robot
        var a1y = 650.0;
        var a2x = 400.0;
        var a2y = 680.0;
        var a3y = 1100.0;
        var a4y = 230.0;
        var a4x = 766.0;
        var a5x = 345.0;
        var a6x = 244.0;
        var A = TCP[3, 0];
        var B = TCP[4, 0];
        var C = TCP[5, 0];

        var bigR = new double[3, 3] { { Math.Cos(Radian(C)) * Math.Cos(Radian(B)), (-Math.Cos(Radian(C)) * Math.Sin(Radian(B)) * Math.Cos(Radian(A))) + (Math.Sin(Radian(C)) * Math.Sin(Radian(A))), (Math.Cos(Radian(C)) * Math.Sin(Radian(B)) * Math.Sin(Radian(A))) + (Math.Sin(Radian(C)) * Math.Cos(Radian(A))) }, { Math.Sin(Radian(B)), Math.Cos(Radian(B)) * Math.Cos(Radian(A)), -Math.Cos(Radian(B)) * Math.Sin(Radian(A)) }, { -Math.Sin(Radian(C)) * Math.Cos(Radian(B)), (Math.Sin(Radian(C)) * Math.Sin(Radian(B)) * Math.Cos(Radian(A))) + (Math.Cos(Radian(C)) * Math.Sin(Radian(A))), (-Math.Sin(Radian(C)) * Math.Sin(Radian(B)) * Math.Sin(Radian(A))) + (Math.Cos(Radian(C) * Math.Cos(Radian(A)))) } };
        var WP = new double[3];
        WP[0] = (TCP[0, 0] - (a6x * bigR[0, 0]));
        WP[2] = -(-TCP[2, 0] - (a6x * bigR[2, 0]));
        WP[1] = (TCP[1, 0] - (a6x * bigR[1, 0]));
        var joints = new double[6, 1];
        joints[0, 0] = Math.Round(-Degree(Math.Atan2(WP[2], WP[0])),4);

        var WPxz = Math.Sqrt((WP[0] * WP[0]) + (WP[2] * WP[2]));
        var l = WPxz - a2x;
        var h = WP[1] - a1y - a2y;
        var rho = Math.Sqrt((h * h) + (l * l));
        var b4x = Math.Sqrt((a4y * a4y) + ((a4x + a5x) * (a4x + a5x)));
        var alpha = Degree(Math.Atan2(h, l));
        var cosbeta = ((rho * rho) + (a3y * a3y) - (b4x * b4x)) / (2 * rho * a3y);
        var sinbeta = Math.Sqrt(1 - (cosbeta * cosbeta));
        var beta = Degree(Math.Atan2(sinbeta, cosbeta));
        joints[1, 0] = Math.Round(-((180 / 2) - (alpha + beta)),4);

        var cosgama = ((a3y * a3y) + (b4x * b4x) - (rho * rho)) / (2 * a3y * b4x);
        var singama = Math.Sqrt(1 - (cosgama * cosgama));
        var gama = Degree(Math.Atan2(singama, cosgama));
        var delta = Degree(Math.Atan2(a4x + a5x, a4y));
        joints[2, 0] = Math.Round(-(180 - (gama + delta)),4);

        var Rarm = new double[3, 3] { { (Math.Cos(Radian(joints[0, 0])) * Math.Cos(Radian(joints[1, 0])) * Math.Cos(Radian(joints[2, 0]))) - (Math.Cos(Radian(joints[0, 0])) * Math.Sin(Radian(joints[1, 0])) * Math.Sin(Radian(joints[2, 0]))), (-Math.Cos(Radian(joints[0, 0])) * Math.Cos(Radian(joints[1, 0])) * Math.Sin(Radian(joints[2, 0]))) - (Math.Cos(Radian(joints[0, 0])) * Math.Sin(Radian(joints[1, 0])) * Math.Cos(Radian(joints[2, 0]))), Math.Sin(Radian(joints[0, 0])) }, { (Math.Sin(Radian(joints[1, 0])) * Math.Cos(Radian(joints[2, 0]))) + (Math.Cos(Radian(joints[1, 0])) * Math.Sin(Radian(joints[2, 0]))), (-Math.Sin(Radian(joints[1, 0])) * Math.Sin(Radian(joints[2, 0]))) + (Math.Cos(Radian(joints[1, 0])) * Math.Cos(Radian(joints[2, 0]))), 0 }, { (-Math.Sin(Radian(joints[0, 0])) * Math.Cos(Radian(joints[1, 0])) * Math.Cos(Radian(joints[2, 0]))) + (Math.Sin(Radian(joints[0, 0])) * Math.Sin(Radian(joints[1, 0])) * Math.Sin(Radian(joints[2, 0]))), (Math.Sin(Radian(joints[0, 0])) * Math.Cos(Radian(joints[1, 0])) * Math.Sin(Radian(joints[2, 0]))) + (Math.Sin(Radian(joints[0, 0])) * Math.Sin(Radian(joints[1, 0])) * Math.Cos(Radian(joints[2, 0]))), Math.Cos(Radian(joints[0, 0])) } };
        var Rtransarm = MatrixOperation.TransposeMatrix(Rarm);
        var Rwrist = MatrixOperation.MultiplicationOperation(Rtransarm, bigR);
        var joints3a = Math.Round(Degree(Math.Atan2(-Rwrist[2, 0], -Rwrist[1, 0])),4);
        var joints3b = Math.Round(Degree(Math.Atan2(Rwrist[2, 0], Rwrist[1, 0])),4);

        var left = -(Math.Sqrt(1 - (Rwrist[0, 0] * Rwrist[0, 0])));
        joints[4, 0] = Math.Round(Degree(Math.Atan2(left, Rwrist[0, 0])),4);
        var joints5a = Math.Round(Degree(Math.Atan2(Rwrist[0, 2], -Rwrist[0, 1])),4);
        var joints5b = Math.Round(Degree(Math.Atan2(-Rwrist[0, 2], Rwrist[0, 1])),4);
        if (left < 0)
        {
            joints[3, 0] = joints3a;
        }
        else
        {
            joints[3, 0] = joints3b;
        }
        if (left <= 0)
        {
            joints[5, 0] = joints5b;
        }
        else
        {
            joints[5, 0] = joints5a;
        }

        return joints;
    }

    static double[,] Quaternions(double[,] initialPoint, double[,] targetPoint, double alpha = 0.0, double segmentSize = 5.0)
    {
        var inA = initialPoint[3, 0];
        var inB = initialPoint[4, 0];
        var inC = initialPoint[5, 0];
        var tarA = targetPoint[3, 0];
        var tarB = targetPoint[4, 0];
        var tarC = targetPoint[5, 0];

        var initialR = new double[3, 3] { { Math.Cos(Radian(inC)) * Math.Cos(Radian(inB)), (-Math.Cos(Radian(inC)) * Math.Sin(Radian(inB)) * Math.Cos(Radian(inA))) + (Math.Sin(Radian(inC)) * Math.Sin(Radian(inA))), (Math.Cos(Radian(inC)) * Math.Sin(Radian(inB)) * Math.Sin(Radian(inA))) + (Math.Sin(Radian(inC)) * Math.Cos(Radian(inA))) }, { Math.Sin(Radian(inB)), Math.Cos(Radian(inB)) * Math.Cos(Radian(inA)), -Math.Cos(Radian(inB)) * Math.Sin(Radian(inA)) }, { -Math.Sin(Radian(inC)) * Math.Cos(Radian(inB)), (Math.Sin(Radian(inC)) * Math.Sin(Radian(inB)) * Math.Cos(Radian(inA))) + (Math.Cos(Radian(inC)) * Math.Sin(Radian(inA))), (-Math.Sin(Radian(inC)) * Math.Sin(Radian(inB)) * Math.Sin(Radian(inA))) + (Math.Cos(Radian(inC) * Math.Cos(Radian(inA)))) } };
        var targetR = new double[3, 3] { { Math.Cos(Radian(tarC)) * Math.Cos(Radian(tarB)), (-Math.Cos(Radian(tarC)) * Math.Sin(Radian(tarB)) * Math.Cos(Radian(tarA))) + (Math.Sin(Radian(tarC)) * Math.Sin(Radian(tarA))), (Math.Cos(Radian(tarC)) * Math.Sin(Radian(tarB)) * Math.Sin(Radian(tarA))) + (Math.Sin(Radian(tarC)) * Math.Cos(Radian(tarA))) }, { Math.Sin(Radian(tarB)), Math.Cos(Radian(tarB)) * Math.Cos(Radian(tarA)), -Math.Cos(Radian(tarB)) * Math.Sin(Radian(tarA)) }, { -Math.Sin(Radian(tarC)) * Math.Cos(Radian(tarB)), (Math.Sin(Radian(tarC)) * Math.Sin(Radian(tarB)) * Math.Cos(Radian(tarA))) + (Math.Cos(Radian(tarC)) * Math.Sin(Radian(tarA))), (-Math.Sin(Radian(tarC)) * Math.Sin(Radian(tarB)) * Math.Sin(Radian(tarA))) + (Math.Cos(Radian(tarC) * Math.Cos(Radian(tarA)))) } };

        var q1 = new double[4, 1];
        q1[3, 0] = Math.Round((1.0 / 2.0) * (Math.Sqrt(1.0 + initialR[0, 0] + initialR[1, 1] + initialR[2, 2])), 4);
        q1[0, 0] = Math.Round((1.0 / (4.0 * q1[3, 0])) * (initialR[2, 1] - initialR[1, 2]), 4);
        q1[1, 0] = Math.Round((1.0 / (4.0 * q1[3, 0])) * (initialR[0, 2] - initialR[2, 0]), 4);
        q1[2, 0] = Math.Round((1.0 / (4.0 * q1[3, 0])) * (initialR[1, 0] - initialR[0, 1]), 4);

        var q2 = new double[4, 1];
        q2[3, 0] = Math.Round((1.0 / 2.0) * (Math.Sqrt(1.0 + targetR[0, 0] + targetR[1, 1] + targetR[2, 2])), 4);
        q2[0, 0] = Math.Round((1.0 / (4.0 * q2[3, 0])) * (targetR[2, 1] - targetR[1, 2]), 4);
        q2[1, 0] = Math.Round((1.0 / (4.0 * q2[3, 0])) * (targetR[0, 2] - targetR[2, 0]), 4);
        q2[2, 0] = Math.Round((1.0 / (4.0 * q2[3, 0])) * (targetR[1, 0] - targetR[0, 1]), 4);

        if (alpha == 0)
        {
            if (MatrixOperation.DotProductVectorOperation(q1, q2) >= 0)
            {
                alpha = Degree(Math.Acos((MatrixOperation.DotProductVectorOperation(q1, q2)) / ((MatrixOperation.NormQuaternionOperation(q1)) * (MatrixOperation.NormQuaternionOperation(q2)))));
            }
            else
            {
                q2 = MatrixOperation.ScalarMultiplicationOperation(-1, q2);
                alpha = Degree(Math.Acos((MatrixOperation.DotProductVectorOperation(q1, q2)) / ((MatrixOperation.NormQuaternionOperation(q1)) * (MatrixOperation.NormQuaternionOperation(q2)))));
            }
        }
        else
        {
            alpha = alpha;
        }

        var rowSize = 100 / segmentSize;
        var TCPs = new double[(int)rowSize, 3];
        try
        {
            var t = 0.0;
            var pointR = new double[3, 3];
            for (int row = 0; row < TCPs.GetLength(0); row++)
            {
                var qMatrix = MatrixOperation.SumOperation(MatrixOperation.ScalarMultiplicationOperation((Math.Sin(Radian(alpha * (1 - t)))) / (Math.Sin(Radian(alpha))), q1), MatrixOperation.ScalarMultiplicationOperation(Math.Sin(Radian(alpha * t)) / Math.Sin(Radian(alpha)), q2));

                pointR[0, 0] = Math.Round(1 - (2 * qMatrix[1, 0] * qMatrix[1, 0]) - (2 * qMatrix[2, 0] * qMatrix[2, 0]), 4);
                pointR[0, 1] = Math.Round((2 * qMatrix[0, 0] * qMatrix[1, 0]) - (2 * qMatrix[2, 0] * qMatrix[3, 0]), 4);
                pointR[0, 2] = Math.Round((2 * qMatrix[0, 0] * qMatrix[2, 0]) + (2 * qMatrix[1, 0] * qMatrix[3, 0]), 4);
                pointR[1, 0] = Math.Round((2 * qMatrix[0, 0] * qMatrix[1, 0]) + (2 * qMatrix[2, 0] * qMatrix[3, 0]), 4);
                pointR[1, 1] = Math.Round(1 - (2 * qMatrix[0, 0] * qMatrix[0, 0]) - (2 * qMatrix[2, 0] * qMatrix[2, 0]), 4);
                pointR[1, 2] = Math.Round((2 * qMatrix[1, 0] * qMatrix[2, 0]) - (2 * qMatrix[0, 0] * qMatrix[3, 0]), 4);
                pointR[2, 0] = Math.Round((2 * qMatrix[0, 0] * qMatrix[2, 0]) - (2 * qMatrix[1, 0] * qMatrix[3, 0]), 4);
                pointR[2, 1] = Math.Round((2 * qMatrix[1, 0] * qMatrix[2, 0]) + (2 * qMatrix[0, 0] * qMatrix[3, 0]), 4);
                pointR[2, 2] = Math.Round(1 - (2 * qMatrix[0, 0] * qMatrix[0, 0]) - (2 * qMatrix[1, 0] * qMatrix[1, 0]), 4);

                var left = Math.Sqrt(1 - (pointR[1, 0] * pointR[1, 0]));
                var Aa = Math.Round(Degree(Math.Atan2(-pointR[1, 2], -pointR[1, 1])), 4);
                var Ab = Math.Round(Degree(Math.Atan2(pointR[1, 2], pointR[1, 1])), 4);
                TCPs[row, 1] = Math.Round(Degree(Math.Asin(Math.Round(pointR[1, 0], 0))), 4);
                var Ca = Math.Round(Degree(Math.Atan2(pointR[2, 0], -pointR[0, 0])), 4);
                var Cb = Math.Round(Degree(Math.Atan2(-pointR[2, 0], pointR[0, 0])), 4);

                if (left < 0)
                {
                    TCPs[row, 0] = Aa;
                }
                else if (left >= 0)
                {
                    TCPs[row, 0] = Ab;
                }
                else
                {
                    TCPs[row, 0] = 0.0;
                }
                if (left < 0)
                {
                    TCPs[row, 2] = Ca;
                }
                else if (left >= 0)
                {
                    TCPs[row, 2] = Cb;
                }
                else
                {
                    TCPs[row, 2] = 0.0;
                }

                //Debug.Log(String.Format("TCPs[x, 0] = {0};  [x, 1] = {1};   [x,2] = {2}", TCPs[row, 0],TCPs[row,1],TCPs[row,2]));

                t = t + (segmentSize / 100.0);
            }
        }

        catch (Exception)
        {
            Console.WriteLine("There is an error occured while calculating final result");
        }

        return TCPs;
    }

    static double[,] LinePath(double[,] pointA, double[,] pointB, double segmentSize = 5.0)
    {
        var rowSize = 100 / segmentSize;
        var Points = new double[(int)rowSize, pointA.GetLength(0)];
        double t = 0.0;

        try
        {
            var orientation = Quaternions(pointA, pointB, alpha: 15, segmentSize: segmentSize);
            for (int row = 0; row < Points.GetLength(0); row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Points[row, col] = (pointA[col, 0] * (1 - t)) + (pointB[col, 0] * t);
                }
                Points[row, 3] = orientation[row, 0];
                Points[row, 4] = orientation[row, 1];
                Points[row, 5] = orientation[row, 2];
                t += segmentSize / 100.0;
            }
        }
        catch (Exception)
        {
            Console.WriteLine("There is an unexpected error while calculating line path");
        }

        return Points;
    }

    static double[,] PTPPath(double[,] pointA, double[,] pointB, double segmentSize = 5.0)
    {
        var JointA = InverseKinematics(pointA);
        var JointB = InverseKinematics(pointB);
        var rowSize = 100 / segmentSize;
        var Joints = new double[(int)rowSize+1, JointA.GetLength(0)];
        try
        {
            var t = 0.0;
            for (int row = 0; row < Joints.GetLength(0); row++)
            {
                for (int col = 0; col < Joints.GetLength(1); col++)
                {
                    Joints[row, col] = (JointA[col, 0] * (1 - t)) + (JointB[col, 0] * t);
                }
                t = t + (segmentSize / 100);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("An unexpected error occurred while calculating point-to-point path");
        }

        return Joints;
    }

    static double[,] CirclePath(double[,] pointA, double[,] pointB, double[,] pointC, double alpha = 0.0, double circleSegmentSize = 9.0, double quatSegmentSize = 10.0)
    {
        var armPointA = new double[3, 1];
        var armPointB = new double[3, 1];
        var armPointC = new double[3, 1];
        for (int i = 0; i < 3; i++)
        {
            armPointA[i, 0] = pointA[i, 0];
            armPointB[i, 0] = pointB[i, 0];
            armPointC[i, 0] = pointC[i, 0];
        }
        var v1 = MatrixOperation.SubstractionOperation(armPointB, armPointA);
        var v2 = MatrixOperation.SubstractionOperation(armPointC, armPointB);
        var v3 = MatrixOperation.SubstractionOperation(armPointA, armPointC);
        var N = MatrixOperation.CrossProductOperation(v1, v2);
        N = MatrixOperation.UnitVectorOperation(N);

        var segmentSize = circleSegmentSize; // Hanya didesain untuk alpha = 180. Perlu penghitungan manual untuk alpha != 180
        var pointsRow = alpha / segmentSize;
        Console.WriteLine("pointsRow = {0}", pointsRow);
        var Points = new double[(int)pointsRow+1, pointA.GetLength(0)];
        if (MatrixOperation.NormVectorOperation(N) != 0)
        {
            var r = ((MatrixOperation.NormVectorOperation(MatrixOperation.SubstractionOperation(armPointA, armPointB))) * (MatrixOperation.NormVectorOperation((MatrixOperation.SubstractionOperation(armPointB, armPointC)))) * (MatrixOperation.NormVectorOperation(MatrixOperation.SubstractionOperation(armPointC, armPointA)))) / (2 * (MatrixOperation.NormVectorOperation(MatrixOperation.CrossProductOperation(MatrixOperation.SubstractionOperation(armPointA, armPointB), MatrixOperation.SubstractionOperation(armPointB, armPointC)))));
            var c0 = (MatrixOperation.DotProductVectorOperation(MatrixOperation.ScalarMultiplicationOperation((MatrixOperation.NormVectorOperation(MatrixOperation.SubstractionOperation(armPointB, armPointC))) * (MatrixOperation.NormVectorOperation(MatrixOperation.SubstractionOperation(armPointB, armPointC))), MatrixOperation.SubstractionOperation(armPointA, armPointB)), MatrixOperation.SubstractionOperation(armPointA, armPointC))) / (2 * ((MatrixOperation.NormVectorOperation(MatrixOperation.CrossProductOperation(MatrixOperation.SubstractionOperation(armPointA, armPointB), MatrixOperation.SubstractionOperation(armPointB, armPointC)))) * (MatrixOperation.NormVectorOperation(MatrixOperation.CrossProductOperation(MatrixOperation.SubstractionOperation(armPointA, armPointB), MatrixOperation.SubstractionOperation(armPointB, armPointC))))));
            var c1 = (MatrixOperation.DotProductVectorOperation(MatrixOperation.ScalarMultiplicationOperation((MatrixOperation.NormVectorOperation(MatrixOperation.SubstractionOperation(armPointA, armPointC))) * (MatrixOperation.NormVectorOperation(MatrixOperation.SubstractionOperation(armPointA, armPointC))), MatrixOperation.SubstractionOperation(armPointB, armPointA)), MatrixOperation.SubstractionOperation(armPointB, armPointC))) / (2 * ((MatrixOperation.NormVectorOperation(MatrixOperation.CrossProductOperation(MatrixOperation.SubstractionOperation(armPointA, armPointB), MatrixOperation.SubstractionOperation(armPointB, armPointC)))) * (MatrixOperation.NormVectorOperation(MatrixOperation.CrossProductOperation(MatrixOperation.SubstractionOperation(armPointA, armPointB), MatrixOperation.SubstractionOperation(armPointB, armPointC))))));
            var c2 = (MatrixOperation.DotProductVectorOperation(MatrixOperation.ScalarMultiplicationOperation((MatrixOperation.NormVectorOperation(MatrixOperation.SubstractionOperation(armPointA, armPointB))) * (MatrixOperation.NormVectorOperation(MatrixOperation.SubstractionOperation(armPointA, armPointB))), MatrixOperation.SubstractionOperation(armPointC, armPointA)), MatrixOperation.SubstractionOperation(armPointC, armPointB))) / (2 * ((MatrixOperation.NormVectorOperation(MatrixOperation.CrossProductOperation(MatrixOperation.SubstractionOperation(armPointA, armPointB), MatrixOperation.SubstractionOperation(armPointB, armPointC)))) * (MatrixOperation.NormVectorOperation(MatrixOperation.CrossProductOperation(MatrixOperation.SubstractionOperation(armPointA, armPointB), MatrixOperation.SubstractionOperation(armPointB, armPointC))))));
            var C = MatrixOperation.SumOperation(MatrixOperation.SumOperation(MatrixOperation.ScalarMultiplicationOperation(c0, armPointA), MatrixOperation.ScalarMultiplicationOperation(c1, armPointB)), MatrixOperation.ScalarMultiplicationOperation(c2, armPointC));
            var U = MatrixOperation.SubstractionOperation(armPointA, C);
            U = MatrixOperation.UnitVectorOperation(U);
            var V = MatrixOperation.CrossProductOperation(N, U);

            if (alpha == 0)
            {
                alpha = Degree(Math.Acos((MatrixOperation.DotProductVectorOperation(v1, v3)) / ((MatrixOperation.NormQuaternionOperation(v1)) * (MatrixOperation.NormQuaternionOperation(v3)))));
                if ((N[0, 0] * (-1) < 0 && N[1, 0] * (-1) < 0 && N[2, 0] * (-1) < 0) || (N[0, 0] * (-1) > 0 && N[1, 0] * (-1) > 0 && N[2, 0] * (-1) > 0))
                {
                    alpha = alpha;
                }
                else
                {
                    alpha = 360 - alpha;
                }
            }
            else
            {
                alpha = alpha;
            }

            try
            {
                var t = 0.0;
                var orientationA = Quaternions(pointA, pointB, alpha: 15, segmentSize: quatSegmentSize); // Hanya didesain untuk alpha = 180. Perlu penghitungan manual untuk alpha != 180
                var orientationB = Quaternions(pointB, pointC, alpha: 15, segmentSize: quatSegmentSize); // Hanya didesain untuk alpha = 180. Perlu penghitungan manual untuk alpha != 180
                for (int row = 0; row < Points.GetLength(0); row++)
                {
                    var matrixP = MatrixOperation.SumOperation(MatrixOperation.SumOperation(C, MatrixOperation.ScalarMultiplicationOperation(r * Math.Cos(Radian(t)), U)), MatrixOperation.ScalarMultiplicationOperation(r * Math.Sin(Radian(t)), V));
                    Points[row, 0] = matrixP[0, 0];
                    Points[row, 1] = matrixP[1, 0];
                    Points[row, 2] = matrixP[2, 0];
                    if (row < pointsRow / 2)
                    {
                        Points[row, 3] = orientationA[row, 0];
                        Points[row, 4] = orientationA[row, 1];
                        Points[row, 5] = orientationA[row, 2];
                    }
                    else if (row >= pointsRow / 2)
                    {
                        Points[row, 3] = orientationB[row - orientationB.GetLength(0), 0];
                        Points[row, 4] = orientationB[row - orientationB.GetLength(0), 0];
                        Points[row, 5] = orientationB[row - orientationB.GetLength(0), 0];
                    }
                    else
                    {
                        Console.WriteLine("An unexpected error occurred while calculating quaternions orientation");
                    }
                    t = t + segmentSize;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An unexpected error occured while calculating points coordinate");
            }
        }
        else
        {
            Console.WriteLine("Cannot generate circle path based on the available points");
        }
        return Points;
    }

    static double[,] SplinePath(double[,] pointA, double[,] pointB, double[,] pointC, double[,] pointD, double segmentSize = 2.0, double quatAlpha = 15)
    {
        var zeroAccPoints = new double[3];
        var oneAccPoints = new double[3];
        var twoAccPoints = new double[3];
        var zeroDobAccPoints = new double[3];
        var oneDobAccPoints = new double[3];
        var rowSize = 100 / segmentSize;
        var finalPoints = new double[(int)rowSize, pointA.GetLength(0)];

        try
        {
            var t = 0.0;
            var orientation = Quaternions(pointA, pointD, alpha: quatAlpha, segmentSize: segmentSize);
            for (int row = 0; row < finalPoints.GetLength(0); row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    zeroAccPoints[col] = ((1 - t) * pointA[col, 0]) + (t * pointB[col, 0]);
                    oneAccPoints[col] = ((1 - t) * pointB[col, 0]) + (t * pointC[col, 0]);
                    twoAccPoints[col] = ((1 - t) * pointC[col, 0]) + (t * pointD[col, 0]);
                    zeroDobAccPoints[col] = ((1 - t) * zeroAccPoints[col]) + (t * oneAccPoints[col]);
                    oneDobAccPoints[col] = ((1 - t) * oneAccPoints[col]) + (t * twoAccPoints[col]);
                    finalPoints[row, col] = Math.Round(((1 - t) * zeroDobAccPoints[col]) + (t * oneDobAccPoints[col]), 4);
                }
                finalPoints[row, 3] = orientation[row, 0];
                finalPoints[row, 4] = orientation[row, 1];
                finalPoints[row, 5] = orientation[row, 2];
                t = t + (segmentSize / 100);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("An unexpected error occurred while creating spline path");
        }

        return finalPoints;
    }

    static double PathLength(double[,] pointA, double[,] pointB, double[,] pointC = null, double alpha = 0.0)
    {
        double length;
        if (pointC == null && alpha == 0.0)
        {
            length = Math.Sqrt(((pointB[0, 0] - pointA[0, 0]) * (pointB[0, 0] - pointA[0, 0])) + ((pointB[1, 0] - pointA[1, 0]) * (pointB[1, 0] - pointA[1, 0])) + ((pointB[2, 0] - pointA[2, 0]) * (pointB[2, 0] - pointA[2, 0])));
        }
        else if (pointC != null && alpha == 0.0)
        {
            var v0 = MatrixOperation.SubstractionOperation(pointB, pointA);
            var v2 = MatrixOperation.SubstractionOperation(pointC, pointB);
            alpha = Degree(Math.Acos((MatrixOperation.DotProductVectorOperation(v0, v2)) / (MatrixOperation.NormVectorOperation(v0) * MatrixOperation.NormVectorOperation(v2))));
            var r = ((MatrixOperation.NormVectorOperation(MatrixOperation.SubstractionOperation(pointA, pointB))) * (MatrixOperation.NormVectorOperation((MatrixOperation.SubstractionOperation(pointB, pointC)))) * (MatrixOperation.NormVectorOperation(MatrixOperation.SubstractionOperation(pointC, pointA)))) / (2 * (MatrixOperation.NormVectorOperation(MatrixOperation.CrossProductOperation(MatrixOperation.SubstractionOperation(pointA, pointB), MatrixOperation.SubstractionOperation(pointB, pointC)))));
            length = r * alpha;
        }
        else if (pointC != null && alpha != 0.0)
        {
            var r = ((MatrixOperation.NormVectorOperation(MatrixOperation.SubstractionOperation(pointA, pointB))) * (MatrixOperation.NormVectorOperation((MatrixOperation.SubstractionOperation(pointB, pointC)))) * (MatrixOperation.NormVectorOperation(MatrixOperation.SubstractionOperation(pointC, pointA)))) / (2 * (MatrixOperation.NormVectorOperation(MatrixOperation.CrossProductOperation(MatrixOperation.SubstractionOperation(pointA, pointB), MatrixOperation.SubstractionOperation(pointB, pointC)))));
            length = r * alpha;
        }
        else
        {
            length = 0.0;
            Console.WriteLine("An unexpected error occurred while calculating length path");
        }
        return length;
    }

    static bool SafeSelfCollision(double[,] armPointA, double[,] armPointB, double radius, double[,] pointInSpace)
    {
        var newArmPointA = new double[3, 1];
        var newArmPointB = new double[3, 1];
        var newPointInSpace = new double[3, 1];
        for (int i = 0; i < newArmPointA.GetLength(0); i++)
        {
            newArmPointA[i, 0] = armPointA[i, 0];
            newArmPointB[i, 0] = armPointB[i, 0];
            newPointInSpace[i, 0] = pointInSpace[i, 0];
        }
        var radiusArray = new double[3, 1] { { radius }, { radius }, { radius } };
        bool flag;

        var n = MatrixOperation.SubstractionOperation(newArmPointB, newArmPointA);
        var v = MatrixOperation.SubstractionOperation(newPointInSpace, newArmPointA);
        var t = (MatrixOperation.DotProductVectorOperation(v, n)) / (MatrixOperation.NormVectorOperation(n) * MatrixOperation.NormVectorOperation(n));
        var tn = MatrixOperation.ScalarMultiplicationOperation(t, n);
        var point = MatrixOperation.SumOperation(newArmPointA, tn);
        var bench = MatrixOperation.SumOperation(radiusArray, point);
        bench = MatrixOperation.SubstractionOperation(bench, point);

        if (t > 0.0 && t < 1.0)
        {
            var distance = MatrixOperation.SubstractionOperation(newPointInSpace, point);
            var dbench = MatrixOperation.SubstractionOperation(distance, bench);
            bool Flag()
            {
                for (int i = 0; i < dbench.GetLength(0); i++)
                {
                    if (dbench[i, 0] <= 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            flag = Flag();
        }
        else
        {
            var distanceA = MatrixOperation.SubstractionOperation(newPointInSpace, newArmPointA);
            var distanceB = MatrixOperation.SubstractionOperation(newPointInSpace, newArmPointB);
            double[,] distance;
            if (MatrixOperation.NormVectorOperation(distanceA) <= MatrixOperation.NormVectorOperation(distanceB))
            {
                distance = distanceA;
            }
            else
            {
                distance = distanceB;
            }

            var newdistance = Math.Sqrt((distance[0, 0] * distance[0, 0]) + (distance[1, 0] * distance[1, 0]) + (distance[2, 0] * distance[2, 0]));
            var newdisrad = newdistance - radius;
            bool Flag()
            {
                if (newdisrad <= 0)
                {
                    return false;
                }
                return true;
            }
            flag = Flag();
        }



        return flag;
    }

    static double[,] ExtractingTCPPath(double[,] path, int row)
    {
        var TCPInSpace = new double[6, 1];
        for(int col = 0; col < path.GetLength(1); col++)
        {
            TCPInSpace[col, 0] = path[row, col];
        }

        return TCPInSpace;
    }

    int NewPath(double[,] path, int row)
    {
        var oldJoint = Joint;
        var targetJoint = InverseKinematics(ExtractingTCPPath(path, row));
        var step = MatrixOperation.SubstractionOperation(targetJoint,oldJoint);

        // Updating joint value
        for(int i = 0; i < 6; i++)
        {
            if(Math.Round(Joint[i, 0], 1) - Math.Round(targetJoint[i, 0], 1) < 0)
            {
                Joint[i, 0] += Math.Abs(Math.Round(step[i,0],4));
            }
            else if(Math.Round(Joint[i, 0], 1) - Math.Round(targetJoint[i, 0], 1) > 0)
            {
                Joint[i, 0] -= Math.Abs(Math.Round(step[i, 0],4));
            }
            else
            {
                Joint[i, 0] = Joint[i, 0];
            }
        }

        // Updating joint angle
        UpdateJointsPosition(Robot,Joint);
        TCP = ForwardKinematics(Joint);

        // Update panel value
        for(int j = 0; j < 6; j++)
        {
            txtJoint[j].text = Joint[j, 0].ToString("0.0000");
            txtTCP[j].text = TCP[j, 0].ToString("0.0000");
        }

        var counter = row + 1;

        return counter;
    }

    bool CheckPath(double[,] targetTCP)
    {
        var targetJoint = InverseKinematics(targetTCP);

        for(int i = 0; i < 6; i++)
        {
            if(Math.Round(Joint[i,0],1) != Math.Round(targetJoint[i,0],1))
            {
                return false;
            }
            else
            {
                continue;
            }
        }
        return true;
    }

    static double[,] WholePath()
    {
        var pointA = new double[6, 1] { { 1755 }, { 2750 }, { -626 }, { 0 }, { 0 }, { 0 } }; // first point circle
        var pointB = new double[6, 1] { { 1755 }, { 2562 }, { -438 }, { 0 }, { 0 }, { 0 } }; // mid point circle
        var pointC = new double[6, 1] { { 1755 }, { 2750 }, { -250 }, { 0 }, { 0 }, { 0 } }; // end point circle
        var pointControlC = new double[6, 1] { { 1755 }, { 2433.34 }, { -250 }, { 0 }, { 0 }, { 0 } };
        var pointControlD = new double[6, 1] { { 1755 }, { 2116 }, { -333 }, { 0 }, { 0 }, { 0 } };
        var pointD = new double[6, 1] { { 1755 }, { 1800 }, { -400 }, { 0 }, { 0 }, { 0 } }; // first point circle
        var pointE = new double[6, 1] { { 1755 }, { 1687 }, { -513 }, { 0 }, { 0 }, { 0 } }; // mid point circle
        var pointF = new double[6, 1] { { 1755 }, { 1800 }, { -626 }, { 0 }, { 0 }, { 0 } }; // end point circle
        var pointControlF = new double[6, 1] { { 1755 }, { 2020 }, { -600 }, { 0 }, { 0 }, { 0 } };
        var pointControlG = new double[6, 1] { { 1755 }, { 2248 }, { -370 }, { 0 }, { 0 }, { 0 } };
        var meetPointG = new double[6, 1] { { 1755 }, { 2468 }, { -150 }, { 0 }, { 0 }, { 0 } }; // middle point
        var pointControlGHa = new double[6, 1] { { 1755 }, { 2588.462 }, { -29.538 }, { 0 }, { 0 }, { 0 } };
        var pointCOntrolGHb = new double[6, 1] { { 1755 }, { 2689.769 }, { 136.231 }, { 0 }, { 0 }, { 0 } };
        var pointH = new double[6, 1] { { 1755 }, { 2750 }, { 76 }, { 0 }, { 0 }, { 0 } }; // top right corner
        var pointControlHGa = new double[6, 1] { { 1755 }, { 2810.231 }, { 15.769 }, { 0 }, { 0 }, { 0 } };
        var pointControlHGb = new double[6, 1] { { 1755 }, { 2588.462 }, { -150 }, { 0 }, { 0 }, { 0 } };
        var pointControlGIa = new double[6, 1] { { 1755 }, { 2130.682 }, { -150 }, { 0 }, { 0 }, { 0 } };
        var pointControlGIb = new double[6, 1] { { 1755 }, { 1798.317 }, { 87.317 }, { 0 }, { 0 }, { 0 } };
        var pointI = new double[6, 1] { { 1755 }, { 1461 }, { -250 }, { 0 }, { 0 }, { 0 } };
        var pointControlILa = new double[6, 1] { { 1755 }, { 1314.411 }, { -396.589 }, { 0 }, { 0 }, { 0 } };
        var pointControlILb = new double[6, 1] { { 1755 }, { 1201.411 }, { -675 }, { 0 }, { 0 }, { 0 } };
        var pointL = new double[6, 1] { { 1755 }, { 1348 }, { -675 }, { 0 }, { 0 }, { 0 } };
        var pointControlLMA = new double[6, 1] { { 1755 }, { 1685.258 }, { -675 }, { 0 }, { 0 }, { 0 } };
        var pointControlLMb = new double[6, 1] { { 1755 }, { 1670.049 }, { -205.95 }, { 0 }, { 0 }, { 0 } };
        var pointM = new double[6, 1] { { 1755 }, { 2026 }, { 150 }, { 0 }, { 0 }, { 0 } };

        var pathA = CirclePath(pointA, pointB, pointC, alpha: 180, circleSegmentSize: 3.6, quatSegmentSize: 4);
        var pathB = SplinePath(pointC, pointControlC, pointControlD, pointD, segmentSize: 1);
        var pathC = CirclePath(pointD, pointE, pointF, alpha: 180, circleSegmentSize: 3.6, quatSegmentSize: 4);
        var pathD = SplinePath(pointF, pointControlF, pointControlG, meetPointG, segmentSize: 2);
        var pathEa = SplinePath(meetPointG, pointControlGHa, pointCOntrolGHb, pointH, segmentSize: 2);
        var pathEb = SplinePath(pointH, pointControlHGa, pointControlHGb, meetPointG, segmentSize: 2);
        var pathF = SplinePath(meetPointG, pointControlGIa, pointControlGIb, pointI, segmentSize: 2);
        var pathG = SplinePath(pointI, pointControlILa, pointControlILb, pointL, segmentSize: 2);
        var pathH = SplinePath(pointL, pointControlLMA, pointControlLMb, pointM, segmentSize: 2);

        var totalPath = new double[pathA.GetLength(0) + pathB.GetLength(0) + pathC.GetLength(0) + pathD.GetLength(0) + pathEa.GetLength(0) + pathEb.GetLength(0) + pathF.GetLength(0) + pathG.GetLength(0) + pathH.GetLength(0), pathA.GetLength(1)];

        for (int rowA = 0; rowA < pathA.GetLength(0); rowA++)
        {
            for (int col = 0; col < pathA.GetLength(1); col++)
            {
                totalPath[rowA, col] = pathA[rowA, col];
            }
        }

        for (int row = 0; row < pathB.GetLength(0); row++)
        {
            for (int col = 0; col < pathB.GetLength(1); col++)
            {
                totalPath[row + pathA.GetLength(0), col] = pathB[row, col];
            }
        }

        for (int row = 0; row < pathC.GetLength(0); row++)
        {
            for (int col = 0; col < pathC.GetLength(1); col++)
            {
                totalPath[row + pathA.GetLength(0) + pathB.GetLength(0), col] = pathC[row, col];
            }
        }

        for (int row = 0; row < pathD.GetLength(0); row++)
        {
            for (int col = 0; col < pathD.GetLength(1); col++)
            {
                totalPath[row + pathA.GetLength(0) + pathB.GetLength(0) + pathC.GetLength(0), col] = pathD[row, col];
            }
        }

        for (int row = 0; row < pathEa.GetLength(0); row++)
        {
            for (int col = 0; col < pathEa.GetLength(1); col++)
            {
                totalPath[row + pathA.GetLength(0) + pathB.GetLength(0) + pathC.GetLength(0) + pathD.GetLength(0), col] = pathEa[row, col];
            }
        }

        for (int row = 0; row < pathEb.GetLength(0); row++)
        {
            for (int col = 0; col < pathEb.GetLength(1); col++)
            {
                totalPath[row + pathA.GetLength(0) + pathB.GetLength(0) + pathC.GetLength(0) + pathD.GetLength(0) + pathEa.GetLength(0), col] = pathEb[row, col];
            }
        }

        for (int row = 0; row < pathF.GetLength(0); row++)
        {
            for (int col = 0; col < pathF.GetLength(1); col++)
            {
                totalPath[row + pathA.GetLength(0) + pathB.GetLength(0) + pathC.GetLength(0) + pathD.GetLength(0) + pathEa.GetLength(0) + pathEb.GetLength(0), col] = pathF[row, col];
            }
        }

        for (int row = 0; row < pathG.GetLength(0); row++)
        {
            for (int col = 0; col < pathG.GetLength(1); col++)
            {
                totalPath[row + pathA.GetLength(0) + pathB.GetLength(0) + pathC.GetLength(0) + pathD.GetLength(0) + pathEa.GetLength(0) + pathEb.GetLength(0) + pathF.GetLength(0), col] = pathG[row, col];
            }
        }

        for (int row = 0; row < pathH.GetLength(0); row++)
        {
            for (int col = 0; col < pathH.GetLength(1); col++)
            {
                totalPath[row + pathA.GetLength(0) + pathB.GetLength(0) + pathC.GetLength(0) + pathD.GetLength(0) + pathEa.GetLength(0) + pathEb.GetLength(0) + pathF.GetLength(0) + pathG.GetLength(0), col] = pathH[row, col];
            }
        }

        return totalPath;
    }

    static double[,] TestPath(double[,] initialPoint, double[,] endPoint)
    {
        var result = LinePath(initialPoint, endPoint);
        return result;
    }

    void HoldObject(bool releaseTag)
    {
        if (Physics.Raycast(objectDetect.transform.position, objectDetect.TransformDirection(Vector3.right), out hitObject, rayDist) && hitObject.collider.tag == "Object" && releaseTag == false)
        {
            hitObject.collider.gameObject.transform.position = objectHolder.position;
            hitObject.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //Debug.DrawRay(objectDetect.transform.position, objectDetect.TransformDirection(Vector3.right) * 20f, Color.green);
            //Debug.Log(String.Format("Holding object"));
        }
        else if(releaseTag == true && hitObject.collider != null)
        {
            hitObject.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            //Debug.Log(String.Format("Release object"));
            //Debug.DrawRay(objectDetect.transform.position, objectDetect.TransformDirection(Vector3.right) * 20f, Color.red);
        }
    }

    bool BoxSensor()
    {
        bool flag = false;
        if(Physics.Raycast(boxSensor.transform.position, boxSensor.TransformDirection(Vector3.forward), out boxHit, 1f) && boxHit.collider.tag == "Object")
        {
            flag = true;
        }
        else
        {
            flag = false;
        }
        return flag;
    }

    void SpawnObject()
    {
        Instantiate(spawnObj, spawnerPoint.transform.position, Quaternion.identity, parent);
    }

    double[,] PalletPath1()
    {
        //var path1a = grabObjPath;
        var path1b = liftObjPath;
        var path1c = initialPatha;
        var path1d = SplinePath(midPoint, controlPoint1a, controlPoint1b, liftPoint1, segmentSize: 5);
        var path1e = LinePath(liftPoint1, point1, segmentSize: 5);
        var path1 = new double[path1b.GetLength(0) + path1c.GetLength(0) + path1d.GetLength(0) + path1e.GetLength(0), path1b.GetLength(1)];
        for (int i = 0; i < path1.GetLength(0); i++)
        {
            for (int j = 0; j < path1.GetLength(1); j++)
            {
                if(i < path1.GetLength(0)/4)
                {
                    path1[i, j] = path1b[i, j];
                }
                else if(i >= path1.GetLength(0)/4 && i < (path1.GetLength(0) - (2 * path1.GetLength(0)/4)))
                {
                    path1[i, j] = path1c[i - path1b.GetLength(0), j];
                }
                else if(i >= (2 * path1.GetLength(0)/4) && i < (path1.GetLength(0) - (path1.GetLength(0)/4)))
                {
                    path1[i, j] = path1d[i - (path1b.GetLength(0) + path1c.GetLength(0)), j];
                }
                else
                {
                    path1[i, j] = path1e[i - (path1b.GetLength(0) + path1c.GetLength(0) + path1d.GetLength(0)), j];
                }
            }
        }

        return path1;
    }

    double[,] PalletPath2()
    {
        var path2a = liftObjPath;
        var path2b = initialPatha;
        var path2c = SplinePath(midPoint, controlPoint2a, controlPoint2b, liftPoint2, segmentSize: 5);
        var path2d = LinePath(liftPoint2, point2, segmentSize: 5);
        var path2 = new double[path2a.GetLength(0) + path2b.GetLength(0) + path2c.GetLength(0) + path2d.GetLength(0), path2a.GetLength(1)];
        for(int i = 0; i < path2.GetLength(0); i ++)
        {
            for(int j = 0; j < path2.GetLength(1); j++)
            {
                if(i < path2.GetLength(0)/4)
                {
                    path2[i, j] = path2a[i, j];
                }
                else if(i >= path2.GetLength(0)/4 && i < (path2.GetLength(0) - (2 * (path2.GetLength(0)/4))))
                {
                    path2[i, j] = path2b[i - path2a.GetLength(0), j];
                }
                else if(i >= (2 * path2.GetLength(0)/4) && i < (path2.GetLength(0) - (path2.GetLength(0)/4)))
                {
                    path2[i, j] = path2c[i - (path2a.GetLength(0) + path2b.GetLength(0)), j];
                }
                else
                {
                    path2[i, j] = path2d[i - (path2a.GetLength(0) + path2b.GetLength(0) + path2c.GetLength(0)), j];
                }
            }
        }
        return path2;
    }

    double[,] PalletPath3()
    {
        var path3a = liftObjPath;
        var path3b = initialPatha;
        var path3c = SplinePath(midPoint, controlPoint3a, controlPoint3b, liftPoint3, segmentSize: 5);
        var path3d = LinePath(liftPoint3, point3, segmentSize: 5);
        var path3 = new double[path3a.GetLength(0) + path3b.GetLength(0) + path3c.GetLength(0) + path3d.GetLength(0), path3a.GetLength(1)];
        for(int i = 0; i < path3.GetLength(0); i++)
        {
            for(int j = 0; j < path3.GetLength(1); j++)
            {
                if(i < path3.GetLength(0)/4)
                {
                    path3[i, j] = path3a[i, j];
                }
                else if(i >= path3.GetLength(0)/4 && i < (path3.GetLength(0) - (2 * path3.GetLength(0)/4)))
                {
                    path3[i, j] = path3b[i - path3a.GetLength(0), j];
                }
                else if(i >= (2 * path3.GetLength(0)/4) && i < (path3.GetLength(0) - path3.GetLength(0)/4))
                {
                    path3[i, j] = path3c[i - (path3a.GetLength(0) + path3b.GetLength(0)), j];
                }
                else
                {
                    path3[i, j] = path3d[i - (path3a.GetLength(0) + path3b.GetLength(0) + path3c.GetLength(0)), j];
                }
            }
        }
        return path3;
    }

    double[,] PalletPath4()
    {
        var path4a = liftObjPath;
        var path4b = initialPatha;
        var path4c = SplinePath(midPoint, controlPoint4a, controlPoint4b, liftPoint4, segmentSize: 5);
        var path4d = LinePath(liftPoint4, point4, segmentSize: 5);
        var path4 = new double[path4a.GetLength(0) + path4b.GetLength(0) + path4c.GetLength(0) + path4d.GetLength(0), path4a.GetLength(1)];
        for(int i = 0; i < path4.GetLength(0); i++)
        {
            for(int j = 0; j < path4.GetLength(1); j++)
            {
                if(i < path4.GetLength(0)/4)
                {
                    path4[i, j] = path4a[i, j];
                }
                else if(i >= path4.GetLength(0)/4 && i < (path4.GetLength(0) - (2 * path4.GetLength(0)/4)))
                {
                    path4[i, j] = path4b[i - path4a.GetLength(0), j];
                }
                else if(i >= (2 * path4.GetLength(0)/4) && i < (path4.GetLength(0) - path4.GetLength(0)/4))
                {
                    path4[i, j] = path4c[i - (path4a.GetLength(0) + path4b.GetLength(0)), j];
                }
                else
                {
                    path4[i, j] = path4d[i - (path4a.GetLength(0) + path4b.GetLength(0) + path4c.GetLength(0)), j];
                }
            }
        }
        return path4;
    }

    double[,] PalletPath5()
    {
        var path5a = liftObjPath;
        var path5b = initialPathb;
        var path5c = SplinePath(midPointb, controlPoint5a, controlPoint5b, liftPoint5, segmentSize: 5);
        var path5d = LinePath(liftPoint5, point5, segmentSize: 5);
        var path5 = new double[path5a.GetLength(0) + path5b.GetLength(0) + path5c.GetLength(0) + path5d.GetLength(0), path5a.GetLength(1)];
        for(int i = 0; i < path5.GetLength(0); i++)
        {
            for(int j = 0; j < path5.GetLength(1); j++)
            {
                if(i < path5.GetLength(0)/4)
                {
                    path5[i, j] = path5a[i, j];
                }
                else if(i >= path5.GetLength(0)/4 && i < (path5.GetLength(0) - (2 * path5.GetLength(0)/4)))
                {
                    path5[i, j] = path5b[i - path5a.GetLength(0), j];
                }
                else if(i >= (2 * path5.GetLength(0)/4) && i < (path5.GetLength(0) - path5.GetLength(0)/4))
                {
                    path5[i, j] = path5c[i - (path5a.GetLength(0) + path5b.GetLength(0)), j];
                }
                else
                {
                    path5[i, j] = path5d[i - (path5a.GetLength(0) + path5b.GetLength(0) + path5c.GetLength(0)), j];
                }
            }
        }
        return path5;
    }

    double[,] PalletPath6()
    {
        var path6a = liftObjPath;
        var path6b = initialPathb;
        var path6c = SplinePath(midPointb, controlPoint6a, controlPoint6b, liftPoint6, segmentSize: 5);
        var path6d = LinePath(liftPoint6, point6, segmentSize: 5);
        var path6 = new double[path6a.GetLength(0) + path6b.GetLength(0) + path6c.GetLength(0) + path6d.GetLength(0), path6a.GetLength(1)];
        for(int i = 0; i < path6.GetLength(0); i++)
        {
            for(int j = 0; j < path6.GetLength(1); j++)
            {
                if(i < path6.GetLength(0)/4)
                {
                    path6[i, j] = path6a[i, j];
                }
                else if (i >= path6.GetLength(0)/4 && i < (path6.GetLength(0) - (2 * path6.GetLength(0)/4)))
                {
                    path6[i, j] = path6b[i - path6a.GetLength(0), j];
                }
                else if(i >= (2 * path6.GetLength(0)/4) && i < (path6.GetLength(0) - path6.GetLength(0)/4))
                {
                    path6[i, j] = path6c[i - (path6a.GetLength(0) + path6b.GetLength(0)), j];
                }
                else
                {
                    path6[i, j] = path6d[i - (path6a.GetLength(0) + path6b.GetLength(0) + path6c.GetLength(0)), j];
                }
            }
        }
        return path6;
    }

    double[,] PalletPath7()
    {
        var path7a = liftObjPath;
        var path7b = initialPathb;
        var path7c = SplinePath(midPointb, controlPoint7a, controlPoint7b, liftPoint7, segmentSize: 5);
        var path7d = LinePath(liftPoint7, point7, segmentSize: 5);
        var path7 = new double[path7a.GetLength(0) + path7b.GetLength(0) + path7c.GetLength(0) + path7d.GetLength(0), path7a.GetLength(1)];
        for(int i = 0; i < path7.GetLength(0); i++)
        {
            for(int j = 0; j < path7.GetLength(1); j++)
            {
                if(i < path7.GetLength(0)/4)
                {
                    path7[i, j] = path7a[i, j];
                }
                else if(i >= path7.GetLength(0)/4 && i < (path7.GetLength(0) - (2 * path7.GetLength(0)/4)))
                {
                    path7[i, j] = path7b[i - path7a.GetLength(0), j];
                }
                else if(i >= (2 * path7.GetLength(0)/4) && i < (path7.GetLength(0) - path7.GetLength(0)/4))
                {
                    path7[i, j] = path7c[i - (path7a.GetLength(0) + path7b.GetLength(0)), j];
                }
                else
                {
                    path7[i, j] = path7d[i - (path7a.GetLength(0) + path7b.GetLength(0) + path7c.GetLength(0)), j];
                }
            }
        }
        return path7;
    }

    double[,] PalletPath8()
    {
        var path8a = liftObjPath;
        var path8b = initialPathb;
        var path8c = SplinePath(midPointb, controlPoint8a, controlPoint8b, liftPoint8, segmentSize: 5);
        var path8d = LinePath(liftPoint8, point8, segmentSize: 5);
        var path8 = new double[path8a.GetLength(0) + path8b.GetLength(0) + path8c.GetLength(0) + path8d.GetLength(0), path8a.GetLength(1)];
        for(int i = 0; i < path8.GetLength(0); i++)
        {
            for(int j = 0; j < path8.GetLength(1); j++)
            {
                if(i < path8.GetLength(0)/4)
                {
                    path8[i, j] = path8a[i, j];
                }
                else if(i >= path8.GetLength(0)/4 && i < (path8.GetLength(0) - (2 * path8.GetLength(0)/4)))
                {
                    path8[i, j] = path8b[i - path8a.GetLength(0), j];
                }
                else if(i >= (2 * path8.GetLength(0)/4) && i < (path8.GetLength(0) - path8.GetLength(0)/4))
                {
                    path8[i, j] = path8c[i - (path8a.GetLength(0) + path8b.GetLength(0)), j];
                }
                else
                {
                    path8[i, j] = path8d[i - (path8a.GetLength(0) + path8b.GetLength(0) + path8c.GetLength(0)), j];
                }
            }
        }
        return path8;
    }

    double[,] PalletPath(double[,] initialPath, double[,] initialPoint, double[,] controlPointa, double[,] controlPointb, double[,] liftTargetPoint, double[,] targetPoint )
    {
        var patha = liftObjPath;
        var pathb = initialPath;
        var pathc = SplinePath(initialPoint, controlPointa, controlPointb, liftTargetPoint, segmentSize: 5);
        var pathd = LinePath(liftTargetPoint, targetPoint, segmentSize: 5);
        var path = new double[patha.GetLength(0) + pathb.GetLength(0) + pathc.GetLength(0) + pathd.GetLength(0), patha.GetLength(1)];
        for(int i = 0; i < path.GetLength(0); i++)
        {
            for(int j = 0; j < path.GetLength(1); j++)
            {
                if(i < path.GetLength(0)/4)
                {
                    path[i, j] = patha[i, j];
                }
                else if(i >= path.GetLength(0)/4 && i < (path.GetLength(0) - (2 * path.GetLength(0)/4)))
                {
                    path[i, j] = pathb[i - patha.GetLength(0), j];
                }
                else if(i >= (2 * path.GetLength(0)/4) && i < (path.GetLength(0) - path.GetLength(0)/4))
                {
                    path[i, j] = pathc[i - (patha.GetLength(0) + pathb.GetLength(0)), j];
                }
                else
                {
                    path[i, j] = pathd[i - (patha.GetLength(0) + pathb.GetLength(0) + pathc.GetLength(0)), j];
                }
            }
        }
        return path;
    }
}