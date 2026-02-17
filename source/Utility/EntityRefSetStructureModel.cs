using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelGenerator.Utility
{
    internal class EntityRefSetStructureModel
    {
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public string Field { get; set; }

        public bool IsForeignKey { get; set; }
        public bool IsRelationKey { get; set; }
        public bool IsIndexedRelation { get; set; }

        public string RefItem { get; set; }
        public string SetItem { get; set; }
        public RefSetJsonModel RefJson { get; set; }
        public List<RefSetJsonModel> SetJson { get; set; }
    }

    internal class RefSetJsonModel
    {
        public string Schema { get; set; }
        public string Field { get; set; }
        public string InverseKey { get; set; }
    }

}
