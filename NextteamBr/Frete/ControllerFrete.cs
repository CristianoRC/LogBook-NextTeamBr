﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace NextteamBr
{
	class ControllerFrete
	{
		public static bool SalvarFrete(Frete InformacoesFrete)
		{
			bool saida;
			string StrJSON;
			try
			{
				StrJSON = JsonConvert.SerializeObject(InformacoesFrete);

				//string url = "http://192.168.0.250/next/painel/";
				string url = "http://painel.nextteambr.com.br/registroapp.php";

				HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);
				UTF8Encoding encoding = new UTF8Encoding();

				string postData = StrJSON;
				byte[] data = encoding.GetBytes(postData);
				httpWReq.Method = "POST";
				httpWReq.ContentType = "application / json; charset = utf-8";
				httpWReq.ContentLength = data.Length;
				using (Stream stream = httpWReq.GetRequestStream())
				{
					stream.Write(data, 0, data.Length);
				}
				HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
				saida = true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);

				saida = false;
			}
			return saida;
		}
	}
}
