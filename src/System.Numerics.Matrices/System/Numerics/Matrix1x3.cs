// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE file in the project root for full license information. 

using System.Runtime.InteropServices;

namespace System.Numerics.Matrices
{
    /// <summary>
    /// Represents a matrix of double precision floating-point values defined by its number of columns and rows
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix1x3: IEquatable<Matrix1x3>, IMatrix
    {
        public const int ColumnCount = 1;
        public const int RowCount = 3;

        static Matrix1x3()
        {
            Zero = new Matrix1x3(0);
        }

        /// <summary>
        /// Gets the smallest value used to determine equality
        /// </summary>
        public double Epsilon { get { return MatrixHelper.Epsilon; } }

        /// <summary>
        /// Constant Matrix1x3 with all values initialized to zero
        /// </summary>
        public static readonly Matrix1x3 Zero;

        /// <summary>
        /// Initializes a Matrix1x3 with all of it values specifically set
        /// </summary>
        /// <param name="m11">The column 1, row 1 value</param>
        /// <param name="m12">The column 1, row 2 value</param>
        /// <param name="m13">The column 1, row 3 value</param>
        public Matrix1x3(double m11, 
                         double m12, 
                         double m13)
        {
            M11 = m11; 
            M12 = m12; 
            M13 = m13; 
        }

        /// <summary>
        /// Initialized a Matrix1x3 with all values set to the same value
        /// </summary>
        /// <param name="value">The value to set all values to</param>
        public Matrix1x3(double value)
        {
            M11 = 
            M12 = 
            M13 = value;
        }

        public double M11;
        public double M12;
        public double M13;

        public unsafe double this[int col, int row]
        {
            get
            {
                if (col < 0 || col >= ColumnCount)
                    throw new ArgumentOutOfRangeException("col", String.Format("Expected greater than or equal to 0 and less than {0}, found {1}.", ColumnCount, col));
                if (row < 0 || row >= RowCount)
                    throw new ArgumentOutOfRangeException("row", String.Format("Expected greater than or equal to 0 and less than {0}, found {1}.", RowCount, row));

                fixed (Matrix1x3* p = &this)
                {
                    double* d = (double*)p;
                    return d[row * ColumnCount + col];
                }
            }
            set
            {
                if (col < 0 || col >= ColumnCount)
                    throw new ArgumentOutOfRangeException("col", String.Format("Expected greater than or equal to 0 and less than {0}, found {1}.", ColumnCount, col));
                if (row < 0 || row >= RowCount)
                    throw new ArgumentOutOfRangeException("row", String.Format("Expected greater than or equal to 0 and less than {0}, found {1}.", RowCount, row));

                fixed (Matrix1x3* p = &this)
                {
                    double* d = (double*)p;
                    d[row * ColumnCount + col] = value;
                }
            }
        }

        /// <summary>
        /// Gets the number of columns in the matrix
        /// </summary>
        public int Columns { get { return ColumnCount; } }
        /// <summary>
        /// Get the number of rows in the matrix
        /// </summary>
        public int Rows { get { return RowCount; } }


        public override bool Equals(object obj)
        {
            if (obj is Matrix1x3)
                return this == (Matrix1x3)obj;

            return false;
        }

        public bool Equals(Matrix1x3 other)
        {
            return this == other;
        }

        public unsafe override int GetHashCode()
        {
            fixed (Matrix1x3* p = &this)
            {
                int* x = (int*)p;
                unchecked
                {
                    return 0xFFE1
                         + 7 * ((((x[00] ^ x[01]) << 0)) << 0)
                         + 7 * ((((x[01] ^ x[02]) << 0)) << 1)
                         + 7 * ((((x[02] ^ x[03]) << 0)) << 2);
                }
            }
        }

        public override string ToString()
        {
            return "Matrix1x3: "
                 + String.Format("{{|{0:00}|}}", M11)
                 + String.Format("{{|{0:00}|}}", M12)
                 + String.Format("{{|{0:00}|}}", M13); 
        }

        /// <summary>
        /// Creates and returns a transposed matrix
        /// </summary>
        /// <returns>Matrix with transposed values</returns>
        public Matrix3x1 Transpose()
        {
            return new Matrix3x1(M11, M12, M13);
        }

        public static bool operator ==(Matrix1x3 matrix1, Matrix1x3 matrix2)
        {
            return MatrixHelper.AreEqual(matrix1.M11, matrix2.M11)
                && MatrixHelper.AreEqual(matrix1.M12, matrix2.M12)
                && MatrixHelper.AreEqual(matrix1.M13, matrix2.M13);
        }

        public static bool operator !=(Matrix1x3 matrix1, Matrix1x3 matrix2)
        {
            return MatrixHelper.NotEqual(matrix1.M11, matrix2.M11)
                || MatrixHelper.NotEqual(matrix1.M12, matrix2.M12)
                || MatrixHelper.NotEqual(matrix1.M13, matrix2.M13);
        }

        public static Matrix1x3 operator +(Matrix1x3 matrix1, Matrix1x3 matrix2)
        {
            double m11 = matrix1.M11 + matrix2.M11;
            double m12 = matrix1.M12 + matrix2.M12;
            double m13 = matrix1.M13 + matrix2.M13;

            return new Matrix1x3(m11, 
                                 m12, 
                                 m13);
        }

        public static Matrix1x3 operator -(Matrix1x3 matrix1, Matrix1x3 matrix2)
        {
            double m11 = matrix1.M11 - matrix2.M11;
            double m12 = matrix1.M12 - matrix2.M12;
            double m13 = matrix1.M13 - matrix2.M13;

            return new Matrix1x3(m11, 
                                 m12, 
                                 m13);
        }

        public static Matrix1x3 operator *(Matrix1x3 matrix, double scalar)
        {
            double m11 = matrix.M11 * scalar;
            double m12 = matrix.M12 * scalar;
            double m13 = matrix.M13 * scalar;

            return new Matrix1x3(m11, 
                                 m12, 
                                 m13);
        }

        public static Matrix1x3 operator *(double scalar, Matrix1x3 matrix)
        {
            double m11 = scalar * matrix.M11;
            double m12 = scalar * matrix.M12;
            double m13 = scalar * matrix.M13;

            return new Matrix1x3(m11, 
                                 m12, 
                                 m13);
        }

        public static Matrix2x3 operator *(Matrix1x3 matrix1, Matrix2x1 matrix2)
        {
            double m11 = matrix1.M11 * matrix2.M11;
            double m21 = matrix1.M11 * matrix2.M21;
            double m12 = matrix1.M12 * matrix2.M11;
            double m22 = matrix1.M12 * matrix2.M21;
            double m13 = matrix1.M13 * matrix2.M11;
            double m23 = matrix1.M13 * matrix2.M21;

            return new Matrix2x3(m11, m21, 
                                 m12, m22, 
                                 m13, m23);
        }
        public static Matrix3x3 operator *(Matrix1x3 matrix1, Matrix3x1 matrix2)
        {
            double m11 = matrix1.M11 * matrix2.M11;
            double m21 = matrix1.M11 * matrix2.M21;
            double m31 = matrix1.M11 * matrix2.M31;
            double m12 = matrix1.M12 * matrix2.M11;
            double m22 = matrix1.M12 * matrix2.M21;
            double m32 = matrix1.M12 * matrix2.M31;
            double m13 = matrix1.M13 * matrix2.M11;
            double m23 = matrix1.M13 * matrix2.M21;
            double m33 = matrix1.M13 * matrix2.M31;

            return new Matrix3x3(m11, m21, m31, 
                                 m12, m22, m32, 
                                 m13, m23, m33);
        }
        public static Matrix4x3 operator *(Matrix1x3 matrix1, Matrix4x1 matrix2)
        {
            double m11 = matrix1.M11 * matrix2.M11;
            double m21 = matrix1.M11 * matrix2.M21;
            double m31 = matrix1.M11 * matrix2.M31;
            double m41 = matrix1.M11 * matrix2.M41;
            double m12 = matrix1.M12 * matrix2.M11;
            double m22 = matrix1.M12 * matrix2.M21;
            double m32 = matrix1.M12 * matrix2.M31;
            double m42 = matrix1.M12 * matrix2.M41;
            double m13 = matrix1.M13 * matrix2.M11;
            double m23 = matrix1.M13 * matrix2.M21;
            double m33 = matrix1.M13 * matrix2.M31;
            double m43 = matrix1.M13 * matrix2.M41;

            return new Matrix4x3(m11, m21, m31, m41, 
                                 m12, m22, m32, m42, 
                                 m13, m23, m33, m43);
        }
    }
}
