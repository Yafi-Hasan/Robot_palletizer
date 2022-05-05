using System;
public class MatrixOperation
{
    public static double[,] SumOperation(double[,] matrixA, double[,] matrixB)
    {
        var matrixC = new double[matrixA.GetLength(0), matrixA.GetLength(1)];

        if (matrixA.GetLength(0) == matrixB.GetLength(0) && matrixA.GetLength(1) == matrixB.GetLength(1))
        {
            for (int i = 0; i < matrixA.GetLength(0); i++)
            {
                for (int j = 0; j < matrixA.GetLength(1); j++)
                {
                    matrixC[i, j] = matrixA[i, j] + matrixB[i, j];
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid matrix size for sum opration");
        }
        return matrixC;
    }

    public static double[,] SubstractionOperation(double[,] matrixA, double[,] matrixB)
    {
        var matrixC = new double[matrixA.GetLength(0), matrixA.GetLength(1)];

        if (matrixA.GetLength(0) == matrixB.GetLength(0) && matrixA.GetLength(1) == matrixB.GetLength(1))
        {
            for (int i = 0; i < matrixA.GetLength(0); i++)
            {
                for (int j = 0; j < matrixA.GetLength(1); j++)
                {
                    matrixC[i, j] = matrixA[i, j] - matrixB[i, j];
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid matrix size for substraction opration");
        }
        return matrixC;
    }

    public static double[,] MultiplicationOperation(double[,] matrixA, double[,] matrixB)
    {
        var matrixC = new double[matrixA.GetLength(0), matrixB.GetLength(1)];

        if (matrixA.GetLength(1) == matrixB.GetLength(0))
        {
            for (int i = 0; i < matrixA.GetLength(0); i++)
            {
                for (int j = 0; j < matrixB.GetLength(1); j++)
                {
                    matrixC[i, j] = 0;
                    for (int k = 0; k < matrixA.GetLength(1); k++)
                    {
                        try
                        {
                            matrixC[i, j] = matrixC[i, j] + (matrixA[i, k] * matrixB[k, j]);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid matrix size for multiplication operation");
        }
        return matrixC;
    }

    public static double[,] ScalarMultiplicationOperation(double scalar, double[,] matrixA)
    {
        var newMatrix = new double[matrixA.GetLength(0), matrixA.GetLength(1)];

        for (int i = 0; i < matrixA.GetLength(0); i++)
        {
            for (int j = 0; j < matrixA.GetLength(1); j++)
            {
                newMatrix[i, j] = scalar * matrixA[i, j];
            }
        }
        return newMatrix;
    }

    public static double[,] CrossProductOperation(double[,] matrixA, double[,] matrixB)
    {
        var matrixC = new double[matrixA.GetLength(0), matrixA.GetLength(1)];
        if (matrixA.GetLength(0) == matrixB.GetLength(0) && matrixA.GetLength(1) == matrixB.GetLength(1) && matrixA.GetLength(0) == 3 && matrixA.GetLength(1) == 1)
        {
            matrixC[0, 0] = (matrixA[1, 0] * matrixB[2, 0]) - (matrixA[2, 0] * matrixB[1, 0]);
            matrixC[1, 0] = (matrixA[2, 0] * matrixB[0, 0]) - (matrixA[0, 0] * matrixB[2, 0]);
            matrixC[2, 0] = (matrixA[0, 0] * matrixB[1, 0]) - (matrixA[1, 0] * matrixB[0, 0]);
        }
        else
        {
            Console.WriteLine("Invalid matrix size for cross product operation");
        }
        return matrixC;
    }

    public static double DotProductVectorOperation(double[,] matrixA, double[,] matrixB)
    {
        var matrixC = new double[matrixA.GetLength(0), matrixA.GetLength(1)];
        for (int i = 0; i < matrixC.GetLength(0); i++)
        {
            matrixC[i, 0] = matrixA[i, 0] * matrixB[i, 0];
        }

        var result = matrixC[0, 0] + matrixC[1, 0] + matrixC[2, 0];
        return result;
    }

    public static double[,] DivisionOperation(double[,] matrixA, double[,] matrixB)
    {
        var matrixC = new double[matrixA.GetLength(0), matrixB.GetLength(1)];

        if (matrixA.GetLength(1) == matrixB.GetLength(0))
        {
            for (int i = 0; i < matrixA.GetLength(0); i++)
            {
                for (int j = 0; j < matrixB.GetLength(1); j++)
                {
                    matrixC[i, j] = 0;
                    for (int k = 0; k < matrixA.GetLength(1); k++)
                    {
                        try
                        {
                            matrixC[i, j] = matrixC[i, j] + (matrixA[i, k] * matrixB[k, j]);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid matrix size for division operation");
        }
        return matrixC;
    }

    public static double[,] IdentitiyMatrix(int rowSize, int colSize)
    {
        var id_matrix = new double[rowSize, colSize];

        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                if (i == j)
                {
                    id_matrix[i, j] = 1;
                }
                else
                {
                    id_matrix[i, j] = 0;
                }
            }
        }
        return id_matrix;
    }

    public static double[,] ZeroMatrix(int rowSize, int colSize)
    {
        var zeroMatrix = new double[rowSize, colSize];

        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                zeroMatrix[i, j] = 0;
            }
        }
        return zeroMatrix;
    }

    public static double[,] TransposeMatrix(double[,] matrixA)
    {
        var transposeMatrix = new double[matrixA.GetLength(1), matrixA.GetLength(0)];
        for (int i = 0; i < transposeMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < transposeMatrix.GetLength(1); j++)
            {
                try
                {
                    transposeMatrix[i, j] = matrixA[j, i];
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
        return transposeMatrix;
    }

    public static double NormVectorOperation(double[,] pointInSpace)
    {
        var result = Math.Sqrt((pointInSpace[0, 0] * pointInSpace[0, 0]) + (pointInSpace[1, 0] * pointInSpace[1, 0]) + (pointInSpace[2, 0] * pointInSpace[2, 0]));
        return result;
    }

    public static double[,] UnitVectorOperation(double[,] vector)
    {
        var newvector = new double[vector.GetLength(0), vector.GetLength(1)];
        var magnitude = Math.Sqrt((vector[0, 0] * vector[0, 0]) + (vector[1, 0] * vector[1, 0]) + (vector[2, 0] * vector[2, 0]));
        for (int i = 0; i < vector.GetLength(0); i++)
        {
            newvector[i, 0] = vector[i, 0] / magnitude;
        }
        return newvector;
    }

    public static double NormQuaternionOperation(double[,] quaternion)
    {
        var result = Math.Sqrt((quaternion[0, 0] * quaternion[0, 0]) + (quaternion[1, 0] * quaternion[1, 0]) + (quaternion[2, 0] * quaternion[2, 0]) + (quaternion[3, 0] * quaternion[3, 0]));
        return result;
    }

    public static double[,] UnitQuaternions(double[,] quaternion)
    {
        var unitq = new double[quaternion.GetLength(0), quaternion.GetLength(1)];
        var magnitude = Math.Sqrt((quaternion[0, 0] * quaternion[0, 0]) + (quaternion[1, 0] * quaternion[1, 0]) + (quaternion[2, 0] * quaternion[2, 0]) + (quaternion[3, 0] * quaternion[3, 0]));
        for (int i = 0; i < unitq.GetLength(0); i++)
        {
            unitq[i, 0] = quaternion[i, 0] / magnitude;
        }
        return unitq;
    }
}
