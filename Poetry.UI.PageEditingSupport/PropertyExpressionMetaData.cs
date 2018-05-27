namespace Poetry.UI.PageEditingSupport
{
    public class PropertyExpressionMetaData
    {
        public string PropertyName { get; }
        public object OwnerObject { get; }

        public PropertyExpressionMetaData(string propertyName, object ownerObject)
        {
            PropertyName = propertyName;
            OwnerObject = ownerObject;
        }
    }
}