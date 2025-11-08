using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab2.Vectors;
using System;

namespace Lab2.Tests
{
    [TestClass]
    public class VectorAllTests
    {
        // --- Арифметика ---
        [TestMethod]
        public void Plus_Adds_Componentwise()
        {
            var a = new Vector(1.0, 2.0, 3.0);
            var b = new Vector(4.0, -2.0, 0.5);
            var c = a + b;

            Assert.AreEqual(5.0, c[0], 1e-9);
            Assert.AreEqual(0.0, c[1], 1e-9);
            Assert.AreEqual(3.5, c[2], 1e-9);
        }

        [TestMethod]
        public void Minus_Subtracts_Componentwise()
        {
            var a = new Vector(5.0, 2.0);
            var b = new Vector(1.0, -3.0);
            var c = a - b;

            Assert.AreEqual(4.0, c[0], 1e-9);
            Assert.AreEqual(5.0, c[1], 1e-9);
        }

        [TestMethod]
        public void Plus_Throws_OnDifferentLengths()
        {
            var a = new Vector(1.0, 2.0);
            var b = new Vector(3.0);
            Assert.ThrowsException<ArgumentException>(() => { var _ = a + b; });
        }

        // --- Сравнение по норме ---
        [TestMethod]
        public void Greater_And_Less_ByNorm()
        {
            var a = new Vector(3.0, 4.0);   // норма 5
            var b = new Vector(6.0, 8.0);   // норма 10
            Assert.IsTrue(b > a);
            Assert.IsTrue(a < b);
        }

        // --- Равенство ---
        [TestMethod]
        public void Equality_Componentwise_WithEps()
        {
            var a = new Vector(1.0, 2.0, 3.0);
            var b = new Vector(1.0, 2.0, 3.0);
            var c = new Vector(1.0, 2.0, 3.001);

            Assert.IsTrue(a == b);
            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        // --- Индексатор и базовые свойства ---
        [TestMethod]
        public void Indexer_GetSet_And_Length_IsEmpty()
        {
            var empty = new Vector();
            var v = new Vector(3);
            v[0] = 10; v[1] = -2.5; v[2] = 0;

            Assert.AreEqual(0, empty.Length);
            Assert.IsTrue(empty.IsEmpty);

            Assert.AreEqual(3, v.Length);
            Assert.IsFalse(v.IsEmpty);
            Assert.AreEqual(10.0, v[0], 1e-9);
            Assert.AreEqual(-2.5, v[1], 1e-9);
            Assert.AreEqual(0.0, v[2], 1e-9);
        }

        // --- Операторы true/false (по варианту: true -> пустой вектор) ---
        [TestMethod]
        public void TrueFalse_EmptyVector_IsTrue()
        {
            var empty = new Vector();
            var nonEmpty = new Vector(1.0);

            if (empty) { Assert.IsTrue(true); } else { Assert.Fail("Пустой вектор должен быть true."); }
            if (nonEmpty) { Assert.Fail("Непустой не должен быть true."); } else { Assert.IsTrue(true); }
        }

        // --- Методы расширения ---
        [TestMethod]
        public void TruncateStart_Works()
        {
            var s = "abcdef";
            Assert.AreEqual("cdef", s.TruncateStart(2));
            Assert.AreEqual("", s.TruncateStart(100));
            Assert.AreEqual("abcdef", s.TruncateStart(0));
            Assert.AreEqual("abcdef", s.TruncateStart(-5));
        }

        [TestMethod]
        public void RemovePositive_Removes_PositiveItems()
        {
            var v = new Vector(1, -2, 0, 3, -1);
            var r = v.RemovePositive();

            Assert.AreEqual(3, r.Length);
            Assert.AreEqual(-2.0, r[0], 1e-9);
            Assert.AreEqual(0.0, r[1], 1e-9);
            Assert.AreEqual(-1.0, r[2], 1e-9);
        }
    }
}
