using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DappAPI.ViewModels
{
    public class GetNonceViewModel
    {
        public string publicAddress { get; set; }
        public long Nonce { get; set; }
    }
}
