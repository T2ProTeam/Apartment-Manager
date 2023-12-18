using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;

namespace ViewModel.People
{
    public class RequestPaging : PagingRequestBase
    {
        public string Keyword { get; set; } = string.Empty;
    }
}