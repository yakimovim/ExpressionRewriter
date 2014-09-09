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
        private VariableDataRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new VariableDataRepository();

            _rewriter = new ExpressionRewriter();
            _rewriter.ChangeArgumentType<VariableInfo>().To<VariableData>();
            _rewriter.ChangeProperty<VariableInfo>(vi => vi.VariableType).To<VariableData>(vd => vd.Type);
            _rewriter.ChangeProperty<VariableInfo>(vi => vi.StorageType).To<VariableData>(vd => vd.Storage.Type);
            _rewriter.ChangeProperty<VariableInfo>(vi => vi.MartTableName).To<VariableData>(vd => vd.Storage.MartTableName);
            _rewriter.ChangeProperty<VariableInfo>(vi => vi.VariableSpaceName).To<VariableData>(vd => vd.Storage.VariableSpaceName);
            _rewriter.ChangeProperty<VariableInfo>(vi => vi.Placement.PhysicalSchemaName).To<VariableData>(vd => vd.Storage.PhysicalSchemaName);
            _rewriter.ChangeProperty<VariableInfo>(vi => vi.Placement.PhysicalTableName).To<VariableData>(vd => vd.Storage.PhysicalTableName);
        }

        [TestMethod]
        public void SimpleRewriting_Works()
        {
            Expression<Func<VariableInfo, bool>> sourceEx = vi => vi.VariableId < 4;

            var targetEx = _rewriter.Rewrite<Func<VariableData, bool>>(sourceEx);

            var variableData = _repository.GetVariables().Where(targetEx.Compile()).Select(vd => vd.Name).ToArray();

            CollectionAssert.AreEquivalent(new[] { "respondent", "respid", "interview_start" }, variableData);
        }

        [TestMethod]
        public void SimpleRewriting_WithFunctionCall_Works()
        {
            Expression<Func<VariableInfo, bool>> sourceEx = vi => vi.Name.StartsWith("r");

            var targetEx = _rewriter.Rewrite<Func<VariableData, bool>>(sourceEx);

            var variableData = _repository.GetVariables().Where(targetEx.Compile()).Select(vd => vd.Name).ToArray();

            CollectionAssert.AreEquivalent(new[] { "respondent", "respid" }, variableData);
        }

        [TestMethod]
        public void RewritingOfProperties_Works()
        {
            Expression<Func<VariableInfo, bool>> sourceEx = vi => vi.VariableType == VariableType.Single;

            var targetEx = _rewriter.Rewrite<Func<VariableData, bool>>(sourceEx);

            var variableData = _repository.GetVariables().Where(targetEx.Compile()).Select(vd => vd.Name).ToArray();

            CollectionAssert.AreEquivalent(new[] { "status", "sex" }, variableData);
        }

        [TestMethod]
        public void RewritingOfProperties_WithFunction_Works()
        {
            Expression<Func<VariableInfo, bool>> sourceEx = vi => vi.Placement.PhysicalTableName.EndsWith("_2");

            var targetEx = _rewriter.Rewrite<Func<VariableData, bool>>(sourceEx);

            var variableData = _repository.GetVariables().Where(targetEx.Compile()).Select(vd => vd.Name).ToArray();

            CollectionAssert.AreEquivalent(new[] { "visited_cities" }, variableData);
        }
    }
}
