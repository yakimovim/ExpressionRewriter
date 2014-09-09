namespace ExpressionRewriting.UnitTest.Model
{
    public class VariableInfo
    {
        public int VariableId { get; set; }
        public string Name { get; set; }
        public VariableType VariableType { get; set; }
        public StorageType StorageType { get; set; }
        public string MartTableName { get; set; }
        public string VariableSpaceName { get; set; }
        public Placement Placement { get; set; }
    }

    public class Placement
    {
        public string PhysicalTableName { get; set; }
        public string PhysicalSchemaName { get; set; }
    }
}