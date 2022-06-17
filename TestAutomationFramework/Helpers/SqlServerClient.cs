using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace TestAutomationFramework.Helpers
{
    public class SqlServerClient : UserImpersonation
    {
        /// <summary>
        /// Opens a database connection (On-Premises or Azure) with SQL Authentication <br />
        /// <b>connectionString: </b>Provide the db connectionString <br />
        /// <b>Example: </b> connectionString = $"Data Source={serverName}.database.windows.net;Initial Catalog={dataBase};User Id={userName};password={userPassword};Encrypt=True;Trusted_Connection=false;";
        /// </summary>
        /// <returns>An Opened database connection</returns>
        public static SqlConnection OpenConnection(string connectionString)
        {
            try
            {
                var connection = new SqlConnection(connectionString);
                connection.Open();

                return connection;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Failed to open a SQL connection.\nErrorLogging:", ex);
            }
        }

        /// <summary>
        /// Open a database connection (On-Premises) with Windows Authentication. <br />
        /// <b>connectionString: </b>Provide the db connectionString <br />
        /// <b>windowsDomain: </b>Provide the windowsDomain. Ex: BCTGTWDOM <br />
        /// <b>windowsUserName: </b>Provide the windowsUserName <br />
        /// Ex: Service account userName: BCTGTWDOM/svcXyz
        /// </summary>
        /// <returns>An Opened database connection</returns>
        public static SqlConnection OpenConnection(string connectionString, string windowsDomain, string windowsUserName, string password)
        {
            try
            {
                return ImpersonateAsDifferUser(windowsDomain, windowsUserName, password, connectionString);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Failed to open a SQL connection.\nErrorLogging:", ex);
            }
        }

        /// <summary>
        /// Open a database connection (On Azure) with a service principal access token. <br />
        /// <b>connectionString: </b>Provide the db connectionString <br />
        /// <b>accessToken: </b>Provide the service principal access token <br />
        /// </summary>
        /// <returns>An Opened database connection</returns>
        public static SqlConnection OpenConnection(string connectionString, string accessToken)
        {
            try
            {
                var connection = new SqlConnection(connectionString)
                {
                    AccessToken = accessToken
                };
                connection.Open();

                return connection;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Failed to open a SQL connection.\nErrorLogging:", ex);
            }
        }
    }
    public class UserImpersonation
    {
        // Adopted from
        // https://stackoverflow.com/questions/5023607/how-to-use-logonuser-properly-to-impersonate-domain-user-from-workgroup-client
        // http://cstruter.com/blog/270

        public static SqlConnection ImpersonateAsDifferUser(string domain, string username, string password, string connectionString)
        {
            var conn = new SqlConnection(connectionString);

            // Impersonate a user under the same domain ONLY!
            //const int LOGON32_LOGON_INTERACTIVE = 2;
            //const int LOGON32_PROVIDER_DEFAULT = 0;

            // Impersonates a user under a different domain or same domain, but 2 domains should be trusted (meaning on the same network, like a VPN)!
            const int LOGON32_LOGON_NEWCREDENTAILS = 9;
            const int LOGON32_PROVIDER_WINNT50 = 3;

            bool returnValue = LogonUser(username, domain, password, LOGON32_LOGON_NEWCREDENTAILS, LOGON32_PROVIDER_WINNT50, out SafeAccessTokenHandle safeAccessTokenHandle);

            if (false == returnValue)
            {
                int ret = Marshal.GetLastWin32Error();
                Console.WriteLine("LogonUser failed with error code : {0}", ret);
                throw new System.ComponentModel.Win32Exception(ret);
            }

            var connection = WindowsIdentity.RunImpersonated(safeAccessTokenHandle, () =>
            {
                conn.Open();
                return conn;
            });

            return connection;
        }

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword,
                                            int dwLogonType, int dwLogonProvider, out SafeAccessTokenHandle phToken);
    }
}
