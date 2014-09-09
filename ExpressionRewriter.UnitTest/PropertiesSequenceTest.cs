using System;
using System.Linq.Expressions;
using ExpressionRewriting.UnitTest.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionRewriting.UnitTest
{
    [TestClass]
    public class PropertiesSequenceTest
    {
        private Expression<Func<VariableData, object>> _expression;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldThrowException_IfArgumentIsNull()
        {
            new PropertiesSequence(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldThrowException_IfArgumentIsNotSequenceOfProperties()
        {
            _expression = vd => string.IsNullOrEmpty(vd.Name);

            new PropertiesSequence(_expression.Body);
        }

        [TestMethod]
        public void Constructor_ShouldFillPropertiesCorrectly_IfOnlyOnePropertyInSequence()
        {
            _expression = vd => vd.Name;

            var sequence = new PropertiesSequence(_expression.Body);
            
            Assert.AreEqual(typeof(VariableData), sequence.SequenceOriginType);
            Assert.AreEqual(1, sequence.Properties.Length);
            Assert.AreEqual("Name", sequence.Properties[0].Name);
            Assert.AreEqual(typeof(String), sequence.Properties[0].ResultType);
        }

        [TestMethod]
        public void Constructor_ShouldFillPropertiesCorrectly_IfSeveralPropertiesInSequence()
        {
            _expression = vd => vd.Storage.PhysicalSchemaName;

            var sequence = new PropertiesSequence(_expression.Body);

            Assert.AreEqual(typeof(VariableData), sequence.SequenceOriginType);
            Assert.AreEqual(2, sequence.Properties.Length);
            Assert.AreEqual("PhysicalSchemaName", sequence.Properties[0].Name);
            Assert.AreEqual(typeof(String), sequence.Properties[0].ResultType);
            Assert.AreEqual("Storage", sequence.Properties[1].Name);
            Assert.AreEqual(typeof(VariableStorage), sequence.Properties[1].ResultType);
        }
    }
}