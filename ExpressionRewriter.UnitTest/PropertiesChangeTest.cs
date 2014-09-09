using System;
using System.Linq.Expressions;
using ExpressionRewriting.UnitTest.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionRewriting.UnitTest
{
    [TestClass]
    public class PropertiesChangeTest
    {
        private Expression<Func<VariableData, object>> _source;
        private Expression<Func<VariableInfo, object>> _target;

        private PropertiesChange _change;

        [TestInitialize]
        public void TestInitialize()
        {
            _source = vd => vd.Storage.PhysicalSchemaName;
            _target = vi => vi.Placement.PhysicalSchemaName;

            _change = GetPropertiesChange();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldThrowException_IfSequencesOfPropertiesHaveDifferentResultTypes()
        {
            _source = vd => vd.Storage.PhysicalSchemaName;
            _target = vi => vi.StorageType;

            GetPropertiesChange();
        }

        [TestMethod]
        public void SourceCorrespondsTo_ShouldReturnFalse_IfPropertiesSequenceDoesNotCorrespondToSource()
        {
            Expression<Func<VariableData, string>> ex = vd => vd.Name;

            Assert.IsFalse(_change.SourceCorrespondsTo(ex.Body));
        }

        [TestMethod]
        public void SourceCorrespondsTo_ShouldReturnTrue_IfPropertiesSequenceCorrespondsToSource()
        {
            Expression<Func<VariableData, string>> ex = vd => vd.Storage.PhysicalSchemaName;

            Assert.IsTrue(_change.SourceCorrespondsTo(ex.Body));
        }

        [TestMethod]
        public void GetSequenceOriginExpression_ShouldReturnNull_IfPropertiesSequenceDoesNotCorrespondToSource()
        {
            Expression<Func<VariableData, string>> ex = vd => vd.Name;

            Assert.IsNull(_change.GetSequenceOriginExpression(ex.Body));
        }

        [TestMethod]
        public void GetSequenceOriginExpression_ShouldReturnNotNull_IfPropertiesSequenceCorrespondsToSource()
        {
            Expression<Func<VariableData, string>> ex = vd => vd.Storage.PhysicalSchemaName;

            Assert.AreEqual(typeof(VariableData), _change.GetSequenceOriginExpression(ex.Body).Type);
        }

        private PropertiesChange GetPropertiesChange()
        {
            return new PropertiesChange(
                new PropertiesSequence(_source.Body),
                new PropertiesSequence(_target.Body)
                );
        }
    }
}