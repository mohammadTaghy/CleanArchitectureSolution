using Application.Common.Interfaces;
using Application.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class PermissionTreeDto: CommonTreeDto<PermissionTreeDto>
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public byte FeatureType { get; set; }
        public bool HasPermission { get; set; }
    }
}
