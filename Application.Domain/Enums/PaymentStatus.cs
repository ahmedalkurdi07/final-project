using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending,         // الدفع معلق
        Completed,       // الدفع مكتمل
        Failed,          // الدفع فشل
        Refunded,        // الدفع مسترد
        Cancelled,       // الدفع ملغي
        PartiallyPaid,   // الدفع جزئي
        Overdue,         // الدفع متأخر
        InProcess        // الدفع قيد المعالجة
    }
}
