using System;
using System.Linq;
using System.Linq.Expressions;
using ExpressionRewriting.UnitTest.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionRewriting.UnitTest
{
    [TestClass]
    public class ExpressionRewriterTest
    {
        private ExpressionRewriter _rewriter;
        private PersonRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new PersonRepository();

            _rewriter = new ExpressionRewriter();
            _rewriter.ChangeArgumentType<PersonInfo>().To<Person>();
            _rewriter.ChangeProperty<PersonInfo>(pi => pi.Status).To<Person>(p => p.FamilyStatus);
            _rewriter.ChangeProperty<PersonInfo>(pi => pi.Country).To<Person>(p => p.Address.Country);
            _rewriter.ChangeProperty<PersonInfo>(pi => pi.Location.Town).To<Person>(vd => vd.Address.City);
        }

        [TestMethod]
        public void SimpleRewriting_Works()
        {
            Expression<Func<PersonInfo, bool>> sourceEx = pi => pi.PersonId < 4;

            var targetEx = _rewriter.Rewrite<Func<Person, bool>>(sourceEx);

            var variableData = _repository.GetPersons().Where(targetEx.Compile()).Select(vd => vd.Name).ToArray();

            CollectionAssert.AreEquivalent(new[] { "John", "Mary", "David" }, variableData);
        }

        [TestMethod]
        public void SimpleRewriting_WithFunctionCall_Works()
        {
            Expression<Func<PersonInfo, bool>> sourceEx = pi => pi.Name.EndsWith("n");

            var targetEx = _rewriter.Rewrite<Func<Person, bool>>(sourceEx);

            var variableData = _repository.GetPersons().Where(targetEx.Compile()).Select(vd => vd.Name).ToArray();

            CollectionAssert.AreEquivalent(new[] { "John", "Ann", "Yen" }, variableData);
        }

        [TestMethod]
        public void RewritingOfProperties_Works()
        {
            Expression<Func<PersonInfo, bool>> sourceEx = pi => pi.Status == FamilyStatus.Single;

            var targetEx = _rewriter.Rewrite<Func<Person, bool>>(sourceEx);

            var variableData = _repository.GetPersons().Where(targetEx.Compile()).Select(vd => vd.Name).ToArray();

            CollectionAssert.AreEquivalent(new[] { "John", "Mary", "Svetlana" }, variableData);
        }

        [TestMethod]
        public void RewritingOfProperties_WithFunction_Works()
        {
            Expression<Func<PersonInfo, bool>> sourceEx = pi => pi.Location.Town.StartsWith("L");

            var targetEx = _rewriter.Rewrite<Func<Person, bool>>(sourceEx);

            var variableData = _repository.GetPersons().Where(targetEx.Compile()).Select(vd => vd.Name).ToArray();

            CollectionAssert.AreEquivalent(new[] { "Mary", "Ann", "Peter" }, variableData);
        }
    }
}
