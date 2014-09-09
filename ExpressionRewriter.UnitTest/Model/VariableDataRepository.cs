using System.Collections.Generic;

namespace ExpressionRewriting.UnitTest.Model
{
    public class VariableDataRepository
    {
        public IEnumerable<VariableData> GetVariables()
        {
            yield return new VariableData
            {
                VariableId = 1,
                Name = "respondent",
                Type = VariableType.Numeric,
                Storage = new VariableStorage
                {
                    Type = StorageType.Inline,
                    MartTableName = "default",
                    VariableSpaceName = "default",
                    PhysicalSchemaName = "p123",
                    PhysicalTableName = "respondent_1"
                }
            };

            yield return new VariableData
            {
                VariableId = 2,
                Name = "respid",
                Type = VariableType.Numeric,
                Storage = new VariableStorage
                {
                    Type = StorageType.Inline,
                    MartTableName = "default",
                    VariableSpaceName = "default",
                    PhysicalSchemaName = "p123",
                    PhysicalTableName = "respondent_1"
                }
            };

            yield return new VariableData
            {
                VariableId = 3,
                Name = "interview_start",
                Type = VariableType.DateTime,
                Storage = new VariableStorage
                {
                    Type = StorageType.Inline,
                    MartTableName = "default",
                    VariableSpaceName = "default",
                    PhysicalSchemaName = "p123",
                    PhysicalTableName = "respondent_1"
                }
            };

            yield return new VariableData
            {
                VariableId = 4,
                Name = "status",
                Type = VariableType.Single,
                Storage = new VariableStorage
                {
                    Type = StorageType.Inline,
                    MartTableName = "default",
                    VariableSpaceName = "default",
                    PhysicalSchemaName = "p123",
                    PhysicalTableName = "respondent_1"
                }
            };

            yield return new VariableData
            {
                VariableId = 5,
                Name = "sex",
                Type = VariableType.Single,
                Storage = new VariableStorage
                {
                    Type = StorageType.Inline,
                    MartTableName = "default",
                    VariableSpaceName = "default",
                    PhysicalSchemaName = "p123",
                    PhysicalTableName = "respondent_1"
                }
            };

            yield return new VariableData
            {
                VariableId = 6,
                Name = "car",
                Type = VariableType.Grid,
                Storage = new VariableStorage
                {
                    Type = StorageType.Inline,
                    MartTableName = "default",
                    VariableSpaceName = "default",
                    PhysicalSchemaName = "p123",
                    PhysicalTableName = "respondent_1"
                }
            };

            yield return new VariableData
            {
                VariableId = 7,
                Name = "visited_cities",
                Type = VariableType.Multi,
                Storage = new VariableStorage
                {
                    Type = StorageType.Sparse,
                    MartTableName = "default",
                    VariableSpaceName = "default",
                    PhysicalSchemaName = "p123",
                    PhysicalTableName = "respondent_2"
                }
            };
        }
    }
}