using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Ninefold.API.Tests
{
    public static class AssertHelper
    {

        public static bool IsEncoded(string urlFragment)
        {
            //todo: determine logic for assuring fragment is encoded

            return true;
        }

        public static void IsAlphaOrdered(IEnumerable<Parameter> parameters)
        {
            var orderedSet = parameters.OrderBy(p => p.Name);
            for(var index = 0; index < parameters.Count(); index++)
            {
                Assert.AreEqual(orderedSet.ElementAt(index).Name, parameters.ElementAt(index).Name);  
            }
        }
    }
}
