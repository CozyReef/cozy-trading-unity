using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
 
namespace ContractJson {
    public class Contracts {
        public List<Contract> contracts;
    }
    public class Contract {
        public string name;
        public string address;
    }
}
