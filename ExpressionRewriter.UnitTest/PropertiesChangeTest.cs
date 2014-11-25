using System;
using System.Linq.Expressions;
using ExpressionRewriting.UnitTest.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionRewriting.UnitTest
{
    [TestClass]
    public class PropertiesChangeTest
    {
        private Expression<Func<Person, object>> _source;
        private Expression<Func<PersonInfo, object>> _target;

        private PropertiesChange _change;

        [TestInitialize]
        public void TestInitialize()
        {
            _source = p => p.Address.City;
            _target = pi => pi.Location.Town;

            _change = GetPropertiesChange();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldThrowException_IfSequencesOfPropertiesHaveDifferentResultTypes()
        {
            _source = p => p.Address.Country;
            _target = pi => pi.Status;

            GetPropertiesChange();
        }

        [TestMethod]
        public void SourceCorrespondsTo_ShouldReturnFalse_IfPropertiesSequenceDoesNotCorrespondToSource()
        {
            Expression<Func<Person, string>> ex = p => p.Name;

            Assert.IsFalse(_change.SourceCorrespondsTo(ex.Body));
        }

        [TestMethod]
        public void SourceCorrespondsTo_ShouldReturnTrue_IfPropertiesSequenceCorrespondsToSource()
        {
            Expression<Func<Person, string>> ex = p => p.Address.City;

            Assert.IsTrue(_change.SourceCorrespondsTo(ex.Body));
        }

        [TestMethod]
        public void GetSequenceOriginExpression_ShouldReturnNull_IfPropertiesSequenceDoesNotCorrespondToSource()
        {
            Expression<Func<Person, string>> ex = p => p.Name;

            Assert.IsNull(_change.GetSequenceOriginExpression(ex.Body));
        }

        [TestMethod]
        public void GetSequenceOriginExpression_ShouldReturnNotNull_IfPropertiesSequenceCorrespondsToSource()
        {
            Expression<Func<Person, string>> ex = p => p.Address.City;

            Assert.AreEqual(typeof(Person), _change.GetSequenceOriginExpression(ex.Body).Type);
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