using System;
using System.Net;
using YARG.Core.Logging;

class Server {
    public static void StartServer() {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:21734/");
        listener.Start();

        YargLogger.LogFormatInfo("Starting server on localhost:21734")

        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            HttpListenerResponse response = context.Response;
            string responseString = "<html><body><<b>Basic HTTPServer for YARG</b></body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);

            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }
    }
}