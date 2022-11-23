using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public partial class BusinessExceptions
    {
        public static string DeletedRecord = "Deleted Record.";
        public static string NotFound = "Not Found.";
        public static string UnknownType = "Unknown Type.";

        public static string BankError = "Bank Error";
        public static string BankBranchError = "Bank Branch Error";
        public static string BankAccountError = "Bank Account Error";
        public static string BankAccountActivityError = "Bank Account Activity Error";
        public static string BankAccountActivityLogError = "Bank Account Activity Log Error";
        public static string BankAccountTypeError = "Bank Account Type Error";

        public static string BranchError = "Branch Error";

        public static string CurrencyError = "Currency Error";

        public static string ProcessError = "Process Error";

        public static string IntegratorError = "Integrator Error";

        public static string CreditDebitIdentificationTypeError = "CreditDebitIdentificationType Error";
    }
}
