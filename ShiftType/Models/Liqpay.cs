using Newtonsoft.Json;
using OfficeOpenXml.Style;
using System.Drawing.Text;
using System.Security.Cryptography;
using System.Text;

namespace ShiftType.Models
{
    public class LiqPay
    {
        private string _privateKey;
        private string _publicKey;
        private Params _param;
        private readonly IConfiguration _configuration;
        public LiqPay(IConfiguration configuration)
        {
            _configuration = configuration;
            _publicKey =_configuration["Liqpay:ClientId"];
            _privateKey = _configuration["Liqpay:ClientSecret"];
        }
        private string Sign(string data)
        {
            return Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(data)));
        }
        public Params PayParams(decimal amount)
        {
            return PayParams(amount, "Support Creator",Guid.NewGuid().ToString());
        }
        public Params PayParams(decimal amount, string description, string orderId)
        {
            _param = new Params
            {
                Version = 3,
                PublicKey = _publicKey,
                Action = "pay",
                Amount = amount,
                Currency = "USD",
                Description = description,
                OrderId = orderId,
                Language = "uk",
            };
            return _param;
        }

        public string GetData(Params param)
        {
            var json = JsonConvert.SerializeObject(param);
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            return data;
        }
		public bool ValidateData(string data, string signature)
		{
			return Sign(_privateKey + data + _privateKey) == signature;
		}
		public string GetSignature(Params param)
		{
			var signature = Sign(_privateKey + GetData(param) + _privateKey);
			return signature;
		}
	}
}
