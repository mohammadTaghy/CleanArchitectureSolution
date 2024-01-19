using DynamicODataToSQL;
using Microsoft.AspNetCore.OData.Query;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ODataExtention
{
    public class ODataQueryOptionsExt
    {
        public (string,IDictionary<string,object>) FilterExpression(ODataQueryOptions oDataQueryOptions,string tableName)
        {
            var transfom= oDataQueryOptions.Apply.ApplyClause.Transformations;
            var converter = new ODataToSqlConverter(new EdmModelBuilder(), new SqlServerCompiler() { UseLegacyPagination = false });
            Dictionary<string, string> dic = new Dictionary<string, string>(){
                    {"top", "20" },
                    {"skip", "5" },
                };
            if (oDataQueryOptions.Filter != null) 
                dic.Add("filter", oDataQueryOptions.Filter.RawValue);
            if (oDataQueryOptions.OrderBy != null)
                dic.Add("orderby", oDataQueryOptions.OrderBy.RawValue);
            if (oDataQueryOptions.SelectExpand != null)
                dic.Add("filter", oDataQueryOptions.SelectExpand.RawSelect);
           return converter.ConvertToSQL(tableName, dic, false);
            //foreach ( var expersion in oDataQueryOptions.Filter.FilterClause.Expression)
            //{

            //}
           
        }
    }
}
