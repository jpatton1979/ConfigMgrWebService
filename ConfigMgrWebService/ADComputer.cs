﻿using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace ConfigMgrWebService
{
    public class ADComputer
    {
        public string SamAccountName { get; set; }
        public string CanonicalName { get; set; }
        public string DnsHostName { get; set; }
        public string DistinguishedName { get; set; }

        public ADComputer() { }

        /// <summary>
        /// The 2nd constructor for <see cref="ADComputer"/>.  Using the specified <see cref="DirectoryEntry"/>,
        /// this will populate the class's properties.
        /// </summary>
        /// <param name="dirEntry">The <see cref="DirectoryEntry"/> to use when populating this class's properties.</param>
        public ADComputer(DirectoryEntry dirEntry)
        {
            using (dirEntry)
            {
                this.DistinguishedName = dirEntry.Properties[ConfigMgrWebService.DISTINGUISHED_NAME].Value as string;
                this.CanonicalName = dirEntry.Properties[ConfigMgrWebService.COMMON_NAME].Value as string;
                this.DnsHostName = dirEntry.Properties[ConfigMgrWebService.DNS_HOST_NAME].Value as string;
                this.SamAccountName = dirEntry.Properties[ConfigMgrWebService.SAM_ACCOUNT_NAME].Value as string;
            }
        }

        private ADComputer(ADComputerFromDC withDC)
        {
            this.CanonicalName = withDC.CanonicalName;
            this.SamAccountName = withDC.SamAccountName;
            this.DistinguishedName = withDC.DistinguishedName;
            this.DnsHostName = withDC.DnsHostName;
        }

        public static explicit operator ADComputer(ADComputerFromDC compFromDC) => new ADComputer(compFromDC);
    }
}