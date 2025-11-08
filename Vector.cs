using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.Vectors
{
    public class Vector : IEquatable<Vector>, ICloneable
    {
        private readonly double[] _data;

        public const double EPS = 1e-9;

        public int Length => _data.Length;
        public bool IsEmpty => Length == 0;

        public double this[int index]
        {
            get => _data[index];
            set => _data[index] = value;
        }

        public Vector()
        {
            _data = Array.Empty<double>();
        }

        public Vector(int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));
            _data = new double[length];
        }

        public Vector(params double[] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            _data = (double[])values.Clone();
        }

        public Vector(IEnumerable<double> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            _data = values.ToArray();
        }

        public Vector(Vector other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            _data = (double[])other._data.Clone();
        }

        public double Norm()
        {
            double sum = 0;
            for (int i = 0; i < _data.Length; i++)
                sum += _data[i] * _data[i];
            return Math.Sqrt(sum);
        }

        public object Clone() => new Vector(this);

        // ----- арифметика -----
        public static Vector operator +(Vector a, Vector b)
        {
            RequireSameLength(a, b);
            var res = new double[a.Length];
            for (int i = 0; i < res.Length; i++) res[i] = a._data[i] + b._data[i];
            return new Vector(res);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            RequireSameLength(a, b);
            var res = new double[a.Length];
            for (int i = 0; i < res.Length; i++) res[i] = a._data[i] - b._data[i];
            return new Vector(res);
        }

        // ----- сравнение по норме -----
        public static bool operator >(Vector a, Vector b)
        {
            EnsureNotNull(a, nameof(a));
            EnsureNotNull(b, nameof(b));
            return a.Norm() > b.Norm() + EPS;
        }

        public static bool operator <(Vector a, Vector b)
        {
            EnsureNotNull(a, nameof(a));
            EnsureNotNull(b, nameof(b));
            return a.Norm() + EPS < b.Norm();
        }

        // ----- равенство покомпонентно -----
        public static bool operator ==(Vector a, Vector b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
                if (Math.Abs(a._data[i] - b._data[i]) > EPS) return false;
            return true;
        }

        public static bool operator !=(Vector a, Vector b) => !(a == b);

        public bool Equals(Vector other) => this == other;

        public override bool Equals(object obj)
        {
            var v = obj as Vector;
            return v != null && this == v;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Length;
                for (int i = 0; i < _data.Length && i < 4; i++)
                    hash = hash * 31 + _data[i].GetHashCode();
                return hash;
            }
        }

        // ----- логические true/false -----
        public static bool operator true(Vector v)
        {
            EnsureNotNull(v, nameof(v));
            return v.IsEmpty;
        }

        public static bool operator false(Vector v)
        {
            EnsureNotNull(v, nameof(v));
            return !v.IsEmpty;
        }

        // ----- служебное -----
        private static void RequireSameLength(Vector a, Vector b)
        {
            EnsureNotNull(a, nameof(a));
            EnsureNotNull(b, nameof(b));
            if (a.Length != b.Length)
                throw new ArgumentException("Размерности векторов должны совпадать.");
        }

        private static void EnsureNotNull(Vector v, string name)
        {
            if ((object)v == null) throw new ArgumentNullException(name);
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", _data.Select(x => x.ToString("0.###")))}]";
        }
    }
}
