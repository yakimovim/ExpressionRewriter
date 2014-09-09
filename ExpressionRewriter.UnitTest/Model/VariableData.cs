namespace ExpressionRewriting.UnitTest.Model
{
    public class VariableData
    {
        public int VariableId { get; set; }
        public string Name { get; set; }
        public VariableType Type { get; set; }
        public VariableStorage Storage { get; set; }
    }

    public class VariableStorage
    {
        public StorageType Type { get; set; }
        public string MartTableName { get; set; }
        public string VariableSpaceName { get; set; }
        public string PhysicalSchemaName { get; set; }
        public string PhysicalTableName { get; set; }
    }

    public enum StorageType
    {
        Inline,
        Sparse,
        Verbose,
    }

    public enum VariableType
    {
        Single,
        Grid,
        Multi,
        OpenText,
        OpenTextList,
        Numeric,
        NumericList,
        DateTime,
    }
}