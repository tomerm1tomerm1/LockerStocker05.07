using System;
using System.Collections.Generic;
using System.Text;

namespace essentialUIKitTry
{
    public static class Constants
    {
        public static readonly string tenantName = "LockerStockerDom";
        public static readonly string tenantId = "LockerStockerDom.onmicrosoft.com";
        public static readonly string clientId = "29963b61-6eb1-4b62-802d-a0815639140f";
        public static readonly string policySignin = "B2C_1_BSC_1_SignInSignUp";
        public static readonly string IosKeychainSecurityGroups = "com.microsoft.aabd2authentication";
        public static readonly string[] Scopes = new string[] { "openid", "offline_access" };
        public static readonly string AuthorityBase = $"https://{tenantName}.b2clogin.com/tfp/{tenantId}/";
        public static readonly string AuthoritySignIn = $"{AuthorityBase}{policySignin}";
    }
}
