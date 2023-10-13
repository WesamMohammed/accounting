namespace jwt.Enums
{
    public enum OperationType
    {
        SalesInvoice,ReturnSalesInvoice,PurchasesInvoice, ReturnPurchasesInvoice
    }
    public static class OperationTypeDic {

        public static Dictionary<OperationType, string> dic = new Dictionary<OperationType, string>
        {
            {OperationType.SalesInvoice,OperationType.SalesInvoice.ToString()},
            {OperationType.ReturnSalesInvoice,OperationType.ReturnSalesInvoice.ToString()},
            {OperationType.PurchasesInvoice,OperationType.PurchasesInvoice.ToString()},
            {OperationType.ReturnPurchasesInvoice,OperationType.ReturnPurchasesInvoice.ToString() }
        };
        
    
    }
}
