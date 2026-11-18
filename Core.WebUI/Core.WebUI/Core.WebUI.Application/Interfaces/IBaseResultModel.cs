using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Interfaces
{
    public interface IBaseResultModel<TDto>
    {
        public TDto ModelData { get; set; }
    }
}