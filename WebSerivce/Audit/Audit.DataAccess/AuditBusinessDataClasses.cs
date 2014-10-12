using JN.ESB.Core.Service.Common;
using System.Configuration;
namespace JN.ESB.Audit.DataAccess
{
    partial class AuditBusinessDataClassesDataContext
    {
        public AuditBusinessDataClassesDataContext() :
            base(EsbConfig.GetConnString("EsbAuditDB"), mappingSource)
        {
            OnCreated();
        }
    }
}
