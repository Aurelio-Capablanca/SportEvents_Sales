using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using SportEvents_Sales_Front_End.Model;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SportEvents_Sales_Front_End.View.Public
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async Task LoginButton_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text;
            var pass = txtPassword.Text;
            var url = "http://0.0.0.0:5105/auth/do-login";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    AuthModel auth = new AuthModel
                    {
                        IsAdmin = false,
                        User = email,
                        Password = pass,
                    };
                    string json = JsonConvert.SerializeObject(auth);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(url, content).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        string result = await response.Content.ReadAsStringAsync();
                        lblLoginMessage.Text = result;
                        Debug.WriteLine(result);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error "+ex.Message);
                    throw ex;
                }
            }
        }
    }
}