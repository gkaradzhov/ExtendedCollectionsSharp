using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtendedCollections.Tests
{
    [TestClass]
    public class CircullarBuffer
    {
        [TestMethod]
        public void ReadAndWriteSimple()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Write(2);
            buffer.Write(3);
            buffer.Write(4);

            Assert.AreEqual(2,buffer.Read());
            Assert.AreEqual(3,buffer.Read());
            Assert.AreEqual(4,buffer.Read());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReadEmpty()
        {
            var buffer = new CircularBuffer<int>(4);
            buffer.Read();
        }

        [TestMethod]
        public void ReadWithOverflow()
        {
            var buffer = new CircularBuffer<int>(4);
            buffer.Write(3);
            buffer.Write(4);
            buffer.Write(5);
            buffer.Write(6);
            buffer.Write(7);

            Assert.AreEqual(4,buffer.Read());
            Assert.AreEqual(5, buffer.Read());
            Assert.AreEqual(6, buffer.Read());
            Assert.AreEqual(7, buffer.Read());
        }

        [TestMethod]
        public void IterationWithPeek()
        {
            var buffer = new CircularBuffer<int>(4);
            buffer.Write(3);
            buffer.Write(4);
            buffer.Write(5);
            buffer.Write(6);

            var position = 0;
            foreach (var element in buffer)
            {
                Assert.AreEqual(buffer.Peek(position),element);
                position++;
            }

            Assert.AreEqual(3, buffer.Read());
            Assert.AreEqual(4, buffer.Read());
            Assert.AreEqual(5, buffer.Read());
            Assert.AreEqual(6, buffer.Read());
        }

        [TestMethod]
        public void IsEmpty()
        {
            var buffer = new CircularBuffer<int>(4);
            Assert.IsTrue(buffer.IsEmpty);
        }

        [TestMethod]
        public void IsFull()
        {
            var buffer = new CircularBuffer<int>(4);

            buffer.Write(3);
            buffer.Write(4);
            buffer.Write(5);
            buffer.Write(6);
            buffer.Write(7);

            Assert.IsTrue(buffer.IsFull);
        }

        [TestMethod]
        public void Clear()
        {
            var buffer = new CircularBuffer<int>(4);

            buffer.Write(3);
            buffer.Write(4);
            buffer.Write(5);
            buffer.Write(6);
            buffer.Write(7);
            buffer.Clear();

            Assert.IsTrue(buffer.IsEmpty);
        }

        [TestMethod]
        public void CreateFromExistingCollection()
        {
            var list = new List<string>
            {
                "Working",
                "with",
                "collections",
                "is cool",
                "GGWP"
            };
            var buffer = new CircularBuffer<string>(list);
            Assert.IsTrue(buffer.IsFull);
            foreach (string item in list)
            {
                Assert.AreEqual(item, buffer.Read());
                Assert.IsFalse(buffer.IsFull);
            }
            Assert.IsTrue(buffer.IsEmpty);
        }   
    }
}
